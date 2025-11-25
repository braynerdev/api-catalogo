using APICatalogo.Repositories.Categoria;
using APICatalogo.Repositories.Produto;

namespace APICatalogo.Repositories
{
    public interface IUnitOfWork
    {
        ICategoriaRepositorie CategoriaRepositorie {  get; }
        IProdutoRepositorie ProdutoRepositorie { get; }
        Task commitAsync();
    }
}
