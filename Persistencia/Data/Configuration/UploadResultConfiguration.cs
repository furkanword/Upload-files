
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class UploadResultConfiguration : IEntityTypeConfiguration<UploadResult>
{
    public void Configure(EntityTypeBuilder<UploadResult> builder)
    {
        builder.ToTable("UploadResults");

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.FileName)
        .HasMaxLength(150);

        builder.Property(p => p.StoredFileName)
        .HasMaxLength(150);
    }
}
