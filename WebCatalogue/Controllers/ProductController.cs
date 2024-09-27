using AutoMapper;
using CatalogueApp.Data.Data.Models;
using CatalogueApp.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        private readonly IValidator <ProductViewModel> _validator;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService,IMapper mapper, IValidator<ProductViewModel> validator, ILogger<ProductController> logger)
        {
            _productService = productService;

            _mapper = mapper;

            _validator = validator;

            _logger = logger;
        }

        [HttpGet]
        public List<ProductViewModel> GetAllProducts()
        {
            List<Product> products = _productService.GetAllProducts();


            List<ProductViewModel> productViewModels = _mapper.Map<List<Product>, List<ProductViewModel>>(products);

            _logger.LogInformation("All Products");

            return productViewModels;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddProduct(ProductViewModel productViewModel)
        {
            var validationResult = _validator.Validate(productViewModel);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
            _productService.AddProduct(_mapper.Map<Product>(productViewModel));
            _logger.LogInformation("Product Added");
            return Ok("Product added");
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveProduct(int id)
        {
            _logger.LogInformation("Product Deleted");
            _productService.DeleteProduct(id);
            return Ok("Product Deleted");
        }

        [HttpPost("UpdateProduct")]
        [Authorize]
        public IActionResult UpdateProduct(ProductViewModel productViewModel)
        {
            var validationResult = _validator.Validate(productViewModel);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { Property = e.PropertyName, e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
            _productService.UpdateProduct(_mapper.Map<Product>(productViewModel));
            _logger.LogInformation("Product succesfully updated");
            return Ok("Product succesfully updated");
        }

        [HttpGet("{id}")]
        public ProductViewModel GetProductById(int id)
        {
            Product product = _productService.GetProductById(id);

            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);

            _logger.LogInformation($"Product #{id}");

            return productViewModel;
        }

        [HttpGet("GetProductByCategoryId")]
        public List<Product> GetProductByCategoryId(int categoryId)
        {
            _logger.LogInformation($"Product by category{categoryId}");
            return _productService.GetProductByCategoryId(categoryId);
        }
    }
}
