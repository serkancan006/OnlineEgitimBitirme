using AutoMapper;
using OnlineEgitimClient.Dtos.AboutDto;
using OnlineEgitimClient.Dtos.ContactDto;
using OnlineEgitimClient.Dtos.CourseDto;
using OnlineEgitimClient.Dtos.LocationDto;
using OnlineEgitimClient.Dtos.PurchasedCourseDto;
using OnlineEgitimClient.Dtos.WidgetClickLogDto;
using EntityLayer.Concrete;
using OnlineEgitimClient.Dtos.AppUserDto;
using EntityLayer.Concrete.identity;

namespace OnlineEgitimClient.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ListAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, About>().ReverseMap();
            CreateMap<AddAboutDto, About>().ReverseMap();

            CreateMap<ListContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, Contact>().ReverseMap();
            CreateMap<AddContactDto, Contact>().ReverseMap();

            CreateMap<ListCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();
            CreateMap<AddCourseDto, Course>().ReverseMap();

            CreateMap<ListLocationDto, Location>().ReverseMap();
            CreateMap<UpdateLocationDto, Location>().ReverseMap();
            CreateMap<AddLocationDto, Location>().ReverseMap();

            CreateMap<ListPurchasedCourseDto, PurchasedCourse>().ReverseMap();
            CreateMap<UpdatePurchasedCourseDto, PurchasedCourse>().ReverseMap();
            CreateMap<AddPurchasedCourseDto, PurchasedCourse>().ReverseMap();

            CreateMap<ListWidgetClickLogDto, WidgetClickLog>().ReverseMap();
            CreateMap<UpdateWidgetClickLogDto, WidgetClickLog>().ReverseMap();
            CreateMap<AddWidgetClickLogDto, WidgetClickLog>().ReverseMap();

            //Kullanıcı kayıt ve giriş
            CreateMap<RegisterAppUserDto, AppUser>().ReverseMap();
            CreateMap<LoginAppUserDto, AppUser>().ReverseMap();

        }
    }
}
