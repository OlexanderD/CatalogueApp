using CatalogueApp.Data.Data.Models;
using ClassLibrary2.Interfaces;
using ClassLibrary2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.ConsoleUi.Controllers
{
    public class ProductController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        public void AddProduct(Product product)
        {
            _productService.AddProduct(product);
        }

        public void RemoveProduct(Product product)
        {
            _productService.DeleteProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productService.UpdateProduct(product);
        }

        public Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }
    }
}
