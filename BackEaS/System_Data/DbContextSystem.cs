

using System.Data.Mapping.Storedepot;
using System.Entity.Storedepot;
using Microsoft.EntityFrameworkCore;

namespace System.Data;

public class DbContextSystem : DbContext
{
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        
        public DbContextSystem(DbContextOptions<DbContextSystem> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfiguration(new CategoryMap());
                modelBuilder.ApplyConfiguration(new ArticleMap());
        }
       
}