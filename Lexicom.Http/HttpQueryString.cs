using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace Lexicom.Http;
public class HttpQueryString : ICollection<HttpQueryParameter>
{
    private readonly List<HttpQueryParameter> _parameters;

    public HttpQueryString()
    {
        _parameters = [];
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryString(IEnumerable<HttpQueryParameter> parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        _parameters = parameters.ToList();
    }

    public int Count => _parameters.Count;
    public bool IsReadOnly => false;

    public void Clear() => _parameters.Clear();

    /// <exception cref="ArgumentNullException"/>
    public bool Contains(HttpQueryParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return _parameters.Contains(parameter);
    }

    /// <exception cref="ArgumentNullException"/>
    public void CopyTo(HttpQueryParameter[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        _parameters.CopyTo(array, arrayIndex);
    }

    /// <exception cref="ArgumentNullException"/>
    public bool Remove(HttpQueryParameter item)
    {
        ArgumentNullException.ThrowIfNull(item);

        return _parameters.Remove(item);
    }

    /// <exception cref="ArgumentNullException"/>
    public void Add(HttpQueryParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        if (!string.IsNullOrWhiteSpace(parameter.Name) && !string.IsNullOrWhiteSpace(parameter.Value))
        {
            _parameters.Add(parameter);
        }
    }

    public override string ToString()
    {
        NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);

        foreach (HttpQueryParameter parameter in _parameters)
        {
            nameValueCollection.Add(parameter.Name, parameter.Value);
        }

        string? parameters = nameValueCollection.ToString();

        if (string.IsNullOrWhiteSpace(parameters))
        {
            return string.Empty;
        }

        return $"?{parameters}";
    }

    public IEnumerator<HttpQueryParameter> GetEnumerator() => _parameters.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}