using AutoMapper;
using DtoLayer.DTOs.AboutDto;
using EntityLayer.Concrete;

namespace OnlineEgitimAPI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddAboutDto, About>();
            CreateMap<About, AddAboutDto>();

            CreateMap<UpdateAboutDto, AddAboutDto>().ReverseMap();
        }
    }
}
