using Lexicom.ConsoleApp.Amenities.Questions;
using Lexicom.ConsoleApp.Amenities.ReadLines;
using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;
using Newtonsoft.Json;

namespace Lexicom.ConsoleApp.Amenities;
public class Consolex
{
    public delegate bool TryParseDelegate<T>(string? input, out T result);

    public static ReadLineSettings DefaultReadLineSettings { get; } = new ReadLineSettings
    {
        CancelKey = ConsoleKey.Escape,
        DefaultKey = ConsoleKey.F1,
        DefaultInput = null,
    };

    private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings();
    /// <exception cref="ArgumentNullException"/>
    public static JsonSerializerSettings JsonSerializerSettings
    {
        get => _jsonSerializerSettings;
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _jsonSerializerSettings = value;
        }
    }

    public static void WriteAsJsonWithType(object? obj) => WriteAsJsonWithType(obj, JsonSerializerSettings);
    /// <exception cref="ArgumentNullException"/>
    public static void WriteAsJsonWithType(object? obj, JsonSerializerSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        string? name = null;

        if (obj is not null)
        {
            Type objType = obj.GetType();

            name = objType.Name;

            var genericArguments = objType.GetGenericArguments();

            if (genericArguments.Any())
            {
                int index = name.IndexOf('`');
                if (index >= 0)
                {
                    name = name[..index];
                }

                string args = string.Empty;

                foreach (var arg in genericArguments)
                {
                    string? tStr = arg?.Name;

                    if (tStr is not null)
                    {
                        if (args != string.Empty)
                        {
                            args += ",";
                        }

                        args += tStr;
                    }
                }

                if (args != string.Empty)
                {
                    args = args.TrimEnd(',');

                    name += $"<{args}>";
                }
            }
        }

        WriteAsJson(obj, name, settings);
    }
    public static void WriteAsJson(object? obj) => WriteAsJson(obj, null);
    public static void WriteAsJson(object? obj, string? name) => WriteAsJson(obj, name, JsonSerializerSettings);
    /// <exception cref="ArgumentNullException"/>
    public static void WriteAsJson(object? obj, string? name, JsonSerializerSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        string json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);

        if (name is not null)
        {
            Console.WriteLine($"\"{name}\":");
        }
        Console.Write(json);
        Console.WriteLine();
    }

    public static QuestionBuilder Question() => new QuestionBuilder();
    public static QuestionBinaryBuilder BinaryQuestion() => new QuestionBinaryBuilder();

    public static string ReadLine() => ReadLine(description: null);
    public static string ReadLine(string? description) => ReadLine(description, DefaultReadLineSettings);
    /// <exception cref="ArgumentNullException"/>
    public static string ReadLine(ReadLineSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return ReadLine(null, settings);
    }
    public static string ReadLine(string? description, string? defaultInput)
    {
        ReadLineSettings settings = CopyDefaultReadLineSettings();

        settings.DefaultInput = defaultInput;

        return ReadLine(description, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static string ReadLine(string? description, ReadLineSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        bool isDefaultable = settings.DefaultKey is not null && settings.DefaultInput is not null;
        bool isCancellable = settings.CancelKey is not null;

        bool isInvalid = false;
        string? input = null;

        while (string.IsNullOrWhiteSpace(input))
        {
            if (isInvalid)
            {
                Console.WriteLine("The input is required");
            }

            if (description is not null)
            {
                string descriptionPart = description is not null ? $"{description} " : string.Empty;
                string? keysPart = null;

                if (isDefaultable)
                {
                    keysPart += $"(default[{settings.DefaultKey}]";
                }
                if (isCancellable)
                {
                    if (keysPart is null)
                    {
                        keysPart += "(";
                    }
                    else
                    {
                        keysPart += " ";
                    }

                    keysPart += $"cancel[{settings.CancelKey}]";
                }

                if (keysPart is not null)
                {
                    keysPart += ")";
                }

                Console.WriteLine($"{descriptionPart}{keysPart}");
            }

            bool isCancelled = false;
            if (!isCancellable && !isDefaultable)
            {
                input = Console.ReadLine();
            }
            else if (isCancellable && !isDefaultable)
            {
                input = ReadLine(settings.CancelKey, out isCancelled);
            }
            else if (isDefaultable)
            {
                var readLineDefault = new AdvancedReadLineDefault(settings.DefaultKey, settings.DefaultInput);

                if (!isCancellable)
                {
                    input = ReadLine(readLineDefault);
                }
                else
                {
                    var readLineCancel = new AdvancedReadLineInterrupt(settings.CancelKey);

                    input = ReadLine(readLineDefault, readLineCancel);

                    isCancelled = readLineCancel.IsInterrupted;
                }
            }

            if (isCancelled)
            {
                throw new OperationCanceledException($"{nameof(Consolex)}.{nameof(ReadLine)} was cancelled.");
            }

            isInvalid = string.IsNullOrWhiteSpace(input);
        }

        return input;
    }

    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);

        return ReadLineParse(tryParseDelegate, description: null);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, string? description)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);

        return ReadLineParse(tryParseDelegate, description, DefaultReadLineSettings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, T defaultInput)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);

        return ReadLineParse(tryParseDelegate, null, defaultInput);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, ReadLineSettings settings)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);
        ArgumentNullException.ThrowIfNull(settings);

        return ReadLineParse(tryParseDelegate, null, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, string? description, T defaultInput)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);

        ReadLineSettings settings = CopyDefaultReadLineSettings();

        settings.DefaultInput = defaultInput?.ToString();

        return ReadLineParse(tryParseDelegate, description, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, string? description, ReadLineSettings settings)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);
        ArgumentNullException.ThrowIfNull(settings);

        bool isInvalid = false;

        while (true)
        {
            if (isInvalid)
            {
                Console.WriteLine($"The input is required to be an {typeof(T).Name}");
            }

            string strInput = ReadLine(description, settings);

            if (tryParseDelegate.Invoke(strInput, out T result))
            {
                return result;
            }

            isInvalid = true;
        }
    }

    public static bool ReadLineBoolean() => ReadLineParse<bool>(bool.TryParse);
    public static bool ReadLineBoolean(string? description) => ReadLineParse<bool>(bool.TryParse, description);
    public static bool ReadLineBoolean(bool defaultInput) => ReadLineParse(bool.TryParse, defaultInput);
    public static bool ReadLineBoolean(ReadLineSettings settings) => ReadLineParse<bool>(bool.TryParse, settings);
    public static bool ReadLineBoolean(string? description, bool defaultInput) => ReadLineParse(bool.TryParse, description, defaultInput);
    public static bool ReadLineBoolean(string? description, ReadLineSettings settings) => ReadLineParse<bool>(bool.TryParse, description, settings);

    public static int ReadLineInteger() => ReadLineParse<int>(int.TryParse);
    public static int ReadLineInteger(string? description) => ReadLineParse<int>(int.TryParse, description);
    public static int ReadLineInteger(int defaultInput) => ReadLineParse(int.TryParse, defaultInput);
    public static int ReadLineInteger(ReadLineSettings settings) => ReadLineParse<int>(int.TryParse, settings);
    public static int ReadLineInteger(string? description, int defaultInput) => ReadLineParse(int.TryParse, description, defaultInput);
    public static int ReadLineInteger(string? description, ReadLineSettings settings) => ReadLineParse<int>(int.TryParse, description, settings);

    public static double ReadLineDouble() => ReadLineParse<double>(double.TryParse);
    public static double ReadLineDouble(string? description) => ReadLineParse<double>(double.TryParse, description);
    public static double ReadLineDouble(double defaultInput) => ReadLineParse(double.TryParse, defaultInput);
    public static double ReadLineDouble(ReadLineSettings settings) => ReadLineParse<double>(double.TryParse, settings);
    public static double ReadLineDouble(string? description, double defaultInput) => ReadLineParse(double.TryParse, description, defaultInput);
    public static double ReadLineDouble(string? description, ReadLineSettings settings) => ReadLineParse<double>(double.TryParse, description, settings);

    public static Guid ReadLineGuid() => ReadLineParse<Guid>(Guid.TryParse);
    public static Guid ReadLineGuid(string? description) => ReadLineParse<Guid>(Guid.TryParse, description);
    public static Guid ReadLineGuid(Guid defaultInput) => ReadLineParse(Guid.TryParse, defaultInput);
    public static Guid ReadLineGuid(ReadLineSettings settings) => ReadLineParse<Guid>(Guid.TryParse, settings);
    public static Guid ReadLineGuid(string? description, Guid defaultInput) => ReadLineParse(Guid.TryParse, description, defaultInput);
    public static Guid ReadLineGuid(string? description, ReadLineSettings settings) => ReadLineParse<Guid>(Guid.TryParse, description, settings);

    public static string? ReadLine(ConsoleKey? keyToInterrupt, out bool isInterrupted)
    {
        if (keyToInterrupt is null)
        {
            isInterrupted = false;
            return Console.ReadLine();
        }

        var readLineInterrupt = new AdvancedReadLineInterrupt(keyToInterrupt.Value);

        string? input = ReadLine(readLineInterrupt);

        isInterrupted = readLineInterrupt.IsInterrupted;

        return input;
    }

    private static string? ReadLine(params AdvancedReadLineIntercept[] readLineIntercepts) => AdvancedReadLine.WithInterception(readLineIntercepts);

    private static ReadLineSettings CopyDefaultReadLineSettings()
    {
        return new ReadLineSettings
        {
            CancelKey = DefaultReadLineSettings.CancelKey,
            DefaultKey = DefaultReadLineSettings.DefaultKey,
            DefaultInput = DefaultReadLineSettings.DefaultInput,
        };
    }
}
