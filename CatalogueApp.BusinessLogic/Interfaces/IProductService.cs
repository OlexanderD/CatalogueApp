using CatalogueApp.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.Interfaces
{
    public interface IProductService
    {

        List<Product> GetAllProducts();

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int id);


        Product GetProductById(int id);

        List<Product> GetProductByCategoryId(int categoryId);
    }
}
