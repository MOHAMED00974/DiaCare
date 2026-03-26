using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using System.Text; 
using DiaCare.Domain.Entities;
using DiaCare.Infrastructure.Data;
using DiaCare.Domain.Interfaces;
using DiaCare.Infrastructure.Repositories;
using DiaCare.Application.Interfaces;
using DiaCare.Application.Services;
using DiaCare.Application.Helpers;
using DiaCare.Application;
using DiaCare.Application.Profiles;


namespace DiaCare.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        //Extension Method => Encapsulation
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>
                (options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                }
               ));

            // 2. User Setting (Identity)
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 8;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // 3.  JWT Setting
            var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key ?? ""))
                };
            });

            // 4.  (Dependency Injection)
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddHttpClient();

            // 5. General Setting
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
            services.AddAutoMapper(typeof(AuthProfile).Assembly);

            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddMemoryCache();



            return services;
        }
    }

}

