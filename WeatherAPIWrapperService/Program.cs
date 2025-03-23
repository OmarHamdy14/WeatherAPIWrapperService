using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherAPIWrapperService.Data.Services.Implementation;
using WeatherAPIWrapperService.Data.Services.Interfaces;
using WeatherAPIWrapperService.Models;

namespace WeatherAPIWrapperService
{
    public class Program
    {
        public static void Main(string[] args)
        {


            // Add services to the container.
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddHttpClient("WeatherClient", client =>
            {
                client.BaseAddress = new Uri("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
}