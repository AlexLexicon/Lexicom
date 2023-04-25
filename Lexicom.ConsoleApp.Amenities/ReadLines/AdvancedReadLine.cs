using Lexicom.ConsoleApp.Amenities.ReadLines.Abstractions;
using System.Text;

namespace Lexicom.ConsoleApp.Amenities.ReadLines;
internal static class AdvancedReadLine
{
    //from https://stackoverflow.com/questions/31996519/listen-on-esc-while-reading-console-line
    /// <exception cref="ArgumentNullException"/>
    public static string? WithInterception(params AdvancedReadLineIntercept?[] keysToInterrupt)
    {
        ArgumentNullException.ThrowIfNull(keysToInterrupt);

        AdvancedReadLineIntercept[] keys = keysToInterrupt
            .Where(k => k is not null)
            .Select(k => k!)
            .ToArray();

        var builder = new StringBuilder();
        int index = 0;
        var initalResults = keys
            .Select(i => i.Initial())
            .Where(r => r.IsInital);

        foreach (AdvancedReadLineInitalResult? initalResult in initalResults)
        {
            if (initalResult.Input is not null)
            {
                foreach (char character in initalResult.Input)
                {
                    Insert(ref index, character, builder);
                }
            }
        }

        ConsoleKeyInfo cki = Console.ReadKey(true);
        (int left, int top) startPosition;

        while (cki.Key is not ConsoleKey.Enter)
        {
            var intercepter = keys.FirstOrDefault(i => i.InterceptKey == cki.Key);
            if (intercepter is not null)
            {
                string? currentInput = builder.ToString();
                var result = intercepter.Intercept(currentInput);

                if (!result.IsContinue)
                {
                    break;
                }

                if (result.Input is not null)
                {
                    foreach (char character in result.Input)
                    {
                        Insert(ref index, character, builder);
                    }
                }
            }

            if (cki.Key is ConsoleKey.LeftArrow)
            {
                if (index < 1)
                {
                    cki = Console.ReadKey(true);
                    continue;
                }

                LeftArrow(ref index, cki);
            }
            else if (cki.Key is ConsoleKey.RightArrow)
            {
                if (index >= builder.Length)
                {
                    cki = Console.ReadKey(true);
                    continue;
                }

                RightArrow(ref index, cki, builder);
            }
            else if (cki.Key is ConsoleKey.Backspace)
            {
                if (index < 1)
                {
                    cki = Console.ReadKey(true);
                    continue;
                }

                BackSpace(ref index, builder);
            }
            else if (cki.Key is ConsoleKey.Delete)
            {
                if (index >= builder.Length)
                {
                    cki = Console.ReadKey(true);
                    continue;
                }

                Delete(ref index, cki, builder);
            }
            else if (cki.Key is ConsoleKey.Tab)
            {
                cki = Console.ReadKey(true);
                continue;
            }
            else
            {
                if (cki.KeyChar is '\0')
                {
                    cki = Console.ReadKey(true);
                    continue;
                }

                Insert(ref index, cki.KeyChar, builder);
            }

            cki = Console.ReadKey(true);
        }

        startPosition = GetStartPosition(index);
        var endPosition = GetEndPosition(startPosition.left, builder.Length);
        int left = 0;
        int top = startPosition.top + endPosition.top + 1;

        if (top >= Console.BufferHeight)
        {
            Console.WriteLine();
            top = Console.BufferHeight - 1;
        }

        Console.SetCursorPosition(left, top);

        return builder.ToString();
    }

    private static void LeftArrow(ref int index, ConsoleKeyInfo cki)
    {
        int previousIndex = index;
        index--;

        if (cki.Modifiers is ConsoleModifiers.Control)
        {
            index = 0;

            var (left, top) = GetStartPosition(previousIndex);
            Console.SetCursorPosition(left, top);

            return;
        }

        if (Console.CursorLeft > 0)
        {
            Console.CursorLeft--;
        }
        else
        {
            Console.CursorTop--;
            Console.CursorLeft = Console.BufferWidth - 1;
        }
    }

