using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;
using OrdersAPI.ServiceHttpClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("OrderConnection");

builder.Services.AddDbContext<OrderContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("OrderConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpClient<IProductServiceHttpClient, ProductServiceHttpClient>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
