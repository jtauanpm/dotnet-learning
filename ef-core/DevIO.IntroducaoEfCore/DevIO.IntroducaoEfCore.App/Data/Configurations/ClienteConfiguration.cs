using DevIO.IntroducaoEfCore.App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.IntroducaoEfCore.App.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(c => c.Telefone).HasColumnType("CHAR(11)");
        builder.Property(c => c.CEP).HasColumnType("CHAR(11)");
        builder.Property(c => c.Estado).HasColumnType("CHAR(2)");
        builder.Property(c => c.Cidade).HasMaxLength(60).IsRequired();
            
        builder.HasIndex(c => c.Telefone).HasDatabaseName("idx_cliente_telefone");
    }
}