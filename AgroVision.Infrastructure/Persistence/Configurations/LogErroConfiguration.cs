using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class LogErroConfiguration : IEntityTypeConfiguration<LogErro>
{
    public void Configure(EntityTypeBuilder<LogErro> builder)
    {
        builder.ToTable("TB_LOG_ERRO_GS");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasColumnName("ID_LOG")
            .ValueGeneratedOnAdd();

        builder.Property(l => l.NomeProcedure)
            .HasColumnName("NOME_PROCEDURE")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(l => l.NomeUsuario)
            .HasColumnName("NOME_USER_GS")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(l => l.DataErro)
            .HasColumnName("DATA_ERRO")
            .IsRequired();

        builder.Property(l => l.CodigoErro)
            .HasColumnName("CODIGO_ERRO")
            .IsRequired();

        builder.Property(l => l.MensagemErro)
            .HasColumnName("MENSAGEM_ERRO")
            .HasMaxLength(500)
            .IsRequired(false);
    }
}