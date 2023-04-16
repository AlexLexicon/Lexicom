using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    internal static T CreateAddCode<T>(string errorCode) where T : ObjectResult, IObjectResultBuilder, new()
    {
        ArgumentNullException.ThrowIfNull(errorCode);

        var builder = new T();

        AddCode(builder, errorCode);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    internal static T CreateFromProperty<T>(string propertyString) where T : ObjectResult, IResultRequestPropertyBuilder, new()
    {
        ArgumentNullException.ThrowIfNull(propertyString);

        string name = GetNameFromPropertyString(propertyString);

        return CreateFromKey<T>(name);
    }

    /// <exception cref="ArgumentNullException"/>
    internal static T CreateFromKey<T>(string key) where T : ObjectResult, IResultRequestPropertyBuilder, new()
    {
        ArgumentNullException.ThrowIfNull(key);

        var builder = new T
        {
            ErrorKey = key,
        };

        FromKey(builder, key);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    internal static IObjectResultBuilder AddCode(this IObjectResultBuilder builder, string code)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(code);

        if (builder is ObjectResult objectResult)
        {
            if (objectResult.Value is not ControllerErrorResponse errorResponse)
            {
                errorResponse = new ControllerErrorResponse();
                objectResult.Value = errorResponse;
            }

            errorResponse.AddCode(code);
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/> 
    internal static IObjectResultBuilder FromProperty(this IObjectResultBuilder builder, string propertyString)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        string name = GetNameFromPropertyString(propertyString);

        return FromKey(builder, name);
    }

    /// <exception cref="ArgumentNullException"/> 
    internal static IObjectResultBuilder FromKey(this IObjectResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        if (builder is ObjectResult objectResult)
        {
            objectResult.Value = objectResult.Value;

            if (objectResult.Value is not ControllerErrorResponse errorResponse)
            {
                errorResponse = new ControllerErrorResponse();
                objectResult.Value = errorResponse;
            }

            errorResponse.AddError(errorKey);

            if (builder is IResultRequestPropertyBuilder propertyBuilder)
            {
                propertyBuilder.ErrorKey = errorKey;
            }
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    internal static IResultRequestPropertyBuilder WithMessage(this IResultRequestPropertyBuilder builder, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorMessage);

        if (builder is ObjectResult objectResult && objectResult.Value is ControllerErrorResponse errorResponse && errorResponse.Errors is not null && builder.ErrorKey is not null)
        {
            errorResponse.AddError(builder.ErrorKey, errorMessage);
        }

        return builder;
    }

    private static string GetNameFromPropertyString(string argumentString)
    {
        int lastPeriod = argumentString.LastIndexOf('.');

        string name;
        if (lastPeriod >= 0)
        {
            name = argumentString[lastPeriod..].Trim('.');
        }
        else
        {
            name = argumentString;
        }

        return name;
    }
}
