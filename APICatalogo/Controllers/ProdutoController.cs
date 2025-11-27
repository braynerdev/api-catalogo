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
        public async Task<ActionResult<ProdutoRequestDTO>> GetPaginator([FromQuery] ProdutosPaginator paginatorParams)
        {
            var service = await _produtoService.GetPaginator(paginatorParams);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPost]
        public async Task<ActionResult<Response<ProdutoResponseDTO>>> Created(ProdutoRequestDTO produtoDTO)
        {
            var service = await _produtoService.Created(produtoDTO);

            return service.Valid ? Ok(service) : BadRequest(service);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Response<string>>> Put(int id, ProdutoRequestDTO produtoDTO)
        {
            if (id < 1)
            {
                return BadRequest(Response<ProdutoResponseDTO>.Fail("O id precisa ser enviado"));
            }

            var service = await _produtoService.Put(id,produtoDTO);
            return service.Valid ? Ok(service) : BadRequest(service);

        }

        [HttpPatch("{id:int}/estoque/add/")]
        public async Task<ActionResult<Response<ProdutoResponseDTO>>> AdicionarEstoque(
                int id, 
                [FromBody] JsonPatchDocument<EstoqueProdutoDTO> patchDoc
            )
        {
            if (id < 1)
            {
                return BadRequest("Informaçõs invalidas");
            }

            var dto = new EstoqueProdutoDTO();
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _produtoService.AdicionarEstoque(id, dto.Estoque);
            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPatch("{id:int}/estoque/Remover/")]
        public async Task<ActionResult<Response<ProdutoResponseDTO>>> RemoverEstoque(
                int id,
                [FromBody] JsonPatchDocument<EstoqueProdutoDTO> patchDoc
            )
        {
            if (id < 1)
            {
                return BadRequest("Informaçõs invalidas");
            }

            var dto = new EstoqueProdutoDTO();
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _produtoService.RemoverEstoque(id, dto.Estoque);
            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPatch("deactivate/{id:int}")]
        public async Task<ActionResult<CategoriaResponseDTO>> Deactivate(int id)
        {
            if (id < 1)
            {
                return BadRequest("Parametro inválido");
            }

            var service = await _produtoService.Deactivate(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPatch("activate/{id:int}")]
        public async Task<ActionResult<CategoriaResponseDTO>> Activate(int id)
        {
            if (id < 1)
            {
                return BadRequest("Parametro inválido");
            }

            var service = await _produtoService.Activate(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }
    }
}
