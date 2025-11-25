using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Produto
{
    public class ProdutoUpdateRequaestDTO : IValidatableObject
    {
        [Range(1, 999999, ErrorMessage = "O ESTOQUE DEVE CONTER PRODUTOS DE 1 ATE 999999")]
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataCadastro.Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("Data cadastro deve ser maior que a data atual!",
                    new[] {nameof(DataCadastro)}); 
            }
        }
    }
}
