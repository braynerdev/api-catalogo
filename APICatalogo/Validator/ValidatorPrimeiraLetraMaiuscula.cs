using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validator
{
    public class ValidatorPrimeiraLetraMaiuscula : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeiraLetra = value.ToString()[0].ToString();

            if(!(primeiraLetra == primeiraLetra.ToUpper()))
            {
                return new ValidationResult($"Primeira letra não é maiuscula! {value.ToString()}");
            }

            return ValidationResult.Success;
        }
    }
}
