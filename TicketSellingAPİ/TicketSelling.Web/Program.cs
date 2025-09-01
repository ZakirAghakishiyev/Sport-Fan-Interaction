using Microsoft.EntityFrameworkCore;
using TicketSelling.Application.Interfaces;
using TicketSelling.Application.Services;
using TicketSelling.Core.Interfaces;
using TicketSelling.Infrastructure;
using TicketSelling.Infrastructure.Repositories;

namespace TicketSelling.Web;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Entered");
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<IMerchandiseService, MerchandiseManager>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        builder.Services.AddScoped<IMerchandiseService, MerchandiseManager>();
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
    }
}
