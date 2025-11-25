using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Categoria;
using APICatalogo.DTOs.Produto;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Paginator.Categoria;
using APICatalogo.Paginator.Conf;
using APICatalogo.Paginator.Produto;
using APICatalogo.Services.Produto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Policy = "UserValidOnly")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult> GetById(int id)
        {
            var service = await _produtoService.GetById(id);

            return service.Valid ? Ok(service) : BadRequest(service);

        }

        [HttpGet("paginator")]
        public async Task<ActionResult<IEnumerable<ProdutoRequestDTO>>> GetPaginator([FromQuery] ProdutosPaginator paginatorParams)
        {
            var produtos = await _unf.ProdutoRepositorie.GetProdutosAsync(paginatorParams);

            if (produtos is null)
            {
                return NotFound("produtos não encontrado");
            }

            return ObterProdutos(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>> Created(ProdutoRequestDTO produtoDTO)
        {
            var produto = _mapper.Map<Produtos>(produtoDTO);

            var postProduto = _unf.ProdutoRepositorie.Create(produto);

            if (postProduto is null)
            {
                return BadRequest();
            }
            await _unf.commitAsync();
            var novosProdutos = _mapper.Map<ProdutoRequestDTO>(postProduto);

            return new CreatedAtRouteResult("ObterProduto",
                new { id = novosProdutos.ProdutoId }, novosProdutos);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Response<string>>> Put(int id, ProdutoRequestDTO produtoDTO)
        {
            if (id != produtoDTO.ProdutoId)
            {
                return BadRequest("Dados Inconcistentes");
            }
            var produto = _mapper.Map<Produtos>(produtoDTO);

            var putProduto = _unf.ProdutoRepositorie.Update(produto);

            if (putProduto is null)
            {
                return NotFound("Produto Não Encontrados");
            }
            await _unf.commitAsync();
            var novosProdutos = _mapper.Map<ProdutoRequestDTO>(putProduto);
            return Ok(novosProdutos);

        }

        [HttpPatch("{id:int}/{estoque:int}/update-estoque")]
        public async Task<ActionResult<Response<string>>> UpdateEstoque(int id, int estoque)
        {
            if (patchProdutoDTO is null || id <= 0)
            {
                return BadRequest("Informaçõs invalidas");
            }

            var produto = await _unf.ProdutoRepositorie.GetAsync(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não existe!");
            }

            var produtoUpdateRequest = _mapper.Map<ProdutoUpdateRequaestDTO>
                (produto);

            patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(produtoUpdateRequest, produto);
            _unf.ProdutoRepositorie.Update(produto);
            await _unf.commitAsync();

            var produtoUpdateResponse = _mapper.Map<ProdutoUpdateResponseDTO>
                (produto);

            return Ok(produtoUpdateResponse);
        }

        [HttpPatch("/deactivate/{id:int}")]
        public async Task<ActionResult<CategoriaResponseDTO>> Deactivate(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id inválido");
            }

            var service = await _categoriaService.Deactivate(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPatch("/activate/{id:int}")]
        public async Task<ActionResult<CategoriaResponseDTO>> Activate(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id inválido");
            }

            var service = await _categoriaService.Activate(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }
    }
}
