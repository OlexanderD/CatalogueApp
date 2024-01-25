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
            // тут сделать запрос в контекст категории, и попробовать вытянуть категорию product.Categories.First().Id

            var category = _dbContext.Categories.Find(product.Categories.First().Id);

            // чекнуть является ли вытянутая пука null, если нет - product.Categories[0] = вытянутая категория
            if (category != null)
            {
                product.Categories[0] = category;
            }

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
            Product? existingProduct = _dbContext.Products.Find(product.Id);

            if (existingProduct != null)
            {
                product.Categories = product.Categories.Select(c =>
                {
                    var category = _dbContext.Categories.Find(c.Id);

                    return category ?? c;
                }).ToList();

                existingProduct.Categories.RemoveAll(c => product.Categories.Any(pc => pc.Id != c.Id));

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;

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
