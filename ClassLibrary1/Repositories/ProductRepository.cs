using CatalogueApp.Data.Data;
using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogueApp.Data.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly TestContext _dbContext;

        public ProductRepository(TestContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.Include(x =>x.Categories).ToList();
        }

        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
                       
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            
        }
        public void UpdateProduct(Product product)
        {
            Product existingProduct = _dbContext.Products.Find(product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;

                existingProduct.Description = product.Description;

                existingProduct.Price = product.Price;               

                _dbContext.SaveChanges();
            }
        }
        public Product GetProductById(int id)
        {
            return _dbContext.Products.Include(x => x.Categories).FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetProductByCategoryId(int categoryId)
        {
           
            return _dbContext.Products
                .Where(p => p.Categories.Any(c => c.Id == categoryId || c.ParentCategoryId == categoryId))
                .ToList();
        }


    }
}
