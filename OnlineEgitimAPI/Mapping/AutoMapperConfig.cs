using AutoMapper;
using DtoLayer.DTOs.AboutDto;
using DtoLayer.DTOs.AppUserDto;
using DtoLayer.DTOs.ContactDto;
using DtoLayer.DTOs.CourseDto;
using DtoLayer.DTOs.LocationDto;
using DtoLayer.DTOs.PurchasedCourseDto;
using DtoLayer.DTOs.WidgetClickLogDto;
using EntityLayer.Concrete;
using EntityLayer.Concrete.identity;

namespace OnlineEgitimAPI.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AddAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, About>().ReverseMap();

            CreateMap<AddContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, Contact>().ReverseMap();

            CreateMap<AddCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();

            CreateMap<AddLocationDto, Location>().ReverseMap();
            CreateMap<UpdateLocationDto, Location>().ReverseMap();

            CreateMap<AddPurchasedCourseDto, PurchasedCourse>().ReverseMap();
            CreateMap<UpdatePurchasedCourseDto, PurchasedCourse>().ReverseMap();

            CreateMap<AddWidgetClickLogDto, WidgetClickLog>().ReverseMap();
            CreateMap<UpdateWidgetClickLogDto, WidgetClickLog>().ReverseMap();

            // Login - Register işlemleri
            CreateMap<RegisterAppUserDto, AppUser>().ReverseMap();
            CreateMap<LoginUserDto, AppUser>().ReverseMap();

        }
    }
}
