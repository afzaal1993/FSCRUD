using Events;
using Events.Data;
using Events.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
builder.Services.AddSingleton<MongoDBContext>(serviceProvider =>
{
    return new MongoDBContext(mongoDbSettings.ConnectionString, mongoDbSettings.Name);
});

//RabbitMQ CAP Library
builder.Services.AddCap(options =>
{
    options.UseMongoDB(options =>
    {
        options.DatabaseConnection = mongoDbSettings.ConnectionString;
        options.DatabaseName = mongoDbSettings.Name;
    });
    options.UseRabbitMQ(rabbitMqoptions =>
    {
        rabbitMqoptions.HostName = "localhost";
        rabbitMqoptions.UserName = "admin";
        rabbitMqoptions.Password = "admin123456";
    });
    options.DefaultGroupName = "fs-crud-proj";
    options.UseDashboard();
});

builder.Services.AddTransient<EventReceiver>();

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
