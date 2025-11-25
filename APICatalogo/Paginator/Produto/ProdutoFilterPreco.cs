using APICatalogo.Paginator.Conf;

namespace APICatalogo.Paginator.Produto
{
    public class ProdutoFilterPreco : PaginatorParamiters
    {
        public decimal? Preco { get; set; }
        public string? Criterios { get; set; }
    }
}
