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
        private readonly TestContext _categoryRepository;

        public CategoryRepository(TestContext categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.Categories.ToList();
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Categories.Add(category);
            _categoryRepository.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            Category category = _categoryRepository.Categories.Find(id);
            if (category != null)
            {
                _categoryRepository.Categories.Remove(category);
                _categoryRepository.SaveChanges();
            }
        }

        public  void UpdateCategory(Category category)
        {
            var exsistingCategory = _categoryRepository.Categories.Find(category.Id);

            if (exsistingCategory != null)
            {
                exsistingCategory.Name = category.Name;

               
            }
            _categoryRepository.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.Categories.Find(id);
        }

    }
}
