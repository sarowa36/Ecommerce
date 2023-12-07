using DataAccessLayer;
using Ecommerce.Middlewares;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    x.SerializerSettings.Converters.Add(new StringEnumConverter());
}); ;
builder.Services.AddDbContext<ADC>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddSpaStaticFiles(options => {
    options.RootPath = "dist";
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.Load("BusinessLayer"));
builder.Services.AddRepositories();
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints => {
    endpoints.MapControllerRoute("default", "api/{area}/{controller}/{action}/{id?}");
});

app.UseSpa(options => {
if(app.Environment.IsDevelopment())
        options.UseProxyToSpaDevelopmentServer("http://localhost:5173/");
});
app.Run();