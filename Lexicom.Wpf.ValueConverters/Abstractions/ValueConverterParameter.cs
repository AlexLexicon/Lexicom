namespace Lexicom.Wpf.ValueConverters.Abstractions;
public class ValueConverterParameter
{
    public ValueConverterParameter(
        string key, 
        string[] values)
    {
        Key = key;
        Values = values;
    }

    public string Key { get; }
    public string[] Values { get; }
}
