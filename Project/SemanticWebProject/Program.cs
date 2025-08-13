using BLL.Options;
using BLL.Services.Abstractions;
using BLL.Services.Implementations;
using DAL;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

Console.WriteLine("Hello World");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      builder =>
                      {
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                          builder.AllowAnyOrigin();
                      });
});


builder.Services.AddScoped<IDataSourceService, DataSourceService>();
builder.Services.AddScoped<IScientistsService, ScientistsService>();

builder.Services.Configure<SourceOptions>(
    builder.Configuration.GetSection("SourceApi"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
