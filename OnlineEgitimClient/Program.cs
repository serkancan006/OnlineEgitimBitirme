using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NuGet.Configuration;
using OnlineEgitimClient.Service;
using System.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<CustomHttpClient>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var customHttpClient = new CustomHttpClient(httpClientFactory, configuration, httpContextAccessor);
    return customHttpClient;
});
//Google Login
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies"; // Varsayýlan þema adýný belirtin
}).AddCookie("Cookies") // Cookies için þema adýný belirtin
.AddGoogle("Google", googleOptions =>
{
    googleOptions.ClientId = "964273312425-1tehhedivhe9pq6quu396a09i7aas403.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
    googleOptions.SaveTokens = true;
})
.AddFacebook("Facebook", facebookOptions =>
{
    facebookOptions.AppId = "318561934272820";
    facebookOptions.AppSecret = "a89c931142a0bc7b216575a6b44957e2";
    facebookOptions.CallbackPath = "/signin-facebook";
    facebookOptions.SaveTokens = true;
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Middleware
//app.Use(async (context, next) =>
//{
//    await next();
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.Run();
