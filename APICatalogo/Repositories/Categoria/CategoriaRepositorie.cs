using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Paginator.Categoria;
using APICatalogo.Paginator.Conf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APICatalogo.Repositories.Categoria
{
    public class CategoriaRepositorie : Repository<Categorias>, ICategoriaRepositorie
    {
        public CategoriaRepositorie(AppDbContext context)
            : base(context)
        {
        }

        public async Task<PagdList<Categorias>> GetCategoriasPaginator(CategoriasPaginator categoriasParams)
        {
            var categorias = await GetAllAsync();
            var name = categoriasParams.Name;
            var active = categoriasParams.Active;

            categorias = categorias.Where(c => c.Active == active);

            if (!name.IsNullOrEmpty())
            {
                categorias = categorias
                                .Where(c => c.Name.Contains(name));
            }
                categorias = categorias.AsQueryable();
            var pagListCategorias = PagdList<Categorias>.ToPagdList(categorias, c => c.Id, categoriasParams.OrderBy, categoriasParams.NumberPage, categoriasParams.PageSize);
            return pagListCategorias;
        }
    }
}
