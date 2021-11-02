using Microsoft.EntityFrameworkCore;
using bazaaramed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace  bazaaramed.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<KategoriAddresses> KategoriAddresses { get; set; }
        public DbSet<Markets> Markets { get; set; }
        public DbSet<ProductAddress> ProductAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<ProductsInBrands> ProductsInBrands { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ProductPoolDbbazaaramed;Trusted_Connection=True;");
        }
    }
}
