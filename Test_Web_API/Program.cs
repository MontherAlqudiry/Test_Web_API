using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Test_Web_API.Data;
using Test_Web_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    // Other Swagger configuration...

//    // Register the custom document filter
//    c.DocumentFilter<SwaggerExcludeFilter>();
//});

//builder.Services.AddSwaggerGen(c =>
//{
//    // Define your Swagger document
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

//    // Register the custom document filter
//    c.DocumentFilter<SwaggerExcludeFilter>();
//});
    

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
