using APICatalogo.Validator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.DTOs.Produto
{
    public class ProdutoUpdateResponseDTO
    {
        public int ProdutoId { get; set; }

        public string? Name { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? ImagemUrl { get; set; }

        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
    }
}
