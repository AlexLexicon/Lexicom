using System.Collections;

namespace Lexicom.Wpf.ValueConverters.Abstractions;
public class ResultForPatternMatchCollection<T> : IEnumerable<ResultForPatternMatch<T>>
{
    private readonly List<ResultForPatternMatch<T>> _resultForPatternMatches;

    public ResultForPatternMatchCollection()
    {
        _resultForPatternMatches = [];
    }

    public void Add(T result, string matchPattern)
    {
        Add(new ResultForPatternMatch<T>(result, new List<string> 
        { 
            matchPattern,
        }));
    }
    public void Add(T result, IEnumerable<string> matchPatterns)
    {
        Add(new ResultForPatternMatch<T>(result, matchPatterns));
    }
    public void Add(ResultForPatternMatch<T> resultForPatternMatch)
    {
        _resultForPatternMatches.Add(resultForPatternMatch);
    }

    public IEnumerator<ResultForPatternMatch<T>> GetEnumerator() => _resultForPatternMatches.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
