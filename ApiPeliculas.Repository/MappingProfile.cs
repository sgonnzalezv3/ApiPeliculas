using ApiPeliculas.Repository.DTO.SalaDeCine;
using ApiPeliculas.Repository.DTO.Usuario;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, UsuarioDto>();

            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(x => typeof(IMapFrom).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
            foreach (var type in types)
            {
                /* Crea instancia del dto */
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }

    }
}
