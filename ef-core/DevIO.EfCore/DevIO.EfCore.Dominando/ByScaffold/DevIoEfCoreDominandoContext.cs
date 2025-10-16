using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class DevIoEfCoreDominandoContext : DbContext
{
    public DevIoEfCoreDominandoContext()
    {
    }

    public DevIoEfCoreDominandoContext(DbContextOptions<DevIoEfCoreDominandoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ator> Ators { get; set; }

    public virtual DbSet<AtoresFilme> AtoresFilmes { get; set; }

    public virtual DbSet<Cidade> Cidades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Configuraco> Configuracoes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Filme> Filmes { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Governador> Governadors { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<TableAtributo> TableAtributos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Dev_IO_EfCore_Dominando;User Id=sa;Password=Jordanna123.;Encrypt=False;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ator>(entity =>
        {
            entity.ToTable("Ator");
        });

        modelBuilder.Entity<AtoresFilme>(entity =>
        {
            entity.HasKey(e => new { e.AtorId, e.FilmeId });

            entity.HasIndex(e => e.FilmeId, "IX_AtoresFilmes_FilmeId");

            entity.Property(e => e.CadastradoEm).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Ator).WithMany(p => p.AtoresFilmes).HasForeignKey(d => d.AtorId);

            entity.HasOne(d => d.Filme).WithMany(p => p.AtoresFilmes).HasForeignKey(d => d.FilmeId);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.ToTable("Cidade");

            entity.HasIndex(e => e.EstadoId, "IX_Cidade_EstadoId");

            entity.HasOne(d => d.Estado).WithMany(p => p.Cidades).HasForeignKey(d => d.EstadoId);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");
        });

        modelBuilder.Entity<Configuraco>(entity =>
        {
            entity.Property(e => e.Chave)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.ToTable("Documento");

            entity.Property(e => e.Cpf).HasColumnName("CPF");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.ClienteId);

            entity.ToTable("Endereco");

            entity.Property(e => e.ClienteId).ValueGeneratedNever();

            entity.HasOne(d => d.Cliente).WithOne(p => p.Endereco).HasForeignKey<Endereco>(d => d.ClienteId);
        });

        modelBuilder.Entity<Filme>(entity =>
        {
            entity.ToTable("Filme");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasIndex(e => e.DepartamentoId, "IX_Funcionarios_DepartamentoId");

            entity.Property(e => e.Cpf).HasColumnName("CPF");
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.Rg).HasColumnName("RG");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Funcionarios).HasForeignKey(d => d.DepartamentoId);
        });

        modelBuilder.Entity<Governador>(entity =>
        {
            entity.ToTable("Governador");

            entity.HasIndex(e => e.EstadoId, "IX_Governador_EstadoId").IsUnique();

            entity.HasOne(d => d.Estado).WithOne(p => p.Governador).HasForeignKey<Governador>(d => d.EstadoId);
        });

        modelBuilder.Entity<TableAtributo>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("Meu comentário de tabela"));

            entity.HasIndex(e => new { e.MinhaDescricao, e.Id }, "IX_TableAtributos_MinhaDescricao_Id").IsUnique();

            entity.Property(e => e.MinhaDescricao)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Observacao).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
