using Microsoft.EntityFrameworkCore;
using ShopyStock.Application.Interfaces;
using ShopyStock.Application.Services;
using ShopyStock.Domain.Interfaces.Repositories;
using ShopyStock.Infraestructure.Context;
using ShopyStock.Infraestructure.Repositories;

namespace ShopyStock.API;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<StockDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        //Actualizacion de la DB a la hora de desplegar, lo hice para el despliegue con Docker
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<StockDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}