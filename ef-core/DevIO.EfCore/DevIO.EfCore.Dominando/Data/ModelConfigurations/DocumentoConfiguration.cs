using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.EfCore.Dominando.Configurations;

public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder
            .Property(d => d.Cpf)
            .HasField("_cpf");

        builder
            .Property(d => d.Cpf)
            .HasColumnName("CPF");
    }
}