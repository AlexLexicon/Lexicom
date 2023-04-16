namespace Lexicom.ConsoleApp.Amenities.Questions;
public class QuestionBinaryBuilder
{
    private Action<QuestionBuilderOptions>? ConfigureDelegate { get; set; }
    private QuestionBuilderAnswer? TrueAnswer { get; set; }
    private QuestionBuilderAnswer? FalseAnswer { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public QuestionBinaryBuilder SetTrue(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return SetTrue(text, "1");
    }
    /// <exception cref="ArgumentNullException"/>
    public QuestionBinaryBuilder SetTrue(string text, string input)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(input);

        return SetTrue(new QuestionBuilderAnswer(text, input));
    }
    public QuestionBinaryBuilder SetTrue(QuestionBuilderAnswer trueAnswer)
    {
        TrueAnswer = trueAnswer;

        return this;
    }

    /// <exception cref="ArgumentNullException"/>
    public QuestionBinaryBuilder SetFalse(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return SetFalse(text, "2");
    }
    /// <exception cref="ArgumentNullException"/>
    public QuestionBinaryBuilder SetFalse(string text, string input)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(input);

        return SetFalse(new QuestionBuilderAnswer(text, input));
    }
    public QuestionBinaryBuilder SetFalse(QuestionBuilderAnswer falseAnswer)
    {
        FalseAnswer = falseAnswer;

        return this;
    }

    /// <exception cref="ArgumentNullException"/>
    public QuestionBinaryBuilder Configure(Action<QuestionBuilderOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        ConfigureDelegate = configure;

        return this;
    }

    public bool Ask() => Ask(null);
    public bool Ask(string? question)
    {
        var builder = new QuestionBuilder();

        if (ConfigureDelegate is not null)
        {
            builder.Configure(ConfigureDelegate);
        }

        QuestionBuilderAnswer trueAnswer = TrueAnswer is not null ? TrueAnswer.Value : new QuestionBuilderAnswer("yes", "1");
        builder.AddAnswer(trueAnswer);

        if (FalseAnswer is not null)
        {
            builder.AddAnswer(FalseAnswer.Value);
        }
        else
        {
            builder.SetAnswerDefault("no");
        }

        var answer = builder.Ask(question);

        return answer == trueAnswer;
    }
}
