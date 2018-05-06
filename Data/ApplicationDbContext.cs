using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AngularSPAWebAPI.Models;

namespace AngularSPAWebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
      
      builder.Entity<ProductProductCategory>()
      .HasKey(pc => new { pc.ProductCategoryID, pc.ProductID });

      builder.Entity<ProductProductCategory>()
          .HasOne(pc => pc.Product)
          .WithMany(p => p.ProductProductCategories)
          .HasForeignKey(pc => pc.ProductID);

      builder.Entity<ProductProductCategory>()
          .HasOne(pc => pc.ProductCategory)
          .WithMany(c => c.ProductProductCategories)
          .HasForeignKey(pc => pc.ProductCategoryID);

      base.OnModelCreating(builder);

    }
  }
}
