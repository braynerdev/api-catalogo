using APICatalogo.Paginator.Conf;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Paginator.Produto
{
    public class ProdutosPaginator : PaginatorParamiters
    {
        public string? Name { get; set; }
        public int? Categoria { get; set; }
    }
}
