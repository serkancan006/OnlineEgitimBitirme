using Microsoft.AspNetCore.Http;
using NuGet.Configuration;
using OnlineEgitimClient.Service;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<CustomHttpClient>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

    var customHttpClient = new CustomHttpClient(httpClientFactory, configuration, httpContextAccessor);

    return customHttpClient;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// b�t�n httpclient isteklerine �erezlerde Token ad�nda de�er varsa headera veri ekleyip yollar
//app.Use(async (context, next) =>
//{
//    var jwtToken = context.Request.Cookies["Token"];
//    //var baseUrlvalue = builder.Configuration["BaseUrls:BaseUrl"];
//    //var baseUrl = new Uri(uriString: baseUrlvalue ?? "").AbsolutePath;
//    //!string.IsNullOrEmpty(jwtToken)
//    context.Request.Headers.Add("Authorization", "Bearer " + jwtToken);

//    await next();
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.Run();
