using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICatalogo.Validator;

namespace APICatalogo.Models;
public class Categorias : Base
{


    [Column("name", TypeName = "varchar(150)")]
    public string Name { get; set; }

    [Column("image_url", TypeName = "varchar(300)")]
    [ValidatorExtensionArquivo]
    public string? ImagemUrl { get; set; }

    public List<Produtos> Produtos { get; set; } = new();
}
