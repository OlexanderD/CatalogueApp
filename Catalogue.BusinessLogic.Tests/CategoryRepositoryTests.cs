using CatalogueApp.Data.Data;
using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.BusinessLogic.Tests
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public void Should_Succesfully_AddCategory()
        {
            List<Category> categories = new List<Category>
            {
                new Category { Id = 1,},
                new Category { Id = 2,}
            };

            Category category = new Category();

            var mock = new Mock<TestContext>();

            mock.Setup(repo => repo.Categories).ReturnsDbSet(categories);

            mock.Setup(repo =>repo.Categories.Add(category))
                .Callback(()=> categories.Add(category));


            var categoryRepo = new CategoryRepository(mock.Object);

            categoryRepo.AddCategory(category);

            mock.Verify(mock => mock.Categories.Add(category),Times.Once);

            Assert.Equal(3, categories.Count);
        }
    }
}
