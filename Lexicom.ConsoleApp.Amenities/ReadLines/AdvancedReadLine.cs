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

        IConsolexConsole console = Consolex.GetConsolexConsole();

        AdvancedReadLineIntercept[] keys = keysToInterrupt
            .Where(k => k is not null)
            .Select(k => k!)
            .ToArray();

        var builder = new StringBuilder();
        int index = 0;
        var initialResults = keys
            .Select(i => i.Initial())
            .Where(r => r.IsInitial);

        foreach (AdvancedReadLineInitialResult? initialResult in initialResults)
        {
            if (initialResult.Input is not null)
            {
                foreach (char character in initialResult.Input)
                {
                    Insert(console, ref index, character, builder);
                }
            }
        }

        ConsoleKeyInfo cki = console.ReadKey(true);
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
                        Insert(console, ref index, character, builder);
                    }
                }
            }

            if (cki.Key is ConsoleKey.LeftArrow)
            {
                if (index < 1)
                {
                    cki = console.ReadKey(true);
                    continue;
                }

                LeftArrow(console, ref index, cki);
            }
            else if (cki.Key is ConsoleKey.RightArrow)
            {
                if (index >= builder.Length)
                {
                    cki = console.ReadKey(true);
                    continue;
                }

                RightArrow(console, ref index, cki, builder);
            }
            else if (cki.Key is ConsoleKey.Backspace)
            {
                if (index < 1)
                {
                    cki = console.ReadKey(true);
                    continue;
                }

                BackSpace(console, ref index, builder);
            }
            else if (cki.Key is ConsoleKey.Delete)
            {
                if (index >= builder.Length)
                {
                    cki = console.ReadKey(true);
                    continue;
                }

                Delete(console, ref index, cki, builder);
            }
            else if (cki.Key is ConsoleKey.Tab)
            {
                cki = console.ReadKey(true);
                continue;
            }
            else
            {
                if (cki.KeyChar is '\0')
                {
                    cki = console.ReadKey(true);
                    continue;
                }

                Insert(console, ref index, cki.KeyChar, builder);
            }

            cki = console.ReadKey(true);
        }

        startPosition = GetStartPosition(console, index);
        var endPosition = GetEndPosition(console, startPosition.left, builder.Length);
        int left = 0;
        int top = startPosition.top + endPosition.top + 1;

        if (top >= console.BufferHeight)
        {
            console.WriteLine();
            top = console.BufferHeight - 1;
        }

        console.SetCursorPosition(left, top);

        return builder.ToString();
    }

    private static void LeftArrow(IConsolexConsole console, ref int index, ConsoleKeyInfo cki)
    {
        int previousIndex = index;
        index--;

        if (cki.Modifiers is ConsoleModifiers.Control)
        {
            index = 0;

            var (left, top) = GetStartPosition(console, previousIndex);
            console.SetCursorPosition(left, top);

            return;
        }

        if (console.CursorLeft > 0)
        {
            console.CursorLeft--;
        }
        else
        {
            console.CursorTop--;
            console.CursorLeft = console.BufferWidth - 1;
        }
    }

    private static void RightArrow(IConsolexConsole console, ref int index, ConsoleKeyInfo cki, StringBuilder builder)
    {
        int previousIndex = index;
        index++;

        if (cki.Modifiers is ConsoleModifiers.Control)
        {
            index = builder.Length;

            var startPosition = GetStartPosition(console, previousIndex);
            var endPosition = GetEndPosition(console, startPosition.left, builder.Length);
            int top = startPosition.top + endPosition.top;
            int left = endPosition.left;

            console.SetCursorPosition(left, top);

            return;
        }

        if (console.CursorLeft < console.BufferWidth - 1)
        {
            console.CursorLeft++;
        }
        else
        {
            console.CursorTop++;
            console.CursorLeft = 0;
        }
    }

    private static void Insert(IConsolexConsole console, ref int index, char keyChar, StringBuilder builder)
    {
        int previousIndex = index;
        index++;

        builder.Insert(previousIndex, keyChar);

        var startPosition = GetStartPosition(console, previousIndex);
        console.SetCursorPosition(startPosition.left, startPosition.top);
        console.Write(builder.ToString());

        GoBackToCurrentPosition(console, index, startPosition);
    }

    private static void BackSpace(IConsolexConsole console, ref int index, StringBuilder builder)
    {
        int previousIndex = index;
        index--;

        var startPosition = GetStartPosition(console, previousIndex);
        ErasePrint(console, builder, startPosition);

        builder.Remove(index, 1);
        console.Write(builder.ToString());

        GoBackToCurrentPosition(console, index, startPosition);
    }

    private static void Delete(IConsolexConsole console, ref int index, ConsoleKeyInfo cki, StringBuilder builder)
    {
        var startPosition = GetStartPosition(console, index);
        ErasePrint(console, builder, startPosition);

        if (cki.Modifiers is ConsoleModifiers.Control)
        {
            builder.Remove(index, builder.Length - index);
            console.Write(builder.ToString());

            GoBackToCurrentPosition(console, index, startPosition);
            return;
        }

        builder.Remove(index, 1);
        console.Write(builder.ToString());

        GoBackToCurrentPosition(console, index, startPosition);
    }

    private static (int left, int top) GetStartPosition(IConsolexConsole console, int previousIndex)
    {
        int top;
        int left;

        if (previousIndex <= console.CursorLeft)
        {
            top = console.CursorTop;
            left = console.CursorLeft - previousIndex;
        }
        else
        {
            int decrementValue = previousIndex - console.CursorLeft;
            int rowsFromStart = decrementValue / console.BufferWidth;
            top = console.CursorTop - rowsFromStart;
            left = decrementValue - rowsFromStart * console.BufferWidth;

            if (left is not 0)
            {
                top--;
                left = console.BufferWidth - left;
            }
        }

        return (left, top);
    }

    private static void GoBackToCurrentPosition(IConsolexConsole console, int index, (int left, int top) startPosition)
    {
        int rowsToGo = (index + startPosition.left) / console.BufferWidth;
        int rowIndex = index - rowsToGo * console.BufferWidth;

        int left = startPosition.left + rowIndex;
        int top = startPosition.top + rowsToGo;

        console.SetCursorPosition(left, top);
    }

    private static (int left, int top) GetEndPosition(IConsolexConsole console, int startColumn, int builderLength)
    {
        int cursorTop = (builderLength + startColumn) / console.BufferWidth;
        int cursorLeft = startColumn + (builderLength - cursorTop * console.BufferWidth);

        return (cursorLeft, cursorTop);
    }

    private static void ErasePrint(IConsolexConsole console, StringBuilder builder, (int left, int top) startPosition)
    {
        console.SetCursorPosition(startPosition.left, startPosition.top);
        console.Write(new string(Enumerable.Range(0, builder.Length).Select(o => ' ').ToArray()));
        console.SetCursorPosition(startPosition.left, startPosition.top);
    }
}
