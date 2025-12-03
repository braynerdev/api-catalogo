using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Paginator.Conf;
using APICatalogo.Paginator.Produto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APICatalogo.Repositories.Produto
{
    public class ProdutoRepositorie : Repository<Produtos>, IProdutoRepositorie
    {
        public ProdutoRepositorie(AppDbContext context)
            : base(context)
        {
        }

        public async Task<bool> GetByExists(int id)
        {
            bool produto = await _context.Produtos.AnyAsync(p => p.Id == id);
            return produto;
        }

        public async Task<Produtos> GetById(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            return produto;
        }

        public async Task<PagdList<Produtos>> GetPaginator(ProdutosPaginator paginatorParams)
        {
            var produtos = await GetAllAsync();
            var name = paginatorParams.Name;
            var categoria = paginatorParams.Categoria;
            var active = paginatorParams.Active;

            produtos = produtos.Where(p => p.Active == active);

            if (!string.IsNullOrEmpty(name))
            {
                produtos = produtos.Where(p => p.Name.Contains(name));
            }

            if (categoria.HasValue)
            {
                produtos = produtos.Where(p => p.CategoriaId == categoria.Value);
            }

            var pageListdProdutos = PagdList<Produtos>.ToPagdList(produtos, c => c.Id, paginatorParams.OrderBy, paginatorParams.NumberPage, paginatorParams.PageSize);
            return pageListdProdutos;
        }
    }
}
