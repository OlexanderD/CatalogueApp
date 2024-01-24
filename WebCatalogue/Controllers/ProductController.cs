using CatalogueApp.Data.Data.Models;
using ClassLibrary2.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCatalogue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public List<Product> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        [HttpPost]
        public void AddProduct(Product product)
        {
            _productService.AddProduct(product);
        }

        [HttpDelete("{id}")]
        public void RemoveProduct(Product product)
        {
            _productService.DeleteProduct(product);
        }

        [HttpPut]
        public void UpdateProduct(Product product)
        {
            _productService.UpdateProduct(product);
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpGet("GetProductByCategoryId")]
        public List<Product> GetProductByCategoryId(int categoryId)
        {
            return _productService.GetProductByCategoryId(categoryId);
        }
    }
}