    private static void RightArrow(ref int index, ConsoleKeyInfo cki, StringBuilder builder)
    {
        int previousIndex = index;
        index++;

        if (cki.Modifiers is ConsoleModifiers.Control)
        {
            index = builder.Length;

            var startPosition = GetStartPosition(previousIndex);
            var endPosition = GetEndPosition(startPosition.left, builder.Length);
            int top = startPosition.top + endPosition.top;
            int left = endPosition.left;

            Console.SetCursorPosition(left, top);

            return;
        }

        if (Console.CursorLeft < Console.BufferWidth - 1)
        {
            Console.CursorLeft++;
        }
        else
        {
            Console.CursorTop++;
            Console.CursorLeft = 0;
        }
    }

    private static void Insert(ref int index, char keyChar, StringBuilder builder)
    {
        int previousIndex = index;
        index++;

        builder.Insert(previousIndex, keyChar);

        var startPosition = GetStartPosition(previousIndex);
        Console.SetCursorPosition(startPosition.left, startPosition.top);
        Console.Write(builder.ToString());

        GoBackToCurrentPosition(index, startPosition);
    }

    private static void BackSpace(ref int index, StringBuilder builder)
    {
        int previousIndex = index;
        index--;

        var startPosition = GetStartPosition(previousIndex);
        ErasePrint(builder, startPosition);

        builder.Remove(index, 1);
        Console.Write(builder.ToString());

        GoBackToCurrentPosition(index, startPosition);
    }

    private static void Delete(ref int index, ConsoleKeyInfo cki, StringBuilder builder)
    {
        var startPosition = GetStartPosition(index);
        ErasePrint(builder, startPosition);

        if (cki.Modifiers is ConsoleModifiers.Control)
        {
            builder.Remove(index, builder.Length - index);
            Console.Write(builder.ToString());

            GoBackToCurrentPosition(index, startPosition);
            return;
        }

        builder.Remove(index, 1);
        Console.Write(builder.ToString());

        GoBackToCurrentPosition(index, startPosition);
    }

    private static (int left, int top) GetStartPosition(int previousIndex)
    {
        int top;
        int left;

        if (previousIndex <= Console.CursorLeft)
        {
            top = Console.CursorTop;
            left = Console.CursorLeft - previousIndex;
        }
        else
        {
            int decrementValue = previousIndex - Console.CursorLeft;
            int rowsFromStart = decrementValue / Console.BufferWidth;
            top = Console.CursorTop - rowsFromStart;
            left = decrementValue - rowsFromStart * Console.BufferWidth;

            if (left is not 0)
            {
                top--;
                left = Console.BufferWidth - left;
            }
        }

        return (left, top);
    }

    private static void GoBackToCurrentPosition(int index, (int left, int top) startPosition)
    {
        int rowsToGo = (index + startPosition.left) / Console.BufferWidth;
        int rowIndex = index - rowsToGo * Console.BufferWidth;

        int left = startPosition.left + rowIndex;
        int top = startPosition.top + rowsToGo;

        Console.SetCursorPosition(left, top);
    }

    private static (int left, int top) GetEndPosition(int startColumn, int builderLength)
    {
        int cursorTop = (builderLength + startColumn) / Console.BufferWidth;
        int cursorLeft = startColumn + (builderLength - cursorTop * Console.BufferWidth);

        return (cursorLeft, cursorTop);
    }

    private static void ErasePrint(StringBuilder builder, (int left, int top) startPosition)
    {
        Console.SetCursorPosition(startPosition.left, startPosition.top);
        Console.Write(new string(Enumerable.Range(0, builder.Length).Select(o => ' ').ToArray()));
        Console.SetCursorPosition(startPosition.left, startPosition.top);
    }
}
