using core.Helpers;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using core.Data;
using static Dapper.SqlMapper;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CoreDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
builder.Services.AddSingleton<MongoDBContext>(serviceProvider =>
{
    return new MongoDBContext(mongoDbSettings.ConnectionString, mongoDbSettings.Name);
});

//RabbitMQ CAP Library
//builder.Services.AddCap(options =>
//{
//    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
//    options.UseRabbitMQ(rabbitMqoptions =>
//    {
//        rabbitMqoptions.HostName = "localhost";
//        rabbitMqoptions.UserName = "admin";
//        rabbitMqoptions.Password = "admin123456";
//    });
//    options.UseDashboard();
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
