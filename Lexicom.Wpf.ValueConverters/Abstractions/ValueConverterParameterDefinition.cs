namespace Lexicom.Wpf.ValueConverters.Abstractions;
public class ValueConverterParameterDefinition<T> : ValueConverterParameterDefinition
{
    private readonly Func<string[], T?>? _parseDelegate;

    /// <exception cref="ArgumentNullException"/>
    public ValueConverterParameterDefinition(string pattern) : this(pattern, parseDelegate: null, null)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public ValueConverterParameterDefinition(string pattern, ValueConverterParameterSettings? settings) : this(pattern, parseDelegate: null, settings)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public ValueConverterParameterDefinition(string pattern, Func<string[], T?>? parseDelegate) : this(pattern, parseDelegate, null)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public ValueConverterParameterDefinition(string pattern, Func<string[], T?>? parseDelegate, ValueConverterParameterSettings? settings) : base(pattern, settings)
    {
        _parseDelegate = parseDelegate;
        ResultForPatternMatches = null;
    }
    /// <exception cref="ArgumentNullException"/>
    public ValueConverterParameterDefinition(string pattern, ResultForPatternMatchCollection<T> resultForPatternMatchCollection) : this(pattern, resultForPatternMatchCollection, null)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public ValueConverterParameterDefinition(string pattern, ResultForPatternMatchCollection<T> resultForPatternMatchCollection, ValueConverterParameterSettings? settings) : base(pattern, settings)
    {
        ArgumentNullException.ThrowIfNull(resultForPatternMatchCollection);

        _parseDelegate = null;
        ResultForPatternMatches = resultForPatternMatchCollection;
    }

    protected ResultForPatternMatchCollection<T>? ResultForPatternMatches { get; }

    public override bool Match(IReadOnlyList<ValueConverterParameter> parameters) => Match(parameters, out T? _);
    public virtual bool Match(IReadOnlyList<ValueConverterParameter> parameters, out T? value)
    {
        var comparison = Settings?.Comparer ?? StringComparison.OrdinalIgnoreCase;

        ValueConverterParameter? parameter = parameters.FirstOrDefault(kvp => string.Equals(kvp.Key, Pattern, comparison));

        value = default;
        if (parameter is not null)
        {
            if (_parseDelegate is not null)
            {
                value = _parseDelegate.Invoke(parameter.Values);

                return true;
            }
            else if (ResultForPatternMatches is not null && parameter.Values.Length is not 0)
            {
                var matches = new List<ResultForPatternMatch<T>>();
                foreach (string parameterValue in parameter.Values)
                {
                    ResultForPatternMatch<T>? resultForPatternMatch = ResultForPatternMatches.FirstOrDefault(rpm => rpm.Patterns.Any(p => string.Equals(p, parameterValue, comparison)));

                    if (resultForPatternMatch is not null)
                    {
                        value = resultForPatternMatch.Result;
                        break;
                    }
                }

                return true;
            }
        }

        return false;
    }
}
public class ValueConverterParameterDefinition
{
    public ValueConverterParameterDefinition(string pattern) : this(pattern, null)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public ValueConverterParameterDefinition(string pattern, ValueConverterParameterSettings? settings)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        Pattern = pattern;
        Settings = settings;
    }

    public string Pattern { get; }
    protected ValueConverterParameterSettings? Settings { get; }

    public virtual bool Match(IReadOnlyList<ValueConverterParameter> parameters)
    {
        var comparison = Settings?.Comparer ?? StringComparison.OrdinalIgnoreCase;

        return parameters.Any(kvp => string.Equals(kvp.Key, Pattern, comparison));
    }
}