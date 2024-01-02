using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;
using OnlineEgitimClient.Service;
using System.Configuration;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
    //options.Cookie.Name = ".UserCookie";
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<CustomHttpClient>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var customHttpClient = new CustomHttpClient(httpClientFactory, configuration, httpContextAccessor);
    return customHttpClient;
});
builder.Services.AddSingleton<BasketService>();
//Google Login
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
    //options.DefaultScheme = "Cookies"; // Varsayýlan þema adýný belirtin
    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//Middleware
app.Use(async (context, next) =>
{
    var jwt = context.Request.Cookies["Token"];

    if (jwt != null)
        context.Request.Headers.Append("Authorization", "Bearer " + jwt);

    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePages(async context =>
{
    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;
    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        // Kullanýcý giriþ yapmamýþsa, giriþ sayfasýna yönlendir
        response.Redirect("/Login/Index");
    }
    if (response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        // Kullanýcý yetkisizse, yetkiniz yok sayfasýna yönlendir
        response.Redirect("/ErrorPage/Error403/");
    }
    if (response.StatusCode == (int)HttpStatusCode.NotFound)
    {
        response.Redirect("/ErrorPage/Error404/");
    }
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");


app.Run();
