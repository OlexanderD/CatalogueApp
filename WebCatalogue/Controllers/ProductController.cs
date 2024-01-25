using AutoMapper;
using CatalogueApp.Data.Data.Models;
using ClassLibrary2.Interfaces;
using ClassLibrary2.Services;
using Microsoft.AspNetCore.Http;
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

        public ProductController(IProductService productService,IMapper mapper)
        {
            _productService = productService;

            _mapper = mapper;
        }

        [HttpGet]
        public List<ProductViewModel> GetAllProducts()
        {
            List<Product> products = _productService.GetAllProducts();


            List<ProductViewModel> productViewModels = _mapper.Map<List<Product>, List<ProductViewModel>>(products);

            return productViewModels;
        }

        [HttpPost]
        public void AddProduct(ProductViewModel productViewModel)
        {
            _productService.AddProduct(_mapper.Map<Product>(productViewModel));
        }

        [HttpDelete("{id}")]
        public void RemoveProduct(int id)
        {
            _productService.DeleteProduct(id);
        }

        [HttpPost("UpdateProduct")]
        public void UpdateProduct(ProductViewModel productViewModel)
        {
            _productService.UpdateProduct(_mapper.Map<Product>(productViewModel));
        }

        [HttpGet("{id}")]
        public ProductViewModel GetProductById(int id)
        {
            Product product = _productService.GetProductById(id);

            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);

            return productViewModel;
        }

        [HttpGet("GetProductByCategoryId")]
        public List<Product> GetProductByCategoryId(int categoryId)
        {
            return _productService.GetProductByCategoryId(categoryId);
        }
    }
}
