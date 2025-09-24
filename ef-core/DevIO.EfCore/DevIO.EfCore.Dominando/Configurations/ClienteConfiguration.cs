using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.EfCore.Dominando.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.OwnsOne(c => c.Endereco, builder =>
        {
            builder.Property(e => e.Bairro).HasColumnName("Bairro");
            builder.ToTable("Endereco"); //Separa o relacionamento em outra tabela
        });
    }
}