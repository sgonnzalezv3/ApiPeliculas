using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActoresDto;
using ApiPeliculas.Repository.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public static class InjectService
    {
        /* Inyectando todos los servicios del ensamblado */
        internal static IServiceCollection AddAllService(this IServiceCollection services)
        {
            var allProviderTypes = Assembly.GetAssembly(typeof(IService<Actor, ActorDto>))
                .GetTypes().Where(x => x.Namespace != null).ToList();
            foreach(var init in allProviderTypes.Where(x=> x.IsInterface))
            {
                var impl = allProviderTypes.FirstOrDefault(x => x.IsClass && init.Name.Substring(1) == x.Name);
                if (impl != null) services.AddTransient(init, impl);
            }
            return services;
        }
    }
}
