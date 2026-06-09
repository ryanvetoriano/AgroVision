using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class DroneConfiguration : IEntityTypeConfiguration<Drone>
{
    public void Configure(EntityTypeBuilder<Drone> builder)
    {
        builder.ToTable("TB_DRONE_GS");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasColumnName("ID_DRONE")
            .ValueGeneratedOnAdd();

        builder.Property(d => d.CodigoIdentificacao)
            .HasColumnName("CODIGO_IDENTIFICACAO")
            .HasMaxLength(40)
            .IsRequired();

        builder.HasIndex(d => d.CodigoIdentificacao)
            .IsUnique();

        builder.Property(d => d.Modelo)
            .HasColumnName("MODELO")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(d => d.Fabricante)
            .HasColumnName("FABRICANTE")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(d => d.AutonomiaMinutos)
            .HasColumnName("AUTONOMIA_MINUTOS")
            .HasColumnType("NUMBER(10,2)")
            .IsRequired();

        builder.Property(d => d.Ativo)
            .HasColumnName("ATIVO")
            .HasColumnType("NUMBER(1)")
            .IsRequired();

        builder.HasMany(d => d.Missoes)
            .WithOne(m => m.Drone)
            .HasForeignKey(m => m.DroneId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}