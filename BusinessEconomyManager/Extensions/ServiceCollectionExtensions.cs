using BusinessEconomyManager.Data;
using BusinessEconomyManager.Data.Repositories.Implementations;
using BusinessEconomyManager.Services.Implementations;
using BusinessEconomyManager.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace BusinessEconomyManager.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Program));
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddCors((options) =>
            {
                options.AddDefaultPolicy(builder => { builder.WithOrigins((isDevelopment) ? "http://localhost:4200" : "https://businesseconomymanager.web.app").AllowAnyHeader().AllowAnyMethod(); });
            });

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAppUserServices, AppUserServices>();
            services.AddScoped<IBusinessServices, BusinessServices>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
        }

        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
        }
    }
}
