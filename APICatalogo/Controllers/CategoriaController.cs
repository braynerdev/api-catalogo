using APICatalogo.DTOs.Categoria;
using APICatalogo.Paginator.Categoria;
using APICatalogo.Services.Categoria;
using Microsoft.AspNetCore.Mvc;



namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult> GetById(int id)
        {
            var service = await _categoriaService.GetById(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpGet("paginator")]
        public async Task<ActionResult<CategoriaResponseDTO>> GetPaginator([FromQuery] CategoriasPaginator categoriasPaginator)
        {
            var service = await _categoriaService.GetPaginator(categoriasPaginator);

            return service.Valid ? Ok(service) : NotFound(service);
        }

        [HttpPost]
        public async Task<ActionResult> Created(CategoriaRequestDTO categoriaRequestDTO)
        {
            var service = await _categoriaService.Created(categoriaRequestDTO);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CategoriaRequestDTO categoriaRequestDTO)
        {
            var service = await _categoriaService.Put(id, categoriaRequestDTO);

            return service.Valid ? Ok(service) : BadRequest(service);

        }


        [HttpPatch("/deactivate/{id:int}")]
        public async Task<ActionResult> Deactivate(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id inválido");
            }

            var service = await _categoriaService.Deactivate(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPatch("/activate/{id:int}")]
        public async Task<ActionResult> Activate(int id)
        {
            if(id < 1)
            {
                return BadRequest("Id inválido");
            }

            var service = await _categoriaService.Activate(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }
    }
}
