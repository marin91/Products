using Domain;
using Hellang.Middleware.ProblemDetails;
using Infrastructure.SqlServer;
using Infrastructure.SqlServer.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Products.Api;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterProblemDetailsHandling();

builder.Services.RegisterTheProductValidator();

builder.Services.Configure<ProductsConnectionOptions>(
    builder.Configuration.GetSection(ProductsConnectionOptions.ConfigSection));

builder.Services.RegisterProductRepositories()   
    .RegisterTheProductMappers()
    .RegisterTheProductInventory();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Customize automatic 400 responses
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://api.yourapp.com/errors/validation",
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path,
            };

            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

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

app.UseProblemDetails();

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
