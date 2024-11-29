
using Ecommerce_app.Context;
using Ecommerce_app.Exception_handler;
using Ecommerce_app.jwt_generator;
using Ecommerce_app.Mapper;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Ecommerce_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthCore API", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token in this field",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
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
                    };

                c.AddSecurityRequirement(securityRequirement);
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Signkey"]))

                };
            }     
            );
            builder.Services.AddDbContext<Application_context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));

            });

            builder.Services.AddAutoMapper(typeof(Mapper_profile));
            builder.Services.AddScoped<IAuth_service, Auth_service>();
            builder.Services.AddScoped<IGenerate_jwt, Generate_jwt>();
            builder.Services.AddScoped<IProduct_service, Product_service>();
            builder.Services.AddScoped<Icategory_service, Category_service>();
            builder.Services.AddScoped<Iwishlist, Wish_list_service>();
            builder.Services.AddScoped<Iuser_service, User_service>();
            builder.Services.AddScoped<ICartitem_service, Cartitem_service>();
            builder.Services.AddScoped<IOrder_service, Order_service>();
            builder.Services.AddScoped<IAdmin_service, Admin_service>();
            builder.Services.AddScoped<Global_exception_handler>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthCore API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<Global_exception_handler>();


            app.MapControllers();

            app.Run();
        }
    }
}
