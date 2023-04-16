using FluentValidation;
using FluentValidation.Results;
using Lexicom.Validation.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Validation.For.AspNetCore.Controllers.Filters;
/*
 * this filter detects a request body ([FromBody]) and finds the associated IValidator for it 
 * then calls the validator to handle the request body and returns a bad request if it fails
 * 
 * Fluent Validation already has this functionality built into its own FluentValidation.AspNetCore nuget package
 * however this funtionality is no longer recommended: https://github.com/FluentValidation/FluentValidation.AspNetCore (see readme introduction)
 * additionally the default Fluent Validation implementation has a response message that includes information i do not want to return to the client
 * and as such this custom filter allows me to customize what that response looks like and standardizing it
 */
public class RequestBodyValidationActionFilter : IAsyncActionFilter
{
    /*
     * the FluentValidation validators have a generic argument
     * but we need to get them from the service provider without knowing what that generic type is
     * so we create a MethodInfo wrapper around the gneric ValidateAsync
     * which we can invoke via reflection
     */
    private static MethodInfo? _staticInvokeValidatorMethodInfo;
    private static MethodInfo StaticInvokeValidatorMethodInfo => _staticInvokeValidatorMethodInfo ??= (typeof(RequestBodyValidationActionFilter).GetMethod(nameof(StaticInvokeValidator), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(StaticInvokeValidator)}' was not found."));
    private static Task<ValidationResult>? StaticInvokeValidator<T>(HttpContext httpContext, T requestBody)
    {
        IValidator<T>? validator = httpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is not null)
        {
            return validator.ValidateAsync(requestBody);
        }

        return null;
    }

    /// <exception cref="ArgumentNullException"/>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        //get the request body parameter from this invoked controller action
        ControllerParameterDescriptor? requestBodyParamter = context.ActionDescriptor.Parameters
            .Where(p => p is ControllerParameterDescriptor)
            .Cast<ControllerParameterDescriptor>()
            .Where(d => d.ParameterInfo.GetCustomAttribute<FromBodyAttribute>() is not null)
            .FirstOrDefault();

        if (requestBodyParamter is not null)
        {
            object? requestBody = context.ActionArguments[requestBodyParamter.Name];

            if (requestBody is not null)
            {
                //because IValidator<T> requires a generic argument we have to invoke a reflected generic method 
                MethodInfo invokeValidatorMethodInfo = StaticInvokeValidatorMethodInfo.MakeGenericMethod(requestBody.GetType());

                var validationResultTask = (Task<ValidationResult>?)invokeValidatorMethodInfo.Invoke(null, new[]
                {
                    context.HttpContext,
                    requestBody
                });

                //validationResultTask is not null if a validator for the request body type was found
                if (validationResultTask is not null)
                {
                    ValidationResult validationResult = await validationResultTask;

                    if (!validationResult.IsValid)
                    {
                        IReadOnlyList<ValidationFailure> sanitizedValidationFailures = validationResult.Errors.StandardizeErrorMessages();

                        var errors = new Dictionary<string, IEnumerable<string>>();
                        foreach (ValidationFailure validationFailure in sanitizedValidationFailures)
                        {
                            string key = validationFailure.PropertyName;

                            if (errors.TryGetValue(key, out IEnumerable<string>? value))
                            {
                                var messages = (List<string>)value;

                                messages.Add(validationFailure.ErrorMessage);
                            }
                            else
                            {
                                errors.Add(key, new List<string>
                                {
                                    validationFailure.ErrorMessage,
                                });
                            }
                        }

                        //a Dictionary<string, IEnumerable<string>> can automatically be converted to an ErrorResponse
                        //in the Lexicom.AspNetCore.Controllers.Amenities.ObjectResultToErrorResponseActionFilter
                        //if that package is used
                        context.Result = new BadRequestObjectResult(errors);
                    }
                }
            }
        }

        if (context.Result is null)
        {
            await next();
        }
    }
}