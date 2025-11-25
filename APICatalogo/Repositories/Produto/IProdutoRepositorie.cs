using APICatalogo.Models;
using APICatalogo.Paginator.Conf;
using APICatalogo.Paginator.Produto;

namespace APICatalogo.Repositories.Produto
{
    public interface IProdutoRepositorie : IRepository<Produtos>
    {
        Task<PagdList<Produtos>> GetProdutosAsync(ProdutosPaginator paginatorParams);
        Task<PagdList<Produtos>> GetProdutosFilterPrecoAsync(ProdutoFilterPreco produtoFilterPreco);
        Task<IEnumerable<Produtos>> GetProdutosCategoriaAsync(int id);
    }
}
