using APICatalogo.DTOs;
using APICatalogo.DTOs.Auth;
using APICatalogo.DTOs.Categoria;
using APICatalogo.DTOs.Produto;
using APICatalogo.Models;
using APICatalogo.Paginator.Conf;
using APICatalogo.Paginator.Produto;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<Response<ProdutoResponseDTO>> Created(ProdutoRequestDTO produtoDTO)
        {
            var produto = _mapper.Map<Produtos>(produtoDTO);

            var postProduto =  _unf.ProdutoRepositorie.Create(produto);

            if (postProduto is null)
            {
                return Response<ProdutoResponseDTO>.Fail("Erro ao criar produto");
            }
            await _unf.commitAsync();
            var novosProdutos = _mapper.Map<ProdutoRequestDTO>(postProduto);

            return Response<ProdutoResponseDTO>.Success("Produto criado com sucesso!",null);
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

        public async Task<Response<PagdList<ProdutoResponseDTO>>> GetPaginator(ProdutosPaginator paginatorParams)
        {
            var produtos = await _unf.ProdutoRepositorie.GetPaginator(paginatorParams);

            if (produtos.Count < 1)
            {
                return Response<PagdList<ProdutoResponseDTO>>.Fail("Nenhum Produto foi encontrado!");
            }
            var map = _mapper.Map<PagdList<ProdutoResponseDTO>>(produtos);

            return Response<PagdList<ProdutoResponseDTO>>.Success(
                "Produtos resgatados com sucesso!",
                map
            );
        }

        public async Task<Response<ProdutoResponseDTO>> Put(int id, ProdutoRequestDTO produtoDTO)
        {
            
            bool produto = await _unf.ProdutoRepositorie.GetByExists(id);

            if (!produto)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto não existe");
            }
            var produtoMap = _mapper.Map<Produtos>(produtoDTO);

            var putProduto = _unf.ProdutoRepositorie.Update(produtoMap);

        
            await _unf.commitAsync();
            return Response<ProdutoResponseDTO>.Success("Produto não existe", null);
        }

        public async Task<Response<ProdutoResponseDTO>> AdicionarEstoque(int id, int estoque)
        {
            bool produto = await _unf.ProdutoRepositorie.GetByExists(id);

            if (!produto)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto não existe");
            }
            var produtoBd = await _unf.ProdutoRepositorie.GetById(id);

            produtoBd.Estoque += estoque;
            _unf.ProdutoRepositorie.Update(produtoBd);
            await _unf.commitAsync();

            return Response<ProdutoResponseDTO>.Success($"Estoque do(a) {produtoBd.Name} atualizado para {produtoBd.Estoque}", null);
        }

        public async Task<Response<ProdutoResponseDTO>> RemoverEstoque(int id, int estoque)
        {
            bool produto = await _unf.ProdutoRepositorie.GetByExists(id);

            if (!produto)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto não existe");
            }
            var produtoBd = await _unf.ProdutoRepositorie.GetById(id);

            produtoBd.Estoque -= estoque;
            _unf.ProdutoRepositorie.Update(produtoBd);
            await _unf.commitAsync();

            return Response<ProdutoResponseDTO>.Success($"Estoque do(a) {produtoBd.Name} atualizado para {produtoBd.Estoque}", null);
        }

        public async Task<Response<ProdutoResponseDTO>> Activate(int id)
        {
            var produto = await _unf.ProdutoRepositorie.GetAsync(d => d.Id == id);

            if (produto is null)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto inexistente!");
            }

            if (produto.Active)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto já está ativo!");
            }

            produto.Active = true;

            await _unf.commitAsync();
            return Response<ProdutoResponseDTO>.Success("Produto reativado com sucesso!", null);

        }

        public async Task<Response<ProdutoResponseDTO>> Deactivate(int id)
        {
            var produto = await _unf.ProdutoRepositorie.GetAsync(d => d.Id == id);

            if (produto is null)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto inexistente!");
            }

            if (!produto.Active)
            {
                return Response<ProdutoResponseDTO>.Fail("Produto já está desativado!");
            }

            produto.Active = false;

            await _unf.commitAsync();
            return Response<ProdutoResponseDTO>.Success("Produto desativado com sucesso!", null);

        }
    }
}
