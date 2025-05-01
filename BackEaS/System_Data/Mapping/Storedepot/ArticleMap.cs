using System.Entity.Storedepot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.Data.Mapping.Storedepot;

public class ArticleMap : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articles")
            .HasKey((a => a.ArtId));
        builder.Property(a => a.ArtId).HasColumnName("ArtId");
        builder.Property(a => a.CatId).HasColumnName("CatId");
        builder.Property(a => a.ArtCode).HasColumnName("ArtCode");
        builder.Property(a => a.ArtName).HasColumnName("ArtName");
        builder.Property(a => a.SellPrice).HasColumnName("SellPrice");
        builder.Property(a => a.ItemCount).HasColumnName("ItemCount");
        builder.Property(a => a.IsActive).HasColumnName("IsActive");
        
    }
}