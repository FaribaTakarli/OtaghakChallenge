
using AutoMapper;
using OtaghakChallenge.Application.ProductApplication.Commands.AddProduct;
using OtaghakChallenge.Application.ProductApplication.Commands.ModifyProduct;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using OtaghakChallenge.Domain;

namespace OtaghakChallenge.Application.AddressApplication.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductView>().ReverseMap();

        CreateMap<Product, AddProductCommand>().ReverseMap();
        CreateMap<ModifyProductCommand, AddProductCommand>().ReverseMap();




    }
}