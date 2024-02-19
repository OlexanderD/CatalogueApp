using CatalogueApp.Data.Data;
using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Interfaces;
using CatalogueApp.Data.Repositories;
using ClassLibrary2.Services;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.BusinessLogic.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void Should_Succesfully_GetAllCategories()
        {

            List<Category> categories = new List<Category>
            {
                new Category { Id = 1,},
                new Category { Id = 2,},
            };
            var mock = new Mock<ICategoryRepository>();

            mock.Setup(repo =>repo.GetAllCategories()).Returns(categories);

            var categoryService = new CategoryService(mock.Object);

           var result = categoryService.GetAllCategories();


            mock.Verify(mock => mock.GetAllCategories(),Times.Once);

            Assert.Equal(result, categories);


        }
        [Fact]
        public void Should_Succesfully_GetCategoryById()
        {
           Category category = new Category();

            var mock = new Mock<ICategoryRepository>();

            mock.Setup(repo => repo.GetCategoryById(1)).Returns(category);

            var categoryService = new CategoryService(mock.Object);

            var result = categoryService.GetCategoryById(1);


            mock.Verify(mock => mock.GetCategoryById(1), Times.Once);

            Assert.NotNull(result);

            Assert.Equal(result, category);
        }
        [Fact]
        public void Should_Succesfully_DeleteCategory()
        {
            List<Category> categories = new List<Category>
            {
                new Category { Id = 1},
                new Category { Id = 2}
            };

            var categoryToDelete = categories[0];

            var mock = new Mock<TestContext>();

            mock.Setup(repo => repo.Categories).ReturnsDbSet(categories);
            mock.Setup(repo => repo.Categories.Remove(categoryToDelete))
                .Callback(() => categories.Remove(categoryToDelete));

            var categoryRepo = new CategoryRepository(mock.Object);

            categoryRepo.RemoveCategory(categoryToDelete);


            mock.Verify(mock => mock.Categories.Remove(categoryToDelete),Times.Once);

            Assert.Equal(1, categories.Count);
        }

    }
}
