using AutoMapper;
using DtoLayer.DTOs.AboutDto;
using DtoLayer.DTOs.ContactDto;
using DtoLayer.DTOs.CourseDto;
using DtoLayer.DTOs.LocationDto;
using DtoLayer.DTOs.PurchasedCourseDto;
using DtoLayer.DTOs.WidgetClickLogDto;
using EntityLayer.Concrete;

namespace OnlineEgitimAPI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, AddAboutDto>().ReverseMap();

            CreateMap<AddContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, AddContactDto>().ReverseMap();

            CreateMap<AddCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, AddCourseDto>().ReverseMap();

            CreateMap<AddLocationDto, Location>().ReverseMap();
            CreateMap<UpdateLocationDto, AddLocationDto>().ReverseMap();

            CreateMap<AddPurchasedCourseDto, PurchasedCourse>().ReverseMap();
            CreateMap<UpdatePurchasedCourseDto, AddPurchasedCourseDto>().ReverseMap();

            CreateMap<AddWidgetClickLogDto, WidgetClickLog>().ReverseMap();
            CreateMap<UpdateWidgetClickLogDto, AddWidgetClickLogDto>().ReverseMap();

        }
    }
}
