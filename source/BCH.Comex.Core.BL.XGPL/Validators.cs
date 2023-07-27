using System;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.Core.BL.XGPL.Validators
{
    public class CodigoPaisValido : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (new XgplService().GetNombrePais((int)value) != "")
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(validationContext.DisplayName + " no es un código de país válido");
        }
    }

    public class CodigoMonedaValido : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (new XgplService().GetNombreMoneda((int)value) != "")
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(validationContext.DisplayName + " no es válido");
        }
    }

    public class CodigoMonedaBancoCentralValido : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (new XgplService().GetNombreMonedaBancoCentral((int)value) != "")
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(validationContext.DisplayName + " no es válido");
        }
    }

    public class DiaHabil : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || MODGTAB0.EsFechaHabil((DateTime)value, new XgplService()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(validationContext.DisplayName + " debe ser día hábil.");
        }
    }

    public class Rango20Anos : ValidationAttribute
    {
        /// <summary>
        /// Retorna si una fecha está dentro de un rango de 20 años.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <remarks>Retorna verdadero en caso de nulo - si se necesita valor, usar en conjunción con Required</remarks>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || MODGTAB0.EsFecha2000((DateTime)value))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(validationContext.DisplayName + " sobrepasa el rango permitido (20 años) respecto del año actual");
        }
    }
}