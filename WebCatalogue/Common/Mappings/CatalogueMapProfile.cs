using AutoMapper;
using CatalogueApp.Data.Data.Models;
using Microsoft.AspNetCore.Identity;
using WebCatalogue.ViewModels;

namespace WebCatalogue.Common.Mappings
{
    public class CatalogueMapProfile:Profile
    {
        public CatalogueMapProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>().ReverseMap();

            CreateMap<IdentityUser,UserViewModel>()
                .ReverseMap()
                .ForMember(m => m.PasswordHash, n => n.MapFrom(u => u.Password)); ;
        }

    }
}
