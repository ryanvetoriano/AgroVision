using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TB_USER_GS");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("ID_USER")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Cpf)
            .HasColumnName("CPF_USER")
            .HasColumnType("NUMBER(11)")
            .IsRequired();

        builder.HasIndex(u => u.Cpf)
            .IsUnique();

        builder.Property(u => u.Nome)
            .HasColumnName("NOME_USER")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(u => u.Senha)
            .HasColumnName("SENHA_USER")
            .HasMaxLength(18)
            .IsRequired();

        builder.Property(u => u.NomeFazenda)
            .HasColumnName("NM_FAZENDA")
            .HasMaxLength(60)
            .IsRequired();

        builder.HasMany(u => u.Plantacoes)
            .WithOne(p => p.Usuario)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}