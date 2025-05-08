using System.Entity.Storedepot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.Data.Mapping.Storedepot;

public class ArticleMap : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articles")
           .HasKey((a => a.ArticleId));
        builder.HasOne(a => a.Category)
            .WithMany(c => c.Articles)
            .HasForeignKey(a => a.CatId)
            .HasConstraintName("FK_Article_Category");
        builder.Property(a => a.ArticleId).HasColumnName("art_id");
        builder.Property(a => a.CatId).HasColumnName("cat_id");
        builder.Property(a => a.ArtCode).HasColumnName("art_code");
        builder.Property(a => a.ArtName).HasColumnName("art_name");
        builder.Property(a => a.SellPrice).HasColumnName("sell_price");
        builder.Property(a => a.ItemCount).HasColumnName("item_count");
        builder.Property(a => a.ArtDescription).HasColumnName("art_description");
        builder.Property(a => a.IsActive).HasColumnName("is_active");
        
    }
}