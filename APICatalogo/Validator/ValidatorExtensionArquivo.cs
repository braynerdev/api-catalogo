using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validator {
    public class ValidatorExtensionArquivo : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string[] ext = { "jpg", "png", "pdf" };
            string valueString = value.ToString()!;

            if (!(valueString.Contains(".")))
            {
                return new ValidationResult("Arquivo sem extensão!");
            }

            string[] splitValue = valueString.Split(".");
            string valueExt = splitValue[splitValue.Length - 1];

            if (!(ext.Contains(valueExt)))
            {
                return new ValidationResult($"Arquivo invalido, só aceitamos .pdf, .png e .jpg!");
            }
            return ValidationResult.Success;    
        }
    }
}
