using APICatalogo.Models;
using APICatalogo.Paginator.Categoria;
using APICatalogo.Paginator.Conf;

namespace APICatalogo.Repositories.Categoria
{
    public interface ICategoriaRepositorie : IRepository<Categorias>
    {
        Task<PagdList<Categorias>> GetCategoriasPaginator(CategoriasPaginator categoriasParams);
    }
}
