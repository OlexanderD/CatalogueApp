using CatalogueApp.Data.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Data
{
    public class TestContext : DbContext
    {

         public DbSet<Product> Products { get; set; }


        
       

        public TestContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
             Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
