using APICatalogo.DTOs;
using APICatalogo.DTOs.Categoria;
using APICatalogo.Models;
using APICatalogo.Paginator.Categoria;
using APICatalogo.Paginator.Conf;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services.Categoria
{
    public interface ICategoriaService
    {
        Task<Response<CategoriaResponseDTO>> GetById(int id);
        Task<Response<PagdList<Categorias>>> GetPaginator(CategoriasPaginator categoriasPaginator);
        Task<Response<CategoriaResponseDTO>> Created(CategoriaRequestDTO categoriaRequestDTO);
        Task<Response<CategoriaResponseDTO>> Put(int id, CategoriaRequestDTO categoriaRequestDTO);
        Task<Response<CategoriaResponseDTO>> Deactivate(int id);
        Task<Response<CategoriaResponseDTO>> Activate(int id);
    }
}
