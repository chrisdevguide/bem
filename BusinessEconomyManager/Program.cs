using BusinessEconomyManager.Extensions;
using BusinessEconomyManager.Middlewares;

namespace BusinessEconomyManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices(builder.Configuration, builder.Environment.IsDevelopment());
            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();

            app.UseCors();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ApiExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}