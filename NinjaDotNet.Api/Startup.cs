using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NinjaDotNet.Api.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NinjaDotNet.Api.Contracts;
using NinjaDotNet.Api.Mappings;
using NinjaDotNet.Api.Services;

namespace NinjaDotNet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NinjaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<NinjaDbContext>();

            services.AddCors(c =>
            {
                c.AddPolicy("NinjaDotNetCorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            services.AddAutoMapper(typeof(Maps));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Contact = new OpenApiContact
                    {
                        Email = "bryan@vesuviustech.com",
                        Name = "Bryan Mudge",
                        Url = new Uri("https://www.ninjadotnet.com/")
                    },
                    Title = "Ninja Dot Net Api",
                    Version = "v1",
                    Description = "API For my personal blogging website."
                });

                var xFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xPath = Path.Combine(AppContext.BaseDirectory, xFile);
                c.IncludeXmlComments(xPath);
            });

            services.AddSingleton<ILoggerService, LoggerService>();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogCommentRepository,BlogCommentRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NinjaDbContext context,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ninja Dot Net Api");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseCors("NinjaDotNetCorsPolicy");

            SeedData.Seed(userManager, roleManager).Wait();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
