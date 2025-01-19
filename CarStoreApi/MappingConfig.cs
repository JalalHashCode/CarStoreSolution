using AutoMapper;
using CarStoreApi.Models;
using CarStoreApi.Models.Dto;

namespace CarStoreApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Car, CarUpdateDTO>().ReverseMap();
            CreateMap<Car, CarCreateDTO>().ReverseMap();
        }
    }
}
