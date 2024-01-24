using CatalogueApp.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();

        void AddCategory(Category category);

        void RemoveCategory(int id);

        void UpdateCategory(Category category);

        Category GetCategoryById(int id);

    }
}
