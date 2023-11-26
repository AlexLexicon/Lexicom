namespace Lexicom.Extensions.Reflection;
public static class TypeExtensions
{
    public static string GetFriendlyName(this Type? type)
    {
        if (type is null)
        {
            return "null";
        }

        string typeName = $"{type.Name}";

        Type[] genericArguments = type.GetGenericArguments();
        if (genericArguments.Length is 0)
        {
            int indexOfGenericCountSymbol = typeName.IndexOf('`');
            if (indexOfGenericCountSymbol is >= 0)
            {
                //remove the generic count symbol
                typeName = typeName[..indexOfGenericCountSymbol];
            }

            typeName += "<";

            foreach (Type genericArgument in genericArguments)
            {
                typeName += $"{GetFriendlyName(genericArgument)}, ";
            }

            typeName = $"{typeName.TrimEnd(' ', ',')}>";
        }

        return typeName;
    }
}
