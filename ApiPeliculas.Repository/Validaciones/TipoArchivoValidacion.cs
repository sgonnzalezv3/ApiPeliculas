using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.Validaciones
{
    internal class TipoArchivoValidacion : ValidationAttribute
    {
        private readonly string[] _tiposValidos;
        public TipoArchivoValidacion(string[] tiposValidos)
        {
            _tiposValidos = tiposValidos;
        }

        public TipoArchivoValidacion(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
                _tiposValidos = new string[] { "image/jpeg", "image/png", "image/gif" };
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            /* transformacion */
            IFormFile formFile = value as IFormFile;

            /* Si la transformacion no es exitosa */
            if (formFile == null)
            {
                return ValidationResult.Success;
            }
            if (!_tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes { string.Join(", ", _tiposValidos)}");
            }
            return ValidationResult.Success;
        }
    }
}
