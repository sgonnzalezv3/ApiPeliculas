using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.SalaDeCine
{
    public class SalaDeCineCercanoFiltroDto : EntityDto
    {
        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }
        public int distanciaEnKm = 10;
        public int distanciaMaximaEnKm = 50;


        /* No permitir distancias mayores a 50km */
        public int DistanciaEnKm
        {
            get
            {
                return distanciaEnKm;
            }
            set
            {
                distanciaEnKm = (value > distanciaMaximaEnKm) ? distanciaMaximaEnKm : value;
            }
        }
    }
}
