using BusinessLayer.Abstract;
using BusinessLayer.Abstract.ExternalService;
using BusinessLayer.Concrete;
using BusinessLayer.Concrete.ExternalService;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Container
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAboutDal, EfAboutDal>();
            services.AddScoped<IAboutService, AboutManager>();

            services.AddScoped<IContactDal, EfContactDal>();
            services.AddScoped<IContactService, ContactManager>();

            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddScoped<ILocationDal, EfLocationDal>();
            services.AddScoped<ILocationService, LocationManager>();

            services.AddScoped<IPurchasedCourseDal, EfPurchasedCourseDal>();
            services.AddScoped<IPurchasedCourseService, PurchasedCourseManager>();

            services.AddScoped<IWidgetClickLogDal, EfWidgetClickLogDal>();
            services.AddScoped<IWidgetClickLogService, WidgetClickLogManager>();

            services.AddScoped<IFileDal, EfFileDal>();
            services.AddScoped<IFileService, FileManager>();
            services.AddScoped<ICourseImageFileDal, EfCourseImageFileDal>();
            services.AddScoped<ICourseImageFileService, CourseImageFileManager>();
            services.AddScoped<ICourseVideoFileDal, EfCourseVideoFile>();
            services.AddScoped<ICourseVideoFileService, CourseVideoFileManager>();

            services.AddScoped<IUserCourseAccessService, UserCourseAccessManager>();

            services.AddScoped<ICreateTokenService, CreateTokenManager>();

            services.AddScoped<IFileOperationsService, FileOperationsManager>();
        }
    }
}
