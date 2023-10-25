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
            CreateMap<DiscountDto, Discount>(); // bConfigure reverse mapping

        }

    }
}
