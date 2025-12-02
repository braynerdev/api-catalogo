using APICatalogo.DTOs;
using APICatalogo.DTOs.Auth;
using APICatalogo.DTOs.Categoria;
using APICatalogo.DTOs.Produto;
using APICatalogo.Models;
using APICatalogo.Paginator.Categoria;
using APICatalogo.Paginator.Conf;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services.Categoria
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IUnitOfWork _unf;
        private readonly IMapper _mapper;

        public CategoriaService(IUnitOfWork unf, IMapper imapper)
        {
            _unf = unf;
            _mapper = imapper;
        }
        public async Task<Response<CategoriaResponseDTO>> Activate(int id)
        {
            var categoria = await _unf.CategoriaRepositorie.GetAsync(d => d.Id == id);

            if (categoria is null)
            {
                return Response<CategoriaResponseDTO>.Fail("Categoria inexistente!");
            }

            if (categoria.Active)
            {
                return Response<CategoriaResponseDTO>.Fail("Categoria já está ativa!");
            }

            categoria.Active = true;

            await _unf.commitAsync();
            return Response<CategoriaResponseDTO>.Success("Categoria reativada com sucesso!", null);

        }

        public async Task<Response<CategoriaResponseDTO>> Created(CategoriaRequestDTO categoriaRequestDTO)
        {
            var categoria = _mapper.Map<Categorias>(categoriaRequestDTO);
            var createCategoria = _unf.CategoriaRepositorie.Create(categoria);

            if (createCategoria is null)
            {
                return Response<CategoriaResponseDTO>.Fail("Erro ao criar Categoria!");
            }

            await _unf.commitAsync();
            return Response<CategoriaResponseDTO>.Success("Categoria criada com sucesso!", null);
        }

        public async Task<Response<CategoriaResponseDTO>> Deactivate(int id)
        {
            var categoria = await _unf.CategoriaRepositorie.GetAsync(d => d.Id == id);

            if (categoria is null)
            {
                return Response<CategoriaResponseDTO>.Fail("Categoria inexistente!");
            }

            if (!categoria.Active)
            {
                return Response<CategoriaResponseDTO>.Fail("Categoria já está desativada!");
            }

            categoria.Active = false;

            await _unf.commitAsync();
            return Response<CategoriaResponseDTO>.Success("Categoria desativada com sucesso!", null);
        }

        public async Task<Response<CategoriaResponseDTO>> GetById(int id)
        {
            var categoria = await _unf.CategoriaRepositorie.GetAsync(c => c.Id == id);
            if (categoria is null)
            {
                return Response<CategoriaResponseDTO>.Fail("Categoria não existe!");
            }

            var categoriaDTO = _mapper.Map<CategoriaResponseDTO>(categoria);

            return Response<CategoriaResponseDTO>.Success("Categoria resgatada com sucesso!", categoriaDTO);

        }

        public async Task<Response<CategoriaPaginatorResponseDTO>> GetPaginator(CategoriasPaginator categoriasPaginator)
        {
            var categorias = await _unf.CategoriaRepositorie.GetCategoriasPaginator(categoriasPaginator);

            if(categorias.Count < 1)
            {
                return Response<CategoriaPaginatorResponseDTO>.Fail("Nenhuma categoria encontrada!");
            }

            var categoriasDto =  _mapper.Map<List<CategoriaResponseDTO>>(categorias);

            var response =  new CategoriaPaginatorResponseDTO
            {
                Paginator = new PaginatorResponseDTO
                {
                    CurrentPag = categorias.CurrentPag,
                    TotalPages = categorias.TotalPages,
                    PageSize = categorias.PageSize,
                    TotalCount = categorias.TotalCount,
                    HasNext = categorias.HasNext,
                    HasPreview = categorias.HasPreview
                },
                Items = categoriasDto
            };


            return Response<CategoriaPaginatorResponseDTO>.Success("Categorias resgatadas com sucesso!", response);
        }

        public async Task<Response<CategoriaResponseDTO>> Put(int id, CategoriaRequestDTO categoriaRequestDTO)
        {
            var categoria = await _unf.CategoriaRepositorie.GetAsync(c => c.Id == id);
            if (categoria is null)
            {
                return Response<CategoriaResponseDTO>.Fail("Categoria não existe");
            }
        
            _mapper.Map(categoriaRequestDTO, categoria);
            _unf.CategoriaRepositorie.Update(categoria);

            await _unf.commitAsync();

            return Response<CategoriaResponseDTO>.Success("Categoria atualizada com sucesso!", null);
        }
    }
}
