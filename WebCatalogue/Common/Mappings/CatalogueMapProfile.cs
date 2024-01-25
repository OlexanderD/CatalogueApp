using AutoMapper;
using CatalogueApp.Data.Data.Models;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Common.Mappings
{
    public class CatalogueMapProfile:Profile
    {
        public CatalogueMapProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>().ReverseMap();
        }

    }
}
