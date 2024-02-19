using CatalogueApp.Data.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Data
{
    public class TestContext : IdentityDbContext<IdentityUser>
    {

         public virtual DbSet<Product> Products { get; set; }

         public virtual DbSet<Category> Categories { get; set; }
        
       public TestContext()
        {

        }

        public TestContext(DbContextOptions options) : base(options)
        {
          
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
