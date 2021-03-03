using ChatApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Installers
{
    public class DataInstaller : IInstaller
    {
        public void ConfigureServcies(IConfiguration configuration, IServiceCollection services)
        {
            //services.AddDbContext<DataContext>(options =>
            //     options.UseInMemoryDatabase(
            //         configuration.GetConnectionString("DefaultConnection")));
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<DataContext>(options =>
            {

                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                services.AddDatabaseDeveloperPageExceptionFilter();
            });

            services.AddDefaultIdentity<IdentityUser>(options => {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                


                })

                .AddEntityFrameworkStores<DataContext>();
        }
    }
}
