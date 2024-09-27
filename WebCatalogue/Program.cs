using CatalogueApp.Data.Data;
using CatalogueApp.Data.Interfaces;
using CatalogueApp.Data.Repositories;
using CatalogueApp.Interfaces;
using CatalogueApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCatalogue.Common.Mappings;
using WebCatalogue.Controllers;
using WebCatalogue.Infrustructure.MiddleWare.ErrorHandling;
using WebCatalogue.Validators;
using WebCatalogue.ViewModels;

var builder = WebApplication.CreateBuilder();


var con = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TestContext>(options => options.UseSqlite(con));

builder.Services.AddScoped<TestContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    
})
.AddEntityFrameworkStores<TestContext>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddTransient<ProductController>();
builder.Services.AddTransient<CategoryController>();

builder.Services.AddTransient<IValidator<CategoryViewModel>, CategoryViewModelValidator>();

builder.Services.AddTransient<IValidator<ProductViewModel>, ProductViewValidator>();

builder.Services.AddTransient<IValidator<UserViewModel>, UserViewValidator>();

builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(CatalogueMapProfile));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();




var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseDeveloperExceptionPage();
app.UseRouting();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();


