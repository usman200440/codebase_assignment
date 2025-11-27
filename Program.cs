
using codebase_Assignment.Interface;
using codebase_Assignment.Models;
using codebase_Assignment.Services;
using Microsoft.EntityFrameworkCore;

namespace codebase_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IotpInterface, OtpService>();
            builder.Services.AddScoped<IUserInterface, UserService>();
            builder.Services.AddScoped<IAddress, AddressService>();
            builder.Services.AddScoped<IUserpin, UserPinService>();
            builder.Services.AddDbContext<CodebaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("codebasecors", builder =>
                {
                    builder.WithOrigins().WithOrigins().WithHeaders().WithMethods();
                });
            });
            // Add services to the container.

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
            app.UseCors("codebasecors");

            app.MapControllers();

            app.Run();
        }
    }
}
