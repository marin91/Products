using Domain;
using Infrastructure.SqlServer;
using Infrastructure.SqlServer.Options;
using Microsoft.OpenApi.Models;
using Products.Api;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterTheProductValidator();

builder.Services.Configure<ProductsConnectionOptions>(
    builder.Configuration.GetSection(ProductsConnectionOptions.ConfigSection));

builder.Services.RegisterProductRepositories()   
    .RegisterTheProductMapper()
    .RegisterTheProductInventory();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Products API",
        Description = "An ASP.NET Core Web API for inventory management and store product handling."
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
