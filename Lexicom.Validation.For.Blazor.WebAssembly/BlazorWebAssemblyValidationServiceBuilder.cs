﻿namespace Lexicom.Validation.For.Blazor.WebAssembly;
public class BlazorWebAssemblyValidationServiceBuilder : IBlazorWebAssemblyValidationServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public BlazorWebAssemblyValidationServiceBuilder(IValidationServiceBuilder validationServiceBuilder)
    {
        ArgumentNullException.ThrowIfNull(validationServiceBuilder);

        ValidationBuilder = validationServiceBuilder;
    }

    public IValidationServiceBuilder ValidationBuilder { get; }
}