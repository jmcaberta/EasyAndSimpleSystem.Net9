using System.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.Data.Mapping.Users;

public class RolMap : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("rol")
            .HasKey(r => r.RolId);
        builder.Property(r => r.RolId).HasColumnName("rol_id");
        builder.Property(r => r.RolName).HasColumnName("rol_name");
        builder.Property(r => r.Description).HasColumnName("description");
        builder.Property(r => r.Active).HasColumnName("active");
    }
}