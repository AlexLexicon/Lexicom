namespace Lexicom.ConsoleApp.Amenities;

public interface IConsolexConsole
{
    ConsoleColor ForegroundColor { get; set; }
    int BufferWidth { get; }
    int BufferHeight { get; }
    int CursorLeft { get; set; }
    int CursorTop { get; set; }
    void ResetColor();
    void Clear();
    void Write(string? value);
    void WriteLine();
    void WriteLine(bool value);
    void WriteLine(char value);
    void WriteLine(char[]? buffer);
    void WriteLine(decimal value);
    void WriteLine(double value);
    void WriteLine(float value);
    void WriteLine(int value);
    void WriteLine(long value);
    void WriteLine(object? value);
    void WriteLine(string? value);
    void WriteLine(uint value);
    void WriteLine(ulong value);
    void WriteLine(ReadOnlySpan<char> value);
    void WriteLine(char[] buffer, int index, int count);
    string? ReadLine();
    ConsoleKeyInfo ReadKey(bool intercept);
    void SetCursorPosition(int left, int top);
}
public class ConsolexConsole : IConsolexConsole
{
    public ConsoleColor ForegroundColor
    {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }

    public int BufferWidth => Console.BufferWidth;

    public int BufferHeight => Console.BufferHeight;

    public int CursorLeft
    {
        get => Console.CursorLeft;
        set => Console.CursorLeft = value;
    }

    public int CursorTop
    {
        get => Console.CursorTop;
        set => Console.CursorTop = value;
    }

    public void ResetColor()
    {
        Console.ResetColor();
    }

    public void Clear()
    {
        Console.Clear();
    }

    public void Write(string? value)
    {
        Console.Write(value);
    }

    public void WriteLine()
    {
        Console.WriteLine();
    }
    public void WriteLine(string? value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(bool value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(char value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(char[]? buffer)
    {
        Console.WriteLine(buffer);
    }
    public void WriteLine(decimal value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(double value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(float value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(int value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(long value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(object? value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(uint value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(ulong value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(ReadOnlySpan<char> value)
    {
        Console.WriteLine(value);
    }
    public void WriteLine(char[] buffer, int index, int count)
    {
        Console.WriteLine(buffer, index, count);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
    public ConsoleKeyInfo ReadKey(bool intercept)
    {
        return Console.ReadKey(intercept);
    }

    public void SetCursorPosition(int left, int top)
    {
        Console.SetCursorPosition(left, top);
    }
}
