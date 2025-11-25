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

        public async Task<PagdList<Produtos>> GetProdutosAsync(ProdutosPaginator paginatorParams)
        {
            var produtos = await GetAllAsync();
            var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();
            var pageListdProdutos = PagdList<Produtos>.ToPagdList(produtosOrdenados, paginatorParams.NumberPage, paginatorParams.PageSize);
            return pageListdProdutos;
        }


        public async Task<PagdList<Produtos>> GetProdutosFilterPrecoAsync(ProdutoFilterPreco produtoFilterPreco)
        {
            var produtos = await GetAllAsync();

            if (produtoFilterPreco.Preco.HasValue && !string.IsNullOrEmpty(produtoFilterPreco.Criterios))
            {
                if (produtoFilterPreco.Criterios.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtoFilterPreco.Preco);
                }
                else if (produtoFilterPreco.Criterios.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtoFilterPreco.Preco);
                }
                else if (produtoFilterPreco.Criterios.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtoFilterPreco.Preco);
                }
                else if (produtoFilterPreco.Criterios.Equals("menorigual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco <= produtoFilterPreco.Preco);
                }
                else if (produtoFilterPreco.Criterios.Equals("maiorigual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco >= produtoFilterPreco.Preco);
                }
            }
            var produtosFiltrados = PagdList<Produtos>.ToPagdList(produtos.AsQueryable(), produtoFilterPreco.NumberPage, produtoFilterPreco.PageSize);
            return produtosFiltrados;
        }
    }
}
