using APICatalogo.DTOs;
using APICatalogo.DTOs.Roles;
using APICatalogo.Models;
using APICatalogo.Paginator;
using APICatalogo.Repositories;
using APICatalogo.Services;
using APICatalogo.Services.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        
        [HttpPost]
        [Route("adicionar-role-user")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Response<RolesResponseDTO>>> AddRoleUser([FromBody] RolesRequestDTO rolesRequestDTO)
        {
            var service = await _rolesService.AddRoleUser(rolesRequestDTO);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPost]
        [Route("remove-perm-user")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Response<RolesResponseDTO>>> RemoveRoleUser([FromBody] RolesRequestDTO rolesRequestDTO)
        {
            var service = await _rolesService.RemoveRoleUser(rolesRequestDTO);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpGet]
        [Route("role-all")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Response<IEnumerable<RolesResponseDTO>>>> RoleAll()
        {
            var service = await _rolesService.RoleAll();

            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpGet]
        [Route("role-all/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Response<RolesResponseDTO>>> Role(string id)
        {
            var service = await _rolesService.RoleId(id);

            return service.Valid ? Ok(service) : BadRequest(service);
        }

    }
}
