using APICatalogo.Paginator.Conf;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Paginator.Categoria
{
    public class CategoriasPaginator : PaginatorParamiters
    {
        [StringLength(150, ErrorMessage = "O nome pode ter no maximo 150 caracteres")]
        public string? Name {  get; init; }
    }
}
