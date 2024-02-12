using CatalogueApp.Data.Data;
using CatalogueApp.Data.Interfaces;
using CatalogueApp.Data.Repositories;
using ClassLibrary2.Interfaces;
using ClassLibrary2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCatalogue.Common.Mappings;
using WebCatalogue.Controllers;

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

builder.Services.AddAutoMapper(typeof(CatalogueMapProfile));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();




var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseRouting();

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


