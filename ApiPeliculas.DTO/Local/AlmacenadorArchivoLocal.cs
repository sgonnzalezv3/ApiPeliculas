using ApiPeliculas.Persitencia.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.DTO.Local
{
    public class AlmacenadorArchivoLocal : IAlmacenadorArchivos
    {
        /* 
         *env : nos permite obtener la ruta de nuestro wwwroot para colocar archivos en dicha carpeta
         *httpCOntextAccessor: 
         */
        public AlmacenadorArchivoLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {

        }

        public Task BorrarArchivo(string ruta, string contenedor)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            throw new NotImplementedException();
        }

        public Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            throw new NotImplementedException();
        }
    }
}
