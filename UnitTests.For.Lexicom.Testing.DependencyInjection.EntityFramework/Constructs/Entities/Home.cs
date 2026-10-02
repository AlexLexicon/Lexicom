using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Entities;

public class Home : IEntityTypeConfiguration<Home>
{
    public required Guid Id { get; init; }
    public required string Address { get; set; }

    public required DateTimeOffset CreatedDateTimeUtc { get; init; }
    public required DateTimeOffset ModifiedDateTimeUtc { get; set; }

    public void Configure(EntityTypeBuilder<Home> builder)
    {
        builder
         .ToTable("Homes")
         .HasKey(h => h.Id);
    }
}
