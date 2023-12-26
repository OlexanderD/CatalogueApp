using CatalogueApp.ConsoleUi.Controllers;
using CatalogueApp.Data.Data;
using CatalogueApp.Data.Interfaces;
using CatalogueApp.Data.Repositories;
using ClassLibrary2.Interfaces;
using ClassLibrary2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.ConsoleUi.Common
{
    public class ServiceHelper
    {
        public static IServiceProvider BuildServiceProvider(IConfiguration config)
        {
            IServiceCollection service = new ServiceCollection();

            var connectionString = config.GetConnectionString("DefaultConnection");

            service.AddDbContext<TestContext>(options => options.UseSqlite(connectionString));

            service.AddTransient<IProductRepository, ProductRepository>();

            service.AddTransient<IProductService, ProductService>();

            service.AddTransient<ProductController>();


            return service.BuildServiceProvider();
        }

    }
}
