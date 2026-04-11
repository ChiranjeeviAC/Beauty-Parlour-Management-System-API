
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;
using WebApplication1.Services;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;
using WebApplication1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 Register Services FIRST
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
    });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAuthCustomerRepository, AuthCustomerRepository>();
            builder.Services.AddScoped<IAuthCustomerService, AuthCustomerService>();
            builder.Services.AddScoped<IAuthStaffRepository, AuthStaffRepository>();
            builder.Services.AddScoped<IAuthStaffService, AuthStaffService>();


            builder.Services.AddScoped<IJwtService, JwtService>();

            var jwtSettings = builder.Configuration.GetSection("Jwt");

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
                        Encoding.UTF8.GetBytes(jwtSettings["Key"])
                    )
                };
            });
            var key = Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345_ABCDE");


            var app = builder.Build();

            // 🔹 Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();   // FIRST
            app.UseAuthorization();    // SECOND 🔥

            app.MapControllers();      // LAST

            app.Run();
        }
    }
}
