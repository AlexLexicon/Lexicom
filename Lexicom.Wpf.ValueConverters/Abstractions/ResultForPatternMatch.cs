namespace Lexicom.Wpf.ValueConverters.Abstractions;
public class ResultForPatternMatch<T>(T result, IEnumerable<string> matchPatterns)
{
    public T Result { get; } = result;
    public IReadOnlyList<string> Patterns { get; } = matchPatterns.ToList();
}
