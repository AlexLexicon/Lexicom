namespace Lexicom.ConsoleApp.Amenities.Questions;
public class QuestionBuilder
{
    private readonly List<QuestionBuilderAnswer> _answers;

    public QuestionBuilder()
    {
        _answers = new List<QuestionBuilderAnswer>();
    }

    private Action<QuestionBuilderOptions>? OptionsDelegate { get; set; }
    private QuestionBuilderAnswer? DefaultAnswer { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public QuestionBuilder AddAnswer(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return AddAnswer(text, out _);
    }
    /// <exception cref="ArgumentNullException"/>
    public QuestionBuilder AddAnswer(string text, string input)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(input);

        return AddAnswer(text, input, out _);
    }
    /// <exception cref="ArgumentNullException"/>
    public QuestionBuilder AddAnswer(string text, out QuestionBuilderAnswer answer)
    {
        ArgumentNullException.ThrowIfNull(text);

        return AddAnswer(text, $"{_answers.Count + 1}", out answer);
    }
    /// <exception cref="ArgumentNullException"/>
    public QuestionBuilder AddAnswer(string text, string input, out QuestionBuilderAnswer answer)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(input);

        answer = new QuestionBuilderAnswer(text, input);

        return AddAnswer(answer);
    }
    public QuestionBuilder AddAnswer(QuestionBuilderAnswer answer)
    {
        _answers.Add(answer);

        return this;
    }

    /// <exception cref="ArgumentNullException"/>
    public QuestionBuilder SetAnswerDefault(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        DefaultAnswer = new QuestionBuilderAnswer(text, string.Empty);

        return this;
    }

    /// <exception cref="ArgumentNullException"/>
    public QuestionBuilder Configure(Action<QuestionBuilderOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        OptionsDelegate = configure;

        return this;
    }

    public QuestionBuilderAnswer Ask() => Ask(null);
    public QuestionBuilderAnswer Ask(string? question)
    {
        var options = new QuestionBuilderOptions();

        OptionsDelegate?.Invoke(options);

        if (!_answers.Any())
        {
            Console.WriteLine(question);
            Console.WriteLine("No answers possible");

            if (DefaultAnswer is not null)
            {
                return DefaultAnswer.Value;
            }

            return QuestionBuilderAnswer.None;
        }
        else
        {
            bool isInvalid = false;
            QuestionBuilderAnswer? selectedAnswer = null;
            while (selectedAnswer is null)
            {
                if (isInvalid)
                {
                    Console.WriteLine("The input selection was invalid, try again.");
                }

                Console.WriteLine(question);

                foreach (var answer in _answers)
                {
                    Console.WriteLine(answer.ToString());
                }

                if (DefaultAnswer is not null)
                {
                    Console.WriteLine($"Default(invalid): {DefaultAnswer.Value.Text}");
                }

                isInvalid = false;
                string? input = Console.ReadLine();

                foreach (var answer in _answers)
                {
                    if (string.Equals(input, answer.Input, options.InputStringComparison))
                    {
                        selectedAnswer = answer;
                        break;
                    }
                }

                if (selectedAnswer is null)
                {
                    if (DefaultAnswer is not null)
                    {
                        selectedAnswer = DefaultAnswer;
                    }
                    else
                    {
                        isInvalid = true;
                        Console.Clear();
                    }
                }
            }

            return selectedAnswer.Value;
        }
    }
}
