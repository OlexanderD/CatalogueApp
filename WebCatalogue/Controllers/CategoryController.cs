using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogueApp.Data.Data.Models;
using ClassLibrary2.Interfaces;

namespace WebCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public List<Category> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpPost]
        public void AddCategory(Category category)
        {
            _categoryService.AddCategory(category);
        }

        [HttpDelete]
        public void RemoveCategory(Category category)
        {
            _categoryService.RemoveCategory(category);
        }

        [HttpPut]
        public void UpdateCategory(Category category)
        {
            _categoryService.UpdateCategory(category);
        }

        [HttpGet("{id}")]
        public Category GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);
        }
    }
}
