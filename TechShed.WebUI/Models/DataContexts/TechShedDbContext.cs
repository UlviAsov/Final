using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechShed.WebUI.Models.Entities;

namespace TechShed.WebUI.Models.DataContexts
{
    public class TechShedDbContext : DbContext
       
    {
        public TechShedDbContext(DbContextOptions options)
            :base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<BlogPostTag> BlogPostTagCloud { get; set; }

        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> Colors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPostTag>(e =>
            {
                e.HasKey(k => new { k.BlogPostId, k.PostTagId });
            });
            modelBuilder.Entity<ProductSpecification>(e =>
            {
                e.HasKey(k => new { k.ProductId, k.SpecificationId });
            });
            modelBuilder.Entity<ProductPricing>(e =>
            {
                e.HasKey(k => new { k.ProductId, k.ColorId });
            });

        }


        // public object Brand { get; internal set; }
    }
}
