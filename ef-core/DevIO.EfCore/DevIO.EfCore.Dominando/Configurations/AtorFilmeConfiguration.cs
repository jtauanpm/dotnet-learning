using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.EfCore.Dominando.Configurations;

public class AtorFilmeConfiguration : IEntityTypeConfiguration<Ator>
{
    public void Configure(EntityTypeBuilder<Ator> builder)
    {
        builder.HasMany(a => a.Filmes)
            .WithMany(f => f.Atores)
            .UsingEntity<Dictionary<string, object>>(
                "AtoresFilmes", 
                typeBuilder => typeBuilder.HasOne<Filme>().WithMany().HasForeignKey("FilmeId"),
                typeBuilder => typeBuilder.HasOne<Ator>().WithMany().HasForeignKey("AtorId"),
                typeBuilder =>
                {
                    typeBuilder.Property<DateTime>("CadastradoEm").HasDefaultValueSql("GETDATE()");
                });
    }
}