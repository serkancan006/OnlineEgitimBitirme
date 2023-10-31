using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>();

builder.Services.AddScoped<IAboutDal, EfAboutDal>();
builder.Services.AddScoped<IAboutService, AboutManager>();

builder.Services.AddScoped<IContactDal, EfContactDal>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<ICourseDal, EfCourseDal>();
builder.Services.AddScoped<ICourseService, CourseManager>();

builder.Services.AddScoped<ILocationDal, EfLocationDal>();
builder.Services.AddScoped<ILocationService, LocationManager>();

builder.Services.AddScoped<IPurchasedCourseDal, EfPurchasedCourseDal>();
builder.Services.AddScoped<IPurchasedCourseService, PurchasedCourseManager>();

builder.Services.AddScoped<IWidgetClickLogDal, EfWidgetClickLogDal>();
builder.Services.AddScoped<IWidgetClickLogService, WidgetClickLogManager>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("E-EgitimApiCors", opts =>
    {
        opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("E-EgitimApiCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
