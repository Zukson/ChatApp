using AutoMapper;
using ChatApp.BackgroundServices;
using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyShop.API.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private void CleanImages()
        {
            DirectoryInfo dfTmp = new DirectoryInfo(@"E:\ChatApp\ChatApp\ChatApp\Images\TemporaryAvatars");
            DirectoryInfo dfA = new DirectoryInfo(@"E:\ChatApp\ChatApp\ChatApp\Images\Avatars");
            DirectoryInfo dfT= new DirectoryInfo(@"E:\ChatApp\ChatApp\ChatApp\Images\Thumbnails");

            foreach (var file in dfTmp.EnumerateFiles())
            {
                file.Delete();
            }
                foreach (var file in dfA.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (var file in dfT.EnumerateFiles())
                {
                    file.Delete();
                }
            
            }

       

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            InstallerExtensions.Configure(services, Configuration);
            services.AddSingleton(_ => Channel.CreateUnbounded<ImageMessage>());
            services.AddHostedService<ImageBackGroundService>();

            services.AddAutoMapper(typeof(Startup));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
                   //.SetIsOriginAllowed((host) => true));
            });
            services.AddSignalR();
          //  services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CleanImages();
            var swaggerSettings = new SwaggerSettings();
            Configuration.GetSection(nameof(SwaggerSettings))
                .Bind(swaggerSettings);
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerSettings.SwaggerEndpoint;
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerSettings.UIEndpoint, swaggerSettings.Description);
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");

      
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
