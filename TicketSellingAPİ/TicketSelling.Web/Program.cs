using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TicketSelling.Application.Interfaces;
using TicketSelling.Application.Services;
using TicketSelling.Core.Entities;
using TicketSelling.Core.Interfaces;
using TicketSelling.Infrastructure;
using TicketSelling.Infrastructure.Email;
using TicketSelling.Infrastructure.Repositories;

namespace TicketSelling.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
        builder.Services.AddScoped<IMerchandiseService, MerchandiseManager>();
        builder.Services.AddScoped<ICardDetailsService, CardDetailsManager>();
        builder.Services.AddScoped<IUserSavedCardService, UserSavedCardManager>();
        builder.Services.AddScoped<IOrderService, OrderManager>();
        builder.Services.AddScoped<IPaymentService, PaymentManager>();
        builder.Services.AddScoped<IStadiumService, StadiumManager>();
        builder.Services.AddScoped<ISectorService, SectorManager>();
        builder.Services.AddScoped<ISeatService, SeatManager>();
        builder.Services.AddScoped<IMatchService, MatchManager>();
        builder.Services.AddScoped<ITicketService, TicketManager>();
        builder.Services.AddScoped<IMatchSectorPriceRepository, MatchSectorPriceRepository>();
        builder.Services.AddSingleton<IEmailSender, EmailSender>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketSelling API", Version = "v1" });

            // ✅ Add JWT Authentication
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token.\r\nExample: \"Bearer eyJhbGci...\""
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        // Add Identity
        builder.Services.AddIdentity<AppUser, IdentityRole<int>>() // your AppUser with int key
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        // JWT configuration
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
            };
        });

        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        builder.Services.AddScoped<IMerchandiseService, MerchandiseManager>();
        builder.Services.AddIdentityCore<AppUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole<int>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddSignInManager<SignInManager<AppUser>>()
        .AddDefaultTokenProviders();


        builder.Services.AddAutoMapper(typeof(Program));

        
        var app = builder.Build();
        app.UseCors("AllowAll");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
