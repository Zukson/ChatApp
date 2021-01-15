using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ChatApp.Installers
{
    public static class InstallerExtensions
    {
        public static void Configure( this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var intstallers = typeof(Startup).Assembly.
                ExportedTypes.Where(x => x.IsClass &&typeof(IInstaller).IsAssignableFrom(x)).
                Select(x => Activator.CreateInstance(x)).Cast<IInstaller>().ToList();
            intstallers.ForEach(x => x.ConfigureServcies(configuration, serviceCollection));


        }
    }
}
