using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Installers
{
    public interface IInstaller
    {
        public void ConfigureServcies(IConfiguration configuration, IServiceCollection services);
    }
}
