using CatalogueApp.Data.Data;
using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly TestContext _dbContext;

        public CategoryRepository(TestContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public void AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void RemoveCategory(Category category)
        {
           
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            
        }

        public  void UpdateCategory(Category category)
        {
            var exsistingCategory = _dbContext.Categories.Find(category.Id);

            if (exsistingCategory == null)
            {
                throw new Exception($"Error occurred while updating category, category with id ({category.Id}) not found");
            }
            exsistingCategory.Name = category.Name;
            _dbContext.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

    }
}
