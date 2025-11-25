using APICatalogo.DTOs;
using APICatalogo.DTOs.Produto;
using APICatalogo.Models;
using APICatalogo.Paginator.Produto;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services.Produto
{
    public interface IProdutoService
    {
        Task<Response<ProdutoResponseDTO>> GetById(int id);
        Task<Response<ProdutoResponseDTO>> GetPaginator(ProdutosPaginator paginatorParams);
        Task<Response<string>> Created(ProdutoRequestDTO produtoDTO);
        Task<Response<string>> Put(int id, ProdutoRequestDTO produtoDTO);
        Task<Response<string>> UpdateEstoque(int id, int estoque);
    }
}
