using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Data.Models
{
    public class Category
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public int? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }

        public List<Category>  ChildCategories { get; set; }

        public List<Product> Products { get; set; } = new();

       

    }
}
