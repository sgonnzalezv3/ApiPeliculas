﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.SalaDeCine
{
    public class SalaDeCineCercanoDto
    {
        public int Id
        {
            get; set;
        }
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double DistanciaEnMetros { get; set; }
    }
}
