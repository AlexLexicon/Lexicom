namespace IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

public struct ValueTypeModel
{
    public required Guid Id { get; init; }
    public string? Value { get; set; }
}
