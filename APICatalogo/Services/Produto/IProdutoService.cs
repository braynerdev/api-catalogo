using APICatalogo.DTOs;
using APICatalogo.DTOs.Produto;
using APICatalogo.Models;
using APICatalogo.Paginator.Conf;
using APICatalogo.Paginator.Produto;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services.Produto
{
    public interface IProdutoService
    {
        Task<Response<ProdutoResponseDTO>> GetById(int id);
        Task<Response<ProdutoPaginatorResponseDTO>> GetPaginator(ProdutosPaginator paginatorParams);
        Task<Response<ProdutoResponseDTO>> Created(ProdutoRequestDTO produtoDTO);
        Task<Response<ProdutoResponseDTO>> Put(int id, ProdutoRequestDTO produtoDTO);
        Task<Response<ProdutoResponseDTO>> AdicionarEstoque(int id, int estoque);
        Task<Response<ProdutoResponseDTO>> RemoverEstoque(int id, int estoque);
        Task<Response<ProdutoResponseDTO>> Activate(int id);
        Task<Response<ProdutoResponseDTO>> Deactivate(int id);
    }
}
