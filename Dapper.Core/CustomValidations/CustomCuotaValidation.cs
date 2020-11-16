using System;
using System.ComponentModel.DataAnnotations;
using Dapper.Core.Model;

namespace Dapper.Core.CustomValidations
{
    public class CustomCuotaValidation : ValidationAttribute
    {
        public CustomCuotaValidation()
        {

        }

        protected override ValidationResult IsValid(object value,
       ValidationContext validationContext)
        {
            var asignaciones = (Asignaciones)validationContext.ObjectInstance;
            if (asignaciones.AplicaInteres == null || asignaciones.AplicaInteres == false)
            {
                if (asignaciones.CuotaMinima == null || asignaciones.CuotaMinima <= 0)
                    return new ValidationResult("Debe ingresar el monto de la cuota.");
            }


            return ValidationResult.Success;
        }

    }
}
