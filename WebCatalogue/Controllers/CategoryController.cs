using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogueApp.Data.Data.Models;
using ClassLibrary2.Interfaces;
using AutoMapper;
using WebCatalogue.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace WebCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        private readonly IValidator<CategoryViewModel> _validator;

        private readonly ILogger<CategoryController> _logger;   
        public CategoryController(ICategoryService categoryService, IMapper mapper, IValidator<CategoryViewModel> validator, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;

            _mapper = mapper;

            _validator = validator;

            _logger = logger;
        }

        [HttpGet]
        public List<CategoryViewModel> GetAllCategories()
        {
            List<Category> categories = _categoryService.GetAllCategories();

            
            List<CategoryViewModel> categoryViewModels = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories);

            _logger.LogInformation("All Categories");

            return categoryViewModels;

        }

        [HttpPost]
        [Authorize]
        public IActionResult AddCategory(CategoryViewModel categoryViewModel)
        {
            var validationResult = _validator.Validate(categoryViewModel);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName,e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
            _categoryService.AddCategory(_mapper.Map<Category>(categoryViewModel));

            _logger.LogInformation("Category Added");

            return Ok("Category Added");
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id)
        {
            _logger.LogInformation("Category Removed");
            _categoryService.RemoveCategory(id);
            return Ok("Category Removed");
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var validationResult = _validator.Validate(categoryViewModel);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
            _categoryService.UpdateCategory(_mapper.Map<Category>(categoryViewModel));
            _logger.LogInformation("Category succesfully updated");
            return Ok("Category succesfully updated");
        }

        [HttpGet("{id}")]
        public Category GetCategoryById(int id)
        {
            _logger.LogInformation($"Category #{id}");
            return _categoryService.GetCategoryById(id);
        }
    }
}
