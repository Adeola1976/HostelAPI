using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Core.Repository.Implementation;
using HostelAPI.Core.Services.Abstraction;
using HostelAPI.Core.Services.Implementation;
using HostelAPI.Database.Context;
using HostelAPI.Database.Seeding;
using HostelAPI.Extension;
using HostelAPI.Model;
using HostelAPI.Model.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HostelAPI
{
    public class Startup
    {
        public  IWebHostEnvironment Environment { get; }
        public static  IConfiguration StaticConfig { get; private  set; }
        public Startup(IConfiguration configuration,IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            StaticConfig = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            //configure entityframeworkcore with PostgreSQL database connection
                 services.AddDbContext<HDBContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("default")));
                 services.AddMvc()
                .AddSessionStateTempDataProvider();
                 services.AddSession();








            //Mail service being registered

            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthetication, Authetication>();
          
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
         
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
          
       
            services.AddControllers();
            services.ConfigureAuthentication();
            services.ConfigureIdentity();
            services.AddSwagger();
            // Register Dependency Injection Service Extension
            services.AddDependencyInjection();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, HDBContext  myContactContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HostelAPI v1"));
            }

            Seeder.Seed(roleManager, userManager, myContactContext).Wait();
            app.UseSession();
           


            app.UseStaticFiles();


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
