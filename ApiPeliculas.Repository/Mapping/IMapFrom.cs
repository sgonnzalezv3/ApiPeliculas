﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.Mapping
{
    public interface IMapFrom
    {
        void Mapping(Profile profile);
    }
}
