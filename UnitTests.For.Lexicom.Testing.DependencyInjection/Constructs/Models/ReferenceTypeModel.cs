namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

public class ReferenceTypeModel
{
    public ReferenceTypeModel()
    {
        Id = Guid.NewGuid();
    }
    public ReferenceTypeModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
    public string? Value { get; set; }
}
