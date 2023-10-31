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
        if (genericArguments.Any())
        {
            int indexOfGenericCountSymbol = typeName.IndexOf('`');
            if (indexOfGenericCountSymbol >= 0)
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
