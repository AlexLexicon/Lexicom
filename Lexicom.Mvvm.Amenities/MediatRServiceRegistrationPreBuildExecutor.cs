using Lexicom.DependencyInjection.Hosting;
using Lexicom.Mvvm.Support.Extensions;
using Lexicom.Mvvm.Support;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Mvvm.Amenities;
public class MediatRServiceRegistrationPreBuildExecutor : IDependencyInjectionHostPreBuildExecutor
{
    /*
     * [The problem]
     * the default mediatR service registration will create a new instance of a service that is implementing a handler based on the registred lifetime for handlers given to mediatR.
     * this means whenever that handler is invoked via a Send or Publish a new handler is often created.
     * 
     * for example: if you have some transient service 'MyService' that implements an 'IRequestHandler<MyRequest>'
     * and you send a new 'MyRequest', mediatR will create a new instance of 'MyService' to handle that request.
     * 
     * this is fine in most applications, however with view models we want only current instances that already exist to handle the requests/notifications that are sent.
     * and we do not want new instances of those view models to ever be created outside of the 'ViewModelFactory'. (Note the factory is used when injecting into another service directly)
     * 
     * [The solution]
     * this implementation uses the normal MediatR assembly scanning to add all handlers and other dependencies
     * then we un-register all handlers (IRequestHandler/INotificationHandler) that are implemented on view models specifically
     * we then register one 'IEnumerable<THandler>' for each type of handler that was implemented on a view model.
     * this 'IEnumerable<THandler>' represents a factory which points to a weak refrence for all of the current view model instances using the 'WeakViewModelRefrenceCollection'
     * that have been created by the 'ViewModelFactory'. because these are weak refrences
     * as soon as the view model would be garbage collected it is and without handing in this 'WeakViewModelRefrenceCollection' object.
     * 
     * what this means is that if you new up three transient view models via the 'ViewModelFactory'
     * they will be added as weak refrences to the 'WeakViewModelRefrenceCollection' and when you send a request/notification
     * through mediatR it will use those already created instances to receive the request/notification via the 'IEnumerable<THandler>' service registration
     * 
     * this is how we solve for view models but there is one additional complication.
     * if a view model and a regular service both implement the same 'IRequestHandler' or 'INotificationHandler' 
     * we must add that specific service registration to also be returned via the 'IEnumerable<THandler>' service registration.
     * to do this we remove that service's regular mediatR registration and instead replace it with a 'MediatRHandlerImplementationConflictingWithViewModels<THandler>' registration
     * then in the 'IEnumerable<THandler>' service registration factory we also pull for all 'MediatRHandlerImplementationConflictingWithViewModels<THandler>'
     * that match the 'THandler' type and combine those with the instances within the 'WeakViewModelRefrenceCollection'.
     * 
     * an additional complexity occurs when you have a generic class that implements an 'INotificationHandler'.
     * in this case mediatR registers the 'INotificationHandler' as an 'INotificationHandler<TNotification>' instead of the actual concrete notification types
     * 
     * for example: if you create a class like this:
     * public class MyClass<T> INotificationHander<MyNoticiation>
     * {
     *     ...
     * }
     * MediatR will register this 'INotificationHander' as 'INotificationHandler<TNotification>' instead of 'INotificationHandler<MyNoticiation>'
     * so in these cases I manually check for this specific generic argument 'TNotification' and manually register the notification handlers for the serivce type
     */

    public void Execute(IServiceCollection services)
    {
        var notificationHandlersForViewModels = new List<ServiceDescriptor>();
        notificationHandlersForViewModels.AddRange(ReRegisterMediatRHandlersForViewModels(services, typeof(INotificationHandler<>)));
        ReRegisterMediatRHandlersForViewModels(services, typeof(IRequestHandler<>));
        ReRegisterMediatRHandlersForViewModels(services, typeof(IRequestHandler<,>));

        foreach (ServiceDescriptor notificationHandlerDescription in notificationHandlersForViewModels)
        {
            List<ServiceDescriptor> handlerImplementations = services
                .Where(sd => sd.ServiceType == notificationHandlerDescription.ServiceType)
                .ToList();

            foreach (ServiceDescriptor handlerImplementation in handlerImplementations)
            {
                if (handlerImplementation.ImplementationType is null)
                {
                    throw new UnreachableException($"The mediatR handler with the service type '{handlerImplementation.ServiceType}' has a 'null' implementation type but that is not possible in this case.");
                }

                services.Remove(handlerImplementation);

                StaticSetupHandlersForImplementationsConflictingWithViewModelsMethodInfo
                    .MakeGenericMethod(notificationHandlerDescription.ServiceType, handlerImplementation.ImplementationType)
                    .Invoke(null, new object[]
                    {
                        services,
                        handlerImplementation.Lifetime
                    });
            }
        }
    }

    private static List<ServiceDescriptor> ReRegisterMediatRHandlersForViewModels(IServiceCollection services, Type genericHandlerInterfaceType)
    {
        List<ServiceDescriptor> handlerDescriptors = services
            .Where(sd => sd.ServiceType.IsGenericType && sd.ServiceType.GetGenericTypeDefinition() == genericHandlerInterfaceType)
            .ToList();

        var handlersForViewModels = new List<ServiceDescriptor>();
        foreach (ServiceDescriptor handlerDescriptor in handlerDescriptors)
        {
            List<ViewModelRegistration> viewModelRegistrations = services.GetViewModelRegistrations();

            List<ViewModelRegistration> viewModelRegistrationsForHandler = viewModelRegistrations
                .Where(vmr =>
                    vmr.ServiceType == handlerDescriptor.ImplementationType ||
                    vmr.ImplementationType == handlerDescriptor.ImplementationType ||
                    vmr.ImplementationType.IsGenericType && vmr.ImplementationType.GetGenericTypeDefinition() == handlerDescriptor.ImplementationType)
                .ToList();

            if (viewModelRegistrationsForHandler.Count is not 0)
            {
                Type[] genericArguments = handlerDescriptor.ServiceType.GetGenericArguments();

                //this is tricky since mediatR does not actually register those generic classes
                //like it does with notifications. some additional custom magic is nessasary.
                if (genericArguments.Any(ga => ga.Name is "TNotification"))
                {
                    handlersForViewModels.Add(handlerDescriptor);

                    services.Remove(handlerDescriptor);

                    foreach (var viewModelRegistrationForHandler in viewModelRegistrationsForHandler)
                    {
                        Type[] interfaces = viewModelRegistrationForHandler.ServiceType.GetInterfaces();

                        var handlerInterfaces = interfaces
                            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericHandlerInterfaceType)
                            .ToList();

                        foreach (var handlerInterface in handlerInterfaces)
                        {
                            StaticSetupNotificationHandlersForViewModelsMethodInfo
                                .MakeGenericMethod(viewModelRegistrationForHandler.ServiceType, handlerInterface)
                                .Invoke(null, new object[]
                                {
                                    services
                                });
                        }
                    }
                }
                else
                {
                    foreach (ViewModelRegistration viewModelRegistrationForHandler in viewModelRegistrationsForHandler)
                    {
                        handlersForViewModels.Add(handlerDescriptor);

                        services.Remove(handlerDescriptor);

                        if (genericHandlerInterfaceType == typeof(INotificationHandler<>))
                        {
                            StaticSetupNotificationHandlersForViewModelsMethodInfo
                                .MakeGenericMethod(viewModelRegistrationForHandler.ServiceType, handlerDescriptor.ServiceType)
                                .Invoke(null, new object[]
                                {
                                    services
                                });
                        }
                        else
                        {
                            StaticSetupRequestHandlersForViewModelsMethodInfo
                                .MakeGenericMethod(viewModelRegistrationForHandler.ServiceType, handlerDescriptor.ServiceType)
                                .Invoke(null, new object[]
                                {
                                    services
                                });
                        }
                    }
                }
            }
        }

