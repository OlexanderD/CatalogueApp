using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Interfaces;
using ClassLibrary2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Services
{
    public class CategoryService:ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryService)
        {
            _categoryRepository = categoryService;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
        }

        public void RemoveCategory(int id)
        {
            Category category = _categoryRepository.GetCategoryById(id);
            _categoryRepository.RemoveCategory(category);
        }
    }
}
