using CatalogueApp.Data.Data.Models;
using ClassLibrary2.Interfaces;

namespace CatalogueApp.ConsoleUi.Controllers
{
    public class CategoryController
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        public void AddCategory(Category category)
        {
            _categoryService.AddCategory(category);
        }

        public void RemoveCategory(Category category)
        {
           // _categoryService.RemoveCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryService.UpdateCategory(category);
        }

        public Category GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);
        }

    }
}
