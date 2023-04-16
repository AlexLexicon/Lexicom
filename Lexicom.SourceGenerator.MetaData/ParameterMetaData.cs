using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;

namespace Lexicom.SourceGenerator.MetaData;
public record struct ParameterMetaData
{
    private readonly IParameterSymbol _symbolInfo;

    /// <exception cref="ArgumentNullException"/>
    public ParameterMetaData(IParameterSymbol symbolInfo)
    {
        if (symbolInfo is null)
        {
            throw new ArgumentNullException(nameof(symbolInfo));
        }

        _symbolInfo = symbolInfo;

        _name = null;
        _type = null;
    }

    private string? _name;
    public string Name => _name ??= _symbolInfo.Name;

    private TypeMetaData? _type;
    /// <exception cref="FailedToParseMetaDataException{ParameterDescription}"/>
    public TypeMetaData Type
    {
        get
        {
            if (_type is null)
            {
                ITypeSymbol symbolType = _symbolInfo.Type;

                if (symbolType is INamedTypeSymbol namedTypeSymbol)
                {
                    _type = new TypeMetaData(namedTypeSymbol);
                }
                else if (symbolType is IArrayTypeSymbol arrayTypeSymbol)
                {
                    _type = new TypeMetaData(arrayTypeSymbol);
                }
                else
                {
                    throw new FailedToParseMetaDataException<ParameterMetaData>(nameof(Type), $"the '{nameof(symbolType)}' was not a '{nameof(INamedTypeSymbol)}' or a '{nameof(IArrayTypeSymbol)}'");
                }
            }

            return _type.Value;
        }
    }

    public override string ToString() => Name;
}
