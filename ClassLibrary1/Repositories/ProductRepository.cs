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
                .Include(p => p.Categories) // подключаем таблицу категорий, чтобы ef следил за изменениями связей с этой таблицей
                .First(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                throw new Exception($"Product with id ({product.Id}) not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            var categoryIds = product.Categories.Select(pc => pc.Id); // выбираем Idшники категорий которые будут у обновленного продукта

            existingProduct.Categories = _dbContext.Categories
                .Where(c => categoryIds.Any(pc => c.Id == pc)) // вытаскиваем из базы категории Id которых содержится в списке идшников
                .ToList();

            _dbContext.SaveChanges(); // значения меняются благодаря трекингу 
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
