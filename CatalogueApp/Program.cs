using CatalogueApp.ConsoleUi.Common;
using CatalogueApp.ConsoleUi.Controllers;
using CatalogueApp.Data.Data.Models;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello");

        var config = ConfigurationHelper.SetupConfiguration();
        
        var serviceProvider = ServiceHelper.BuildServiceProvider(config);

        ProductController productController = serviceProvider.GetRequiredService<ProductController>();

        Product product = new Product { Name = "Puka",Description = "PukaDes",Price = 50};

        Product product1 = new Product { Name = "Sraka",Description = "SrakaDes",Price=30};

        productController.AddProduct(product);

        productController.AddProduct(product1);

        productController.RemoveProduct(product);

        product1.Name = "PukaReborn";

        product1.Description = "PukaDes";

        product1.Price = 50;

        productController.UpdateProduct(product1);

        var result = productController.GetAllProducts();
        
        foreach (var resultItem in result)
        {
            Console.WriteLine(resultItem.Name);
            Console.WriteLine(resultItem.Description);
            Console.WriteLine(resultItem.Price);
        }


    }
}