using Castle.Core.Logging;
using CatalogueApp.Data.Data;
using CatalogueApp.Data.Data.Models;
using CatalogueApp.Data.Interfaces;
using CatalogueApp.Data.Repositories;
using ClassLibrary2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.BusinessLogic.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void Should_Succesfully_GetAllProducts()
        {
            List<Product> products = new List<Product>
           {
               new Product(),
               new Product()
           };
            var mock = new Mock<IProductRepository>();

            mock.Setup(repo => repo.GetAllProducts()).Returns(products);


            var productService = new ProductService(mock.Object);
            
            var result = productService.GetAllProducts();


            mock.Verify(mock => mock.GetAllProducts(),Times.Once);
            Assert.NotNull(result);
            Assert.Equal(products,result);


        }
        [Fact]
        public void Should_Succesfully_GetProductById()
        {
            Product product = new Product();

            var mock = new Mock<IProductRepository>();

            mock.Setup(repo =>repo.GetProductById(1)).Returns(product);

            var productService = new ProductService(mock.Object);

            var result = productService.GetProductById(1);

            mock.Verify(mock => mock.GetProductById(1),Times.Once);
            Assert.NotNull(result);
            Assert.Equal(product,result);
        }
        [Fact]
        public void Should_Succesfully_GetProductByCategoryId()
        {
            int categoryId = 1;

            List<Product> products = new List<Product>
            {
                new Product() { Id = categoryId },
                new Product() { Id = categoryId }
            };
            var mock = new Mock<IProductRepository>();

            mock.Setup(repo =>repo.GetProductByCategoryId(categoryId)).Returns(products);

            var productService = new ProductService(mock.Object);

            var result = productService.GetProductByCategoryId(categoryId);

            mock.Verify(mock => mock.GetProductByCategoryId(categoryId), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(products, result);
        }
        [Fact]
        public void Should_Succesfully_DeleteProduct()
        {
            
            List<Product> products = new List<Product>
            {
                new Product(),
                new Product(),
                new Product()
            };

            var productToDelete = products[0];

            var mock = new Mock<TestContext>();          

            mock.Setup(repo => repo.Products).ReturnsDbSet(products);
            mock.Setup(x => x.Products.Remove(productToDelete))
                .Callback(() => products.Remove(productToDelete));

            var productRepository = new ProductRepository(mock.Object);

            productRepository.DeleteProduct(productToDelete);

            mock.Verify(mock => mock.Products.Remove(productToDelete), Times.Once);
            Assert.Equal(2, products.Count);
        }

    }
}
