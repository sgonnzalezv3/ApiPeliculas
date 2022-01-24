using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace ApiPeliculas.Repository.Azure
{
    public class AlmacenadorArchivosAzure : IAlmacenadorArchivos
    {
        private readonly string _connectionString;
        
        public AlmacenadorArchivosAzure(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage");
        }
        public async Task BorrarArchivo(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
                return;
            var cliente = new BlobContainerClient(_connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var archivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(archivo);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor,contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            /*Enviar el arreglo de bytes a azure*/
            var cliente = new BlobContainerClient(_connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();

            cliente.SetAccessPolicy(PublicAccessType.Blob);

            var archivoNombre = $"{Guid.NewGuid()}{extension}";
            var blob = cliente.GetBlobClient(archivoNombre);

            /* pasarle el contentype para que sepa que son imagenes */

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeader;

            /* Carga de archivo */
            await blob.UploadAsync(new BinaryData(contenido), blobUploadOptions);
            return blob.Uri.ToString();
        }
    }
}
