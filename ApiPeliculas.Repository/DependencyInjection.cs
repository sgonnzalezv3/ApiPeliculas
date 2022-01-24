using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            /* Enviando los servicios a Program para inyectarlos */
            services.AddAllService();
            return services;
        }
    }
}