        return handlersForViewModels;
    }

    private static MethodInfo? _staticSetupNotificationHandlersForViewModelsMethodInfo;
    private static MethodInfo StaticSetupNotificationHandlersForViewModelsMethodInfo => _staticSetupNotificationHandlersForViewModelsMethodInfo ??= (typeof(MediatRServiceRegistrationPreBuildExecutor).GetMethod(nameof(SetupNotificationHandlersForViewModels), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(SetupNotificationHandlersForViewModels)}' was not found."));
    private static void SetupNotificationHandlersForViewModels<TViewModelImplementation, THandler>(IServiceCollection services) where TViewModelImplementation : class, THandler where THandler : class
    {
        services.AddTransient<IMediatRHandlersProvider<THandler>, MediatRHandlersProvider<THandler, TViewModelImplementation>>();

        services.TryAddSingleton<WeakViewModelRefrenceCollection<TViewModelImplementation>>();

        services.TryAddTransient<IEnumerable<THandler>>(GetHandlers<THandler>);
    }

    private static MethodInfo? _staticSetupRequestHandlersForViewModelsMethodInfo;
    private static MethodInfo StaticSetupRequestHandlersForViewModelsMethodInfo => _staticSetupRequestHandlersForViewModelsMethodInfo ??= (typeof(MediatRServiceRegistrationPreBuildExecutor).GetMethod(nameof(SetupRequestHandlersForViewModels), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(SetupRequestHandlersForViewModels)}' was not found."));
    private static void SetupRequestHandlersForViewModels<TViewModelImplementation, THandler>(IServiceCollection services) where TViewModelImplementation : class, THandler where THandler : class
    {
        services.AddTransient<IMediatRHandlersProvider<THandler>, MediatRHandlersProvider<THandler, TViewModelImplementation>>();

        services.TryAddSingleton<WeakViewModelRefrenceCollection<TViewModelImplementation>>();

        services.TryAddTransient(sp =>
        {
            var handlers = GetHandlers<THandler>(sp);

            return handlers.First();
        });
    }

    private static List<THandler> GetHandlers<THandler>(IServiceProvider serviceProvider) where THandler : class
    {
        var mediatRHandlersProvider = serviceProvider.GetRequiredService<IEnumerable<IMediatRHandlersProvider<THandler>>>();

        var viewModelHandlers = new List<THandler>();
        var regularHandlers = new List<THandler>();
        foreach (IMediatRHandlersProvider<THandler> provider in mediatRHandlersProvider)
        {
            IEnumerable<THandler> providerViewModelHandlers = provider.GetViewModelHandlers();
            viewModelHandlers.AddRange(providerViewModelHandlers);

            IEnumerable<THandler> providerRegularHandlers = provider.GetRegularHandlers();
            regularHandlers.AddRange(providerRegularHandlers);
        }

        var handlers = new List<THandler>();
        handlers.AddRange(regularHandlers.DistinctBy(h => h.GetType().FullName));
        handlers.AddRange(viewModelHandlers);

        return handlers;
    }

    private static MethodInfo? _staticSetupHandlersForImplementationsConflictingWithViewModelsMethodInfo;
    private static MethodInfo StaticSetupHandlersForImplementationsConflictingWithViewModelsMethodInfo => _staticSetupHandlersForImplementationsConflictingWithViewModelsMethodInfo ??= (typeof(MediatRServiceRegistrationPreBuildExecutor).GetMethod(nameof(SetupHandlersForImplementationsConflictingWithViewModels), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(SetupHandlersForImplementationsConflictingWithViewModels)}' was not found."));
    private static void SetupHandlersForImplementationsConflictingWithViewModels<THandler, TImplementation>(IServiceCollection services, ServiceLifetime serviceLifetime) where TImplementation : class, THandler
    {
        services.Add(new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), serviceLifetime));

        //these 'MediatRHandlerImplementationConflictingWithViewModels' are pulled into the 'MediatRHandlersProvider'
        services.Add(new ServiceDescriptor(typeof(MediatRHandlerImplementationConflictingWithViewModels<THandler>), sp =>
        {
            var implementation = sp.GetRequiredService<TImplementation>();

            return new MediatRHandlerImplementationConflictingWithViewModels<THandler>(implementation, serviceLifetime);
        }, serviceLifetime));
    }
}
