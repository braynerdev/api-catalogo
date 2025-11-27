using APICatalogo.Models;
using APICatalogo.Paginator.Conf;
using APICatalogo.Paginator.Produto;

namespace APICatalogo.Repositories.Produto
{
    public interface IProdutoRepositorie : IRepository<Produtos>
    {
        Task<PagdList<Produtos>> GetPaginator(ProdutosPaginator paginatorParams);
        Task<Produtos> GetById(int id);
        Task<bool> GetByExists(int id);
    }
}
