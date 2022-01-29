using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.Usuario
{
    public class EditarRolDto : EntityDto
    {
        public string UsuarioId { get; set; }
        public string NombreRol { get; set; }
    }
}
