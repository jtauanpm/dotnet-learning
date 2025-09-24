using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.EfCore.Dominando.Configurations;

public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.HasOne(e => e.Governador)
            .WithOne(g => g.Estado)
            .HasForeignKey<Governador>(g => g.EstadoId);

        builder.Navigation(e => e.Governador).AutoInclude();
    }
}