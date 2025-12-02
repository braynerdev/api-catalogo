using APICatalogo.Validator;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Categoria
{
    public class CategoriaRequestDTO
    {
        [Required]
        [StringLength(150, ErrorMessage = "O nome pode ter no maximo 150 caracteres")]
        public string? Name { get; init; }

        [Required]
        [StringLength(300, ErrorMessage = "A url da imagem pode ter no maximo 300 caracteres")]
        [ValidatorExtensionArquivo]
        public string? ImagemUrl { get; init; }
    }
}
