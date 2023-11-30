﻿namespace Lexicom.DependencyInjection.Hosting;
//classes that implement this interface and are registed in the service collection will
//have their run function called immediately after the service provider is built
public interface IDependencyInjectionHostPostBuildService
{
    void Run(IServiceProvider provider);
}
