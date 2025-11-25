using APICatalogo.Context;
using APICatalogo.Repositories.Categoria;
using APICatalogo.Repositories.Produto;
using APICatalogo.Repositories;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoriaRepositorie _categoriaRepositorie;

        private IProdutoRepositorie _produtoRepositorie;

        public AppDbContext Context;

        public UnitOfWork(AppDbContext context)
        {
            Context = context;
        }

        public ICategoriaRepositorie CategoriaRepositorie
        {
            get
            {
                return _categoriaRepositorie = _categoriaRepositorie ?? new CategoriaRepositorie(Context);
            }
        }
        public IProdutoRepositorie ProdutoRepositorie
        {
            get
            {
                return _produtoRepositorie = _produtoRepositorie ?? new ProdutoRepositorie(Context);
            }
        }

        public async Task commitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
