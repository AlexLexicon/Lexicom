using Lexicom.ConsoleApp.Amenities.Questions;
using Lexicom.ConsoleApp.Amenities.ReadLines;
using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;
using Lexicom.ConsoleApp.Amenities.ReadLines.Settings;
using Newtonsoft.Json;
using System.Globalization;

namespace Lexicom.ConsoleApp.Amenities;

public static class Consolex
{
    internal static IConsolexConsole GetConsolexConsole()
    {
        if (Consolex.ConsolexConsole is null)
        {
            throw new NullReferenceException($"{nameof(Consolex)}.{nameof(ConsolexConsole)} is null.");
        }

        return Consolex.ConsolexConsole;
    }

    public delegate bool TryParseDelegate<T>(string? input, out T result);
    public delegate bool TryParseWithSettingsDelegate<T, TSettings>(string? input, TSettings settings, out T result) where TSettings : ReadLineSettings;

    /// <exception cref="ArgumentNullException"/>
    public static ReadLineSettings DefaultReadLineSettings
    {
        get;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            field = value;
        }
    } = new ReadLineSettings(
        cancelKey: ConsoleKey.Escape,
        defaultKey: ConsoleKey.F1,
        defaultInput: null,
        initialInput: null,
        inputColor: ConsoleColor.Green
    );

    public static IConsolexConsole? ConsolexConsole { get; set; } = new ConsolexConsole();

    /// <exception cref="ArgumentNullException"/>
    public static JsonSerializerSettings JsonSerializerSettings
    {
        get;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            field = value;
        }
    } = new JsonSerializerSettings();

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

            if (genericArguments.Length is not 0)
            {
                int index = name.IndexOf('`');
                if (index is >= 0)
                {
                    name = name[..index];
                }

                string args = string.Empty;

                foreach (Type arg in genericArguments)
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
            ConsolexConsole?.WriteLine($"\"{name}\":");
        }
        ConsolexConsole?.Write(json);
        ConsolexConsole?.WriteLine();
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
    public static string ReadLine(string? description, string? initialInput)
    {
        ReadLineSettings settings = CopyDefaultReadLineSettings();

        settings.InitialInput = initialInput;

        return ReadLine(description, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static string ReadLine(string? description, ReadLineSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        bool isDefaultable = settings.DefaultKey is not null && settings.DefaultInput is not null;
        bool isCancellable = settings.CancelKey is not null;
        bool isInitialable = settings.InitialInput is not null;

        bool isInvalid = false;
        string? input = null;

        while (string.IsNullOrWhiteSpace(input))
        {
            if (isInvalid)
            {
                ConsolexConsole?.WriteLine("The input is required");
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

                ConsolexConsole?.WriteLine($"{descriptionPart}{keysPart}");
            }

            if (settings.InputColor.HasValue)
            {
                ConsolexConsole?.ForegroundColor = settings.InputColor.Value;
            }

            bool isCancelled = false;
            if (!isCancellable && !isDefaultable && !isInitialable)
            {
                input = ConsolexConsole?.ReadLine();
            }
            else
            {
                AdvancedReadLineInterrupt? readLineCancel = null;
                AdvancedReadLineDefault? readLineDefault = null;
                AdvancedReadLineInitial? readLineInitial = null;

                if (isCancellable)
                {
                    readLineCancel = new AdvancedReadLineInterrupt(settings.CancelKey);
                }

                if (isDefaultable)
                {
                    readLineDefault = new AdvancedReadLineDefault(settings.DefaultKey, settings.DefaultInput);
                }

                if (isInitialable)
                {
                    readLineInitial = new AdvancedReadLineInitial(settings.InitialInput);
                }

                input = ReadLine(readLineCancel, readLineDefault, readLineInitial);

                if (isCancellable && readLineCancel is not null)
                {
                    isCancelled = readLineCancel.IsInterrupted;
                }
            }

            if (settings.InputColor.HasValue)
            {
                ConsolexConsole?.ResetColor();
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
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, T initialInput)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);

        return ReadLineParse(tryParseDelegate, description: null, initialInput);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T, TSettings>(TryParseWithSettingsDelegate<T, TSettings> tryParseWithSettingsDelegate, T initialInput, TSettings settings) where TSettings : ReadLineSettings
    {
        ArgumentNullException.ThrowIfNull(tryParseWithSettingsDelegate);

        return ReadLineParse(tryParseWithSettingsDelegate, description: null, initialInput, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, ReadLineSettings settings)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);
        ArgumentNullException.ThrowIfNull(settings);

        return ReadLineParse(tryParseDelegate, description: null, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T, TSettings>(TryParseWithSettingsDelegate<T, TSettings> tryParseWithSettingsDelegate, TSettings settings) where TSettings : ReadLineSettings
    {
        ArgumentNullException.ThrowIfNull(tryParseWithSettingsDelegate);
        ArgumentNullException.ThrowIfNull(settings);

        return ReadLineParse(tryParseWithSettingsDelegate, description: null, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, string? description, T initialInput)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);

        ReadLineSettings settings = CopyDefaultReadLineSettings();

        settings.InitialInput = initialInput?.ToString();

        return ReadLineParse(tryParseDelegate, description, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T, TSettings>(TryParseWithSettingsDelegate<T, TSettings> tryParseWithSettingsDelegate, string? description, T initialInput, TSettings settings) where TSettings : ReadLineSettings
    {
        ArgumentNullException.ThrowIfNull(tryParseWithSettingsDelegate);

        settings.InitialInput = initialInput?.ToString();

        return ReadLineParse(tryParseWithSettingsDelegate, description, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T>(TryParseDelegate<T> tryParseDelegate, string? description, ReadLineSettings settings)
    {
        ArgumentNullException.ThrowIfNull(tryParseDelegate);
        ArgumentNullException.ThrowIfNull(settings);

        return ReadLineParse((string? input, ReadLineSettings _, out T result) =>
        {
            return tryParseDelegate.Invoke(input, out result);
        }, description, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static T ReadLineParse<T, TSettings>(TryParseWithSettingsDelegate<T, TSettings> tryParseWithSettingsDelegate, string? description, TSettings settings) where TSettings : ReadLineSettings
    {
        ArgumentNullException.ThrowIfNull(tryParseWithSettingsDelegate);
        ArgumentNullException.ThrowIfNull(settings);

        bool isInvalid = false;

        while (true)
        {
            if (isInvalid)
            {
                ConsolexConsole?.WriteLine($"The input is required to be an {typeof(T).Name}");
            }

            string strInput = ReadLine(description, settings);

            if (tryParseWithSettingsDelegate.Invoke(strInput, settings, out T result))
            {
                return result;
            }

            isInvalid = true;
        }
    }

    public static bool ReadLineBoolean() => ReadLineParse<bool>(bool.TryParse);
    public static bool ReadLineBoolean(string? description) => ReadLineParse<bool>(bool.TryParse, description);
    public static bool ReadLineBoolean(bool initialInput) => ReadLineParse(bool.TryParse, initialInput);
    public static bool ReadLineBoolean(ReadLineSettings settings) => ReadLineParse<bool>(bool.TryParse, settings);
    public static bool ReadLineBoolean(string? description, bool initialInput) => ReadLineParse(bool.TryParse, description, initialInput);
    public static bool ReadLineBoolean(string? description, ReadLineSettings settings) => ReadLineParse<bool>(bool.TryParse, description, settings);

    public static int ReadLineInteger() => ReadLineParse<int>(int.TryParse);
    public static int ReadLineInteger(string? description) => ReadLineParse<int>(int.TryParse, description);
    public static int ReadLineInteger(int initialInput) => ReadLineParse(int.TryParse, initialInput);
    public static int ReadLineInteger(ReadLineSettings settings) => ReadLineParse<int>(int.TryParse, settings);
    public static int ReadLineInteger(string? description, int initialInput) => ReadLineParse(int.TryParse, description, initialInput);
    public static int ReadLineInteger(string? description, ReadLineSettings settings) => ReadLineParse<int>(int.TryParse, description, settings);

    public static double ReadLineDouble() => ReadLineParse<double>(double.TryParse);
    public static double ReadLineDouble(string? description) => ReadLineParse<double>(double.TryParse, description);
    public static double ReadLineDouble(double initialInput) => ReadLineParse(double.TryParse, initialInput);
    public static double ReadLineDouble(ReadLineSettings settings) => ReadLineParse<double>(double.TryParse, settings);
    public static double ReadLineDouble(string? description, double initialInput) => ReadLineParse(double.TryParse, description, initialInput);
    public static double ReadLineDouble(string? description, ReadLineSettings settings) => ReadLineParse<double>(double.TryParse, description, settings);

    public static Guid ReadLineGuid() => ReadLineParse<Guid>(Guid.TryParse);
    public static Guid ReadLineGuid(string? description) => ReadLineParse<Guid>(Guid.TryParse, description);
    public static Guid ReadLineGuid(Guid initialInput) => ReadLineParse(Guid.TryParse, initialInput);
    public static Guid ReadLineGuid(ReadLineSettings settings) => ReadLineParse<Guid>(Guid.TryParse, settings);
    public static Guid ReadLineGuid(string? description, Guid initialInput) => ReadLineParse(Guid.TryParse, description, initialInput);
    public static Guid ReadLineGuid(string? description, ReadLineSettings settings) => ReadLineParse<Guid>(Guid.TryParse, description, settings);

    public static DateTimeOffset ReadLineDateTimeOffset() => ReadLineParse<DateTimeOffset, DateTimeOffsetReadLineSettings>(DateTimeOffsetTryParse, new DateTimeOffsetReadLineSettings());
    public static DateTimeOffset ReadLineDateTimeOffset(string? description) => ReadLineParse<DateTimeOffset, DateTimeOffsetReadLineSettings>(DateTimeOffsetTryParse, description, new DateTimeOffsetReadLineSettings());
    public static DateTimeOffset ReadLineDateTimeOffset(DateTimeOffset initialInput) => ReadLineParse(DateTimeOffsetTryParse, initialInput, new DateTimeOffsetReadLineSettings());
    public static DateTimeOffset ReadLineDateTimeOffset(DateTimeOffsetReadLineSettings settings) => ReadLineParse<DateTimeOffset, DateTimeOffsetReadLineSettings>(DateTimeOffsetTryParse, settings);
    public static DateTimeOffset ReadLineDateTimeOffset(string? description, DateTimeOffset initialInput) => ReadLineParse(DateTimeOffsetTryParse, description, initialInput, new DateTimeOffsetReadLineSettings());
    public static DateTimeOffset ReadLineDateTimeOffset(string? description, DateTimeOffsetReadLineSettings settings) => ReadLineParse<DateTimeOffset, DateTimeOffsetReadLineSettings>(DateTimeOffsetTryParse, description, settings);
    private static bool DateTimeOffsetTryParse(string? input, DateTimeOffsetReadLineSettings settings, out DateTimeOffset result) => DateTimeOffset.TryParseExact(input, settings.Format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out result);

    public static void WriteLine() => ConsolexConsole?.WriteLine();
    public static void WriteLine(bool value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(char value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(char[]? buffer, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(buffer, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(decimal value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(double value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(float value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(int value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(long value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(object? value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(string? value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(uint value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(ulong value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(ReadOnlySpan<char> value, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(value, ConsolexConsole.WriteLine, color);
        }
    }
    public static void WriteLine(char[] buffer, int index, int count, ConsoleColor? color = null)
    {
        if (ConsolexConsole is not null)
        {
            WriteColoredLine(buffer, _ => ConsolexConsole.WriteLine(buffer, index, count), color);
        }
    }
    public static void WriteLine(params IEnumerable<WriteLineSegment> segments)
    {
        if (ConsolexConsole is not null)
        {
            foreach (WriteLineSegment segment in segments)
            {
                if (segment.Color.HasValue)
                {
                    ConsolexConsole.ForegroundColor = segment.Color.Value;
                }

                ConsolexConsole.Write(segment.Text);

                if (segment.Color.HasValue)
                {
                    ConsolexConsole.ResetColor();
                }
            }

            ConsolexConsole.WriteLine();
        }
    }
    private static void WriteColoredLine<T>(T value, Action<T> writeLineDelegate, ConsoleColor? color) where T : allows ref struct
    {
        if (ConsolexConsole is not null)
        {
            if (color.HasValue)
            {
                ConsolexConsole.ForegroundColor = color.Value;
            }

            writeLineDelegate.Invoke(value);

            if (color.HasValue)
            {
                ConsolexConsole.ResetColor();
            }
        }
    }

    internal static ReadLineSettings CopyDefaultReadLineSettings()
    {
        return new ReadLineSettings(
            cancelKey: DefaultReadLineSettings.CancelKey,
            defaultKey: DefaultReadLineSettings.DefaultKey,
            defaultInput: DefaultReadLineSettings.DefaultInput,
            initialInput: DefaultReadLineSettings.InitialInput,
            inputColor: DefaultReadLineSettings.InputColor
        );
    }

    private static string? ReadLine(params AdvancedReadLineIntercept?[] readLineIntercepts) => AdvancedReadLine.WithInterception(readLineIntercepts);
}
