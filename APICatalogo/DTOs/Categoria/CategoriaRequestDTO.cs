using APICatalogo.Validator;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Categoria
{
    public class CategoriaRequestDTO
    {
        [Required]
        [StringLength(80)]
        public string? Nome { get; init; }

        [Required]
        [StringLength(300)]
        [ValidatorExtensionArquivo]
        public string? ImagemUrl { get; init; }
    }
}
