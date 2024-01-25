using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogueApp.Data.Data.Models;
using ClassLibrary2.Interfaces;
using AutoMapper;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;

            _mapper = mapper;
        }

        [HttpGet]
        public List<CategoryViewModel> GetAllCategories()
        {
            List<Category> categories = _categoryService.GetAllCategories();

            
            List<CategoryViewModel> categoryViewModels = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories);

            return categoryViewModels;

        }

        [HttpPost]
        public void AddCategory(CategoryViewModel categoryViewModel)
        {
            _categoryService.AddCategory(_mapper.Map<Category>(categoryViewModel));
        }

        [HttpDelete("{id}")]
        public void RemoveCategory(int id)
        {
            _categoryService.RemoveCategory(id);
        }

        [HttpPut]
        public void UpdateCategory(CategoryViewModel categoryViewModel)
        {
            _categoryService.UpdateCategory(_mapper.Map<Category>(categoryViewModel));
        }

        [HttpGet("{id}")]
        public Category GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);
        }
    }
}
