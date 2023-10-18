using AutoMapper;
using DogApp.API.Models;
using DogApp.BLL.DTOs;
using DogApp.DAL.Entities;

namespace DogApp.API.Mappings;

public class DogMappingProfile : Profile
{
    public DogMappingProfile()
    {
        CreateMap<AddDogRequest, Dog>();

        CreateMap<Dog, DogDto>();
    }
}
