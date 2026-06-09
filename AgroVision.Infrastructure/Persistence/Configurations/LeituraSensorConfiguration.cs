using AgroVision.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroVision.Infrastructure.Persistence.Configurations;

public class LeituraSensorConfiguration : IEntityTypeConfiguration<LeituraSensor>
{
    public void Configure(EntityTypeBuilder<LeituraSensor> builder)
    {
        builder.ToTable("TB_LEITURA_SENSOR_GS");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasColumnName("ID_LEITURA_SENSOR")
            .ValueGeneratedOnAdd();

        builder.Property(l => l.MissaoDroneId)
            .HasColumnName("ID_MISSAO_DRONE")
            .IsRequired();

        builder.Property(l => l.TipoSensor)
            .HasColumnName("TIPO_SENSOR")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(l => l.Valor)
            .HasColumnName("VALOR")
            .HasColumnType("NUMBER(15,4)")
            .IsRequired();

        builder.Property(l => l.UnidadeMedida)
            .HasColumnName("UNIDADE_MEDIDA")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(l => l.DataLeitura)
            .HasColumnName("DATA_LEITURA")
            .IsRequired();

        builder.Property(l => l.Latitude)
            .HasColumnName("LATITUDE")
            .HasColumnType("NUMBER(10,6)")
            .IsRequired(false);

        builder.Property(l => l.Longitude)
            .HasColumnName("LONGITUDE")
            .HasColumnType("NUMBER(10,6)")
            .IsRequired(false);

        builder.HasOne(l => l.MissaoDrone)
            .WithMany(m => m.LeiturasSensor)
            .HasForeignKey(l => l.MissaoDroneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}