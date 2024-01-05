using CatalogueApp.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Data.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);


        Product GetProductById(int id);

         List <Product> GetProductByCategoryId(int categoryId);

    }
}
