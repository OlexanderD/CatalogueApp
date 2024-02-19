using CatalogueApp.Data.Data;
using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Interfaces;
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
    public class ProductRepositoryTests
    {
    [Fact]
    public void Should_Succesfully_AddProduct()
        {
            List<Category> categories = new List<Category>
            {
               new Category { Id = 1,},
               new Category { Id = 2,}
            };

            List<Product> products = new List<Product>
            {
                new Product { Id = 1,},
                new Product { Id = 2,}
            };

            var mock = new Mock<TestContext>();
            
            Product product = new Product();


            product.Categories = categories;
                     
            mock.Setup(repo =>repo.Products).ReturnsDbSet(products);
            mock.Setup(repo => repo.Categories).ReturnsDbSet(categories);
            mock.Setup(repo => repo.Categories.Find(1)).Returns(categories[1]);
            mock.Setup(repo => repo.Products.Add(product))
                .Callback(() => products.Add(product));


            var productRepo = new ProductRepository(mock.Object);
          
            productRepo.AddProduct(product);

            mock.Verify(mock => mock.Products.Add(product),Times.Once);

            Assert.Equal(3, products.Count);

            Assert.Equal(categories[1], product.Categories[1]);

        }
    }
}
