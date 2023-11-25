using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<Context>();

// Add Jwt Bearer Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = true;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = "https://localhost",
        ValidAudience = "https://localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aspnetcoreapiapi")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

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

builder.Services.AddScoped<IFileDal, EfFileDal>();
builder.Services.AddScoped<IFileService, FileManager>();
builder.Services.AddScoped<ICourseImageFileDal, EfCourseImageFileDal>();
builder.Services.AddScoped<ICourseImageFileService, CourseImageFileManager>();
builder.Services.AddScoped<ICourseVideoFileDal, EfCourseVideoFile>();
builder.Services.AddScoped<ICourseVideoFileService, CourseVideoFileManager>();
//builder.Services.AddScoped<ICourseCourseVideoFileDal, EfCourseCourseVideoFileDal>();
//builder.Services.AddScoped<ICourseCourseVideoFileService, CourseCourseVideoFileManager>();
builder.Services.AddScoped<IUserCourseAccessService, UserCourseAccessManager>();

builder.Services.AddScoped<ICreateTokenService, CreateTokenManager>();

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
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseCors("E-EgitimApiCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
