using DataAccessLayer;
using DataAccessLayer.JsonData;
using FluentValidation;
using IdentityLayer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ServiceLayer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddDbContext<ADC>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddMyIdentity();

builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = "dist";
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddValidatorsFromAssembly(Assembly.Load("BusinessLayer"));

builder.Services.AddDataAccessLayerServices();
builder.Services.AddApplicationServices();
builder.Services.AddCors(options => {
    options.AddPolicy("iyzico", policy =>
    {
        policy.WithOrigins("https://sandbox-api.iyzipay.com", "https://api.iyzipay.com").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // I will add specific error page per Role
    // app.UseExceptionHandler("/Home/Error"); 
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSpaStaticFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("iyzico");

app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("defaultArea", "api/{area}/{controller}/{action}/{id?}");
    endpoints.MapControllerRoute("defaultController", "api/{controller}/{action}/{id?}");
});

app.UseSpa(options =>
{
    if (app.Environment.IsDevelopment())
        options.UseProxyToSpaDevelopmentServer("http://localhost:5173/");
});

app.Run();