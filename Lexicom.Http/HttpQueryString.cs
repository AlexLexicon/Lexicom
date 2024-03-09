using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace Lexicom.Http;
public class HttpQueryString : IList<HttpQueryParameter>
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

    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
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

    /// <exception cref="ArgumentNullException"/>
    public int IndexOf(HttpQueryParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return _parameters.IndexOf(parameter);
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Insert(int index, HttpQueryParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        if (!string.IsNullOrWhiteSpace(parameter.Name) && !string.IsNullOrWhiteSpace(parameter.Value))
        {
            _parameters.Insert(index, parameter);
        }
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void RemoveAt(int index)
    {
        _parameters.RemoveAt(index);
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public HttpQueryParameter this[int index]
    {
        get => _parameters[index];
        set => Insert(index, value);
    }
    /// <exception cref="ArgumentNullException"/>
    public IReadOnlyList<HttpQueryParameter> this[string name]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(name);

            var parameters = new List<HttpQueryParameter>();
            foreach (HttpQueryParameter parameter in _parameters)
            {
                if (parameter.Name == name)
                {
                    parameters.Add(parameter);
                }
            }

            return parameters;
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