using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Produto
{
    public class EstoqueProdutoDTO
    {
        [Required(ErrorMessage = "Valor do estoque não pode ser nulo")]
        [Range(1, 1000, ErrorMessage = "Valor do estoque deve ser entre 1 e 1000")]
        public int Estoque {  get; set; }
    }
}
