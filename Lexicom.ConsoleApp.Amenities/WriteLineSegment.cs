namespace Lexicom.ConsoleApp.Amenities;

public readonly struct WriteLineSegment
{
    public static implicit operator WriteLineSegment(bool value) => new(value.ToString(), null);
    public static implicit operator WriteLineSegment(char value) => new(value.ToString(), null);
    public static implicit operator WriteLineSegment(char[]? buffer) => new(buffer?.ToString(), null);
    public static implicit operator WriteLineSegment(double value) => new(value.ToString(), null);
    public static implicit operator WriteLineSegment(string? value) => new(value?.ToString(), null);
    public static implicit operator WriteLineSegment(ulong value) => new(value.ToString(), null);

    public static implicit operator WriteLineSegment((bool value, ConsoleColor color) segment) => new(segment.value.ToString(), segment.color);
    public static implicit operator WriteLineSegment((char value, ConsoleColor color) segment) => new(segment.value.ToString(), segment.color);
    public static implicit operator WriteLineSegment((char[]? buffer, ConsoleColor color) segment) => new(segment.buffer?.ToString(), segment.color);
    public static implicit operator WriteLineSegment((double value, ConsoleColor color) segment) => new(segment.value.ToString(), segment.color);
    public static implicit operator WriteLineSegment((object? value, ConsoleColor color) segment) => new(segment.value?.ToString(), segment.color);
    public static implicit operator WriteLineSegment((string? value, ConsoleColor color) segment) => new(segment.value?.ToString(), segment.color);
    public static implicit operator WriteLineSegment((ulong value, ConsoleColor color) segment) => new(segment.value.ToString(), segment.color);

    private WriteLineSegment(string? text, ConsoleColor? color)
    {
        Text = text;
        Color = color;
    }

    public string? Text { get; }
    public ConsoleColor? Color { get; }
}
