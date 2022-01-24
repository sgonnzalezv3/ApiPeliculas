using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.Helpers
{
    /* el model binder no puede asignar un arreglo de enteros a un listado de cualquier dato, porque es FromForm */
    /* el model binder por defecto no esta funcionando */

    /* esto permite mapear un arreglo de enteros proveniente de una peticion a un dato correspondiente del objeto */
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            /* propiedad que se va a trabajaer */
            var nombrePropiedad = bindingContext.ModelName;
            /* valor de dicha propiedad */
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if(proveedorDeValores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<T>(proveedorDeValores.FirstValue);
                /*pasarle el valor de ids ya deserializado*/
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "Valor inválido para tipo List<int>");
            }
            return Task.CompletedTask;
        }
    }
}
