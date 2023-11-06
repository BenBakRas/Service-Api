﻿using Service_Api.DTOs;
using AutoMapper;
using ServiceData.ModelLayer;

namespace Service_Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CustoemrGroup
            CreateMap<CustomerGroup, CustomerGroupDto>(); // Configure the mapping
            CreateMap<CustomerGroupDto, CustomerGroup>(); // Configure reverse mapping


            //Discount
            CreateMap<Discount, DiscountDto>(); // Configure the mapping
            CreateMap<DiscountDto, Discount>(); // Configure reverse mapping

            //Product
            CreateMap<Product, ProductDto>(); // Configure the mapping
            CreateMap<ProductDto, Product>(); // Configure reverse mapping

            //Shop
            CreateMap<Shop, ShopDto>(); // Configure the mapping
            CreateMap<ShopDto, Shop>(); // Configure reverse mapping

            //Ingredient
            CreateMap<Ingredient, IngredientDto>(); // Configure the mapping
            CreateMap<IngredientDto, Ingredient>(); // Configure reverse mapping

            //ProductGroup
            CreateMap<ProductGroup, ProductGroupDto>(); // Configure the mapping
            CreateMap<ProductGroupDto, ProductGroup>(); // Configure reverse mapping

        }

    }
}
