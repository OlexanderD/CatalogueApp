using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Data.Models.Configurations
{
    internal class CategoryConfiguration:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> categoryBuilder)
        {
            categoryBuilder.HasKey(x => x.Id);

            categoryBuilder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            categoryBuilder.HasOne(x => x.ParentCategory)
                .WithMany(x => x.ChildCategories)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(x => x.ParentCategoryId);

        }
    }
}
