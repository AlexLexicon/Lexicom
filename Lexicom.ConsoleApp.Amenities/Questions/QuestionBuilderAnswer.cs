namespace Lexicom.ConsoleApp.Amenities.Questions;
public readonly struct QuestionBuilderAnswer
{
    public static QuestionBuilderAnswer None { get; } = new QuestionBuilderAnswer();

    public static bool operator ==(QuestionBuilderAnswer answer1, QuestionBuilderAnswer answer2) => answer1.Equals(answer2);
    public static bool operator !=(QuestionBuilderAnswer answer1, QuestionBuilderAnswer answer2) => !(answer1 == answer2);

    public QuestionBuilderAnswer()
    {
        Text = string.Empty;
        Input = string.Empty;
    }
    /// <exception cref="ArgumentNullException"/>
    public QuestionBuilderAnswer(string text, string input)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(input);

        Text = text;
        Input = input;
    }

    public string Text { get; }
    public string Input { get; }

    public override bool Equals(object? obj) => obj is QuestionBuilderAnswer answer && Equals(answer);
    public bool Equals(QuestionBuilderAnswer answer) => Text == answer.Text && Input == answer.Input;

    public override int GetHashCode() => (Text, Input).GetHashCode();

    public override string ToString()
    {
        if (Input == string.Empty)
        {
            return "None";
        }

        if (Text == string.Empty)
        {
            return $"[{Input}]: None";
        }

        return $"[{Input}]: {Text}";
    }
}
