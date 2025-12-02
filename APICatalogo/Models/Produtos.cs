using APICatalogo.Validator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;
public class Produtos : Base
{
    [Column("name", TypeName = "varchar(150)")]
    public string Name { get; set; }

    [Column("descricao", TypeName = "varchar(300)")]
    public string Descricao { get; set; }


    [Column("preco",TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Column("image_url", TypeName = "varchar(300)")]
    [ValidatorExtensionArquivo]
    public string? ImagemUrl { get; set; }

    [Column("qtd_estoque", TypeName = "decimal(10,0)")]
    public int Estoque { get; set; }

    [Column("categoria_id")]
    public int CategoriaId { get; set; }

    public Categorias Categoria { get; set; }

}
