using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Repository
{
    /* se hereda de ValidationAttribute para poder reutilizarlo en otras validaciones */
    public class TamanoArchivoValidacion : ValidationAttribute
    {
        private readonly int _pesoMaximoMb;
        public TamanoArchivoValidacion(int pesoMaximoMb)
        {
            _pesoMaximoMb = pesoMaximoMb;
        }

        protected override ValidationResult IsValid(object value , ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }
            /* transformacion */
            IFormFile formFile = value as IFormFile;
            
            /* Si la transformacion no es exitosa */
            if(formFile == null)
            {
                return ValidationResult.Success;
            }
            /* si el tamaño es maximo a: */
            if(formFile.Length > _pesoMaximoMb * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {_pesoMaximoMb}");
            }
            return ValidationResult.Success;
        }
    }
}
