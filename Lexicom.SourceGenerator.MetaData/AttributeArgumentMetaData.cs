using Lexicom.SourceGenerator.MetaData.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lexicom.SourceGenerator.MetaData;
public record struct AttributeArgumentMetaData
{
    private const string TYPEOF_START = "typeof(";
    private const string TYPEOF_END = ")";

    private readonly Compilation _compilation;
    private readonly AttributeArgumentSyntax _syntax;
    private readonly IParameterSymbol _parameterSymbol;

    /// <exception cref="ArgumentNullException"/>
    public AttributeArgumentMetaData(
        Compilation compilation,
        AttributeArgumentSyntax syntax,
        IParameterSymbol parameterSymbol)
    {
        if (compilation is null)
        {
            throw new ArgumentNullException(nameof(compilation));
        }
        if (syntax is null)
        {
            throw new ArgumentNullException(nameof(syntax));
        }
        if (parameterSymbol is null)
        {
            throw new ArgumentNullException(nameof(parameterSymbol));
        }

        _compilation = compilation;
        _syntax = syntax;
        _parameterSymbol = parameterSymbol;

        _value = null;
        _parameter = null;
        _constantSyntaxExpressionString = null;
    }

    private string? _value;
    /// <exception cref="FailedToParseMetaDataException{AttributeArgumentDescription}"/>
    public string Value
    {
        get
        {
            if (_value is null)
            {
                string? attributeSyntaxString = _syntax.ToString();

                if (!string.IsNullOrWhiteSpace(attributeSyntaxString) && attributeSyntaxString.StartsWith(TYPEOF_START) && attributeSyntaxString.EndsWith(TYPEOF_END))
                {
                    int length = attributeSyntaxString.Length - TYPEOF_START.Length - TYPEOF_END.Length;

                    if (length <= 0)
                    {
                        throw new FailedToParseMetaDataException<AttributeArgumentMetaData>(nameof(Value), $"the '{nameof(attributeSyntaxString)}' had no length");
                    }

                    _value = attributeSyntaxString.Substring(TYPEOF_START.Length, length);
                }
                else
                {
                    _value = ConstantSyntaxExpressionString;
                }
            }

            return _value;
        }
    }

    private ParameterMetaData? _parameter;
    public ParameterMetaData Parameter => _parameter ??= new ParameterMetaData(_parameterSymbol);

    private string? _constantSyntaxExpressionString;
    private string ConstantSyntaxExpressionString => _constantSyntaxExpressionString ??= _compilation.GetSemanticModel(_syntax.SyntaxTree).GetConstantValue(_syntax.Expression).ToString();

    public override string ToString() => Value;
}
