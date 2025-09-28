using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.EfCore.Dominando.Data.ModelConfigurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder
            .ToTable("Pessoas")
            .HasDiscriminator<int>("TipoPessoa")
            .HasValue<Pessoa>(3)
            .HasValue<Instrutor>(6)
            .HasValue<Pessoa>(99);
    }
}