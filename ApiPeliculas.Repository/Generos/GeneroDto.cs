using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository
{
    /* Cada clase que creemos va a heredar de IMapFrom, el cual realiza el mapeo de automapper en la propia clase y EntityDto que es una generalizacion
     * ya que todos van a tener id
     */
    public class GeneroDto : EntityDto,IMapFrom
    {
        public string Nombre { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApiPeliculas.Dominio.Genero, GeneroDto>().ReverseMap();
        }
    }
}
