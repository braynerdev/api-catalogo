using APICatalogo.Models;
using APICatalogo.Validator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.DTOs.Produto
{
    public class ProdutoRequestDTO
    {

        [Required(ErrorMessage = "Nome não pode ser nulo!")]
        [StringLength(80)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descrição não pode nulo!")]
        [StringLength(300)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preço não ode ser nulo!")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [StringLength(300)]
        [ValidatorExtensionArquivo]
        public string? ImagemUrl { get; set; }
        
        [Required]
        public int CategoriaId { get; set; }

    }
}
