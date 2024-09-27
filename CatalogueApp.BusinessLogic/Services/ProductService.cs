using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Interfaces;
using CatalogueApp.Interfaces;

namespace CatalogueApp.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public void AddProduct(Product product)
        {
            product.Categories.First().Products.Add(product);

            _productRepository.AddProduct(product);
        }

        public void DeleteProduct(int id)
        {
            Product product = _productRepository.GetProductById(id);
            _productRepository.DeleteProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public List<Product> GetProductByCategoryId(int categoryId)
        {
            return _productRepository.GetProductByCategoryId(categoryId);
        }
    }
}
