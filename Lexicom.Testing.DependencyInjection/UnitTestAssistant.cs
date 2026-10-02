namespace Lexicom.Testing.DependencyInjection;

public class UnitTestAssistant : TestAssistant
{
    public UnitTestAssistant() : this(new TestAssistantConfiguration())
    {
    }
    public UnitTestAssistant(TestAssistantConfiguration configuration) : base(configuration)
    {
    }

    public override TestingCategory Category => TestingCategory.UnitTest;
}
