using AutoMapper;
using CarStore.Models.Dto;

namespace CarStore
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CarDTO, CarUpdateDTO>().ReverseMap();
            CreateMap<CarDTO, CarCreateDTO>().ReverseMap();
        }
    }
}
