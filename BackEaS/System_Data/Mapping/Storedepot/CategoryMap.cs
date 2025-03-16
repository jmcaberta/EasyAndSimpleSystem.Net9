using System.Entity.Storedepot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.Data.Mapping.Storedepot;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category")
            .HasKey(c => c.CatId);
        builder.Property(c => c.CatId).HasColumnName("cat_id");
        builder.Property(c => c.CatName).HasColumnName("cat_name");
        builder.Property(c => c.CatDescription).HasColumnName("cat_description");
        builder.Property(c => c.IsActive).HasColumnName("is_active");
    }
}