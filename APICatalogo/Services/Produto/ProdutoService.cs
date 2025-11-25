using APICatalogo.DTOs;
using APICatalogo.DTOs.Auth;
using APICatalogo.DTOs.Produto;
using APICatalogo.Paginator.Produto;
using APICatalogo.Repositories;
using AutoMapper;

namespace APICatalogo.Services.Produto
{
    public class ProdutoService : IProdutoService
    {
        private readonly IUnitOfWork _unf;
        private readonly IMapper _mapper;

        public ProdutoService(IUnitOfWork unf, IMapper mapper)
        {
            _unf = unf;
            _mapper = mapper;
        }
        public Task<Response<string>> Created(ProdutoRequestDTO produtoDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ProdutoResponseDTO>> GetById(int id)
        {
            var produto = await _unf.ProdutoRepositorie.GetAsync(p => p.Id == id);

            if (produto is null)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto não encontrado"); ;
            }
            var produtoResponse = _mapper.Map<ProdutoResponseDTO>(produto);
            return Response<ProdutoResponseDTO>.Success("Produto encontrado com sucesso!", produtoResponse);
        }

        public Task<Response<ProdutoResponseDTO>> GetPaginator(ProdutosPaginator paginatorParams)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> Put(int id, ProdutoRequestDTO produtoDTO)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> UpdateEstoque(int id, int estoque)
        {
            throw new NotImplementedException();
        }
    }
}
