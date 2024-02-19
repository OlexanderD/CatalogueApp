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
            

            var category = _dbContext.Categories.Find(product.Categories.First().Id);

            
            if (category == null)
            {
                throw new Exception($"Product with such id{product.Id} not found");
            }
            product.Categories[0] = category;
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
            Product? existingProduct = _dbContext.Products
                .Include(p => p.Categories) 
                .First(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                throw new Exception($"Product with id ({product.Id}) not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            var categoryIds = product.Categories.Select(pc => pc.Id); 

            existingProduct.Categories = _dbContext.Categories
                .Where(c => categoryIds.Any(pc => c.Id == pc)) 
                .ToList();

            _dbContext.SaveChanges(); 
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
