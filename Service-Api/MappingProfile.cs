using Service_Api.DTOs;
using AutoMapper;
using ServiceData.ModelLayer;

namespace Service_Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerGroup, CustomerGroupDto>(); // Configure the mapping
            CreateMap<CustomerGroupDto, CustomerGroup>(); // Optionally, configure reverse mapping
        }

    }
}
