using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Data.Models.Configurations
{
    public class ProductConfugurations:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.HasMany(x => x.Categories)
                .WithMany(x => x.Products)
                .UsingEntity("Producy Categories");

            
        }

    }
}
