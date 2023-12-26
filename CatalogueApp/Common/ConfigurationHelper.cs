using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueApp.ConsoleUi.Common
{
    public class ConfigurationHelper
    {
        public static IConfigurationRoot SetupConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            return builder.Build();
            
        }

    }
}
