using ApiPeliculas.Repository.Azure;

namespace ApiPeliculas.Local
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        /* 
* env :  nos ayudara a obtener la ruta donde se encuentra el wwwroot
* httpContextAccessor : determinar el dominio donde tenemos la web api, para construir la url que se va a almacenar en la bd de actore
*/
        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if(ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);

                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";

            // el contenedor es como una carpeta, combinanos la direccion del root con el de la carpeta
            string folder = Path.Combine(env.WebRootPath, contenedor);
            /* Si no existe el directorio  */
            if (!Directory.Exists(folder))
            {
                /* Crearlo */
                Directory.CreateDirectory(folder);
            }
            string ruta = Path.Combine(folder, nombreArchivo);
            /* Escribiendo en el disco duro el contenido del archivo */
            await File.WriteAllBytesAsync(ruta, contenido);
            // HTTP O HTTPS                                             HOST
            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlParaBd = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\", "/");
            return urlParaBd;
        }
    }
}
