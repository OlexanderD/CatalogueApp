using CatalogueApp.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();

        void AddCategory(Category category);

        void RemoveCategory(Category category);

        void UpdateCategory(Category category);

        Category GetCategoryById(int id);


    }
}
