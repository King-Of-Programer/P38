using AutoMapper;
using P38.DTO;
using P38.Models;

namespace P38.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Entity -> DTO (для відповіді)
            CreateMap<Product, ProductReadDTO>();
            CreateMap<Characteristics, CharacteristicsDTO>();

            // DTO -> Entity (для створення/оновлення)
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore());

            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore());

            CreateMap<CharacteristicsDTO, Characteristics>();
        }
    }
}
