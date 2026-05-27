using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Entities;

public class Person : IEntityTypeConfiguration<Person>
{
    public required Guid Id { get; init; }
    public required Guid HomeId { get; init; }
    public required string Name { get; set; }
    public required int Age { get; set; }

    public required DateTimeOffset CreatedDateTimeUtc { get; init; }
    public required DateTimeOffset ModifiedDateTimeUtc { get; set; }

    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder
            .ToTable("People")
            .HasKey(pe => pe.Id);
    }
}
