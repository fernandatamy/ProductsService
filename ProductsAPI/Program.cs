using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Data.Repository;
using ProductsAPI.EventProcessor;
using ProductsAPI.RabbitMqClient;
using ProductsAPI.RabbitMqSubscriber;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ProductConnection");
builder.Services.AddDbContext<ProductContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();

builder.Services.AddHostedService<RabbitMqSubscriber>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductContext>();
    db.Database.Migrate();
}

app.Run();
