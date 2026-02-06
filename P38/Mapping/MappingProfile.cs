using AutoMapper;
using P38.DTO;
using P38.Models;

namespace P38.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Product, ProductCreateDTO> ();
            CreateMap<Product, CharacteristicsDTO> ();

            CreateMap<ProductCreateDTO, Product>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Characteristics, 
                opt => opt.MapFrom(s => s.Characteristics ?? new CharacteristicsDTO()));

           CreateMap<ProductUpdateDTO, Product>()
                 .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Characteristics,
                opt => opt.MapFrom(s => s.Characteristics ?? new CharacteristicsDTO()));

            CreateMap<CharacteristicsDTO, Characteristics>();
        }
    }
}
