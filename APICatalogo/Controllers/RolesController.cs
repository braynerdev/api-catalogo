using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Paginator;
using APICatalogo.Repositories;
using APICatalogo.Services;
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
        private readonly UserManager<AplicationUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(ITokenService tokenService,
            UserManager<AplicationUser> userManage,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManage = userManage;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        [Route("create-role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var role = await _roleManager.RoleExistsAsync(roleName);
            if (!role)
            {
                var createRole = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (createRole.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { status = "sucesso", message = $"Permissão {roleName} criada com sucesso" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"Erro na criação da premissão {roleName}" });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { status = "Error", message = $"Peremissão {roleName} já existe!" });
        }

        [HttpPost]
        [Route("adicionar-role-user")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AddRoleUser(string email, string roleName)
        {
            var user = await _userManage.FindByEmailAsync(email);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (user is not null && role is not null)
            {
                var addRoleUser = await _userManage.AddToRoleAsync(user, roleName);

                if (addRoleUser.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { status = "Success", message = $"Permissão {roleName} adicionada ao usuário {user.UserName}!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"Não foi possível adicionar {roleName} ao usuário {user.UserName}!" });
                }
            }
            else if (user is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"Usuário {user.UserName} Não existe!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"Permissão {roleName} Não existe!" });
            }
        }

        [HttpPost]
        [Route("remove-perm-user")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> RemovePermUser(string email, string roleName)
        {
            var user = await _userManage.FindByEmailAsync(email);
            var role = await _roleManager.FindByNameAsync(roleName);
            var userPerm = await _userManage.IsInRoleAsync(user, roleName);

            if (user is not null && role is not null && userPerm)
            {
                var addRoleUser = await _userManage.RemoveFromRoleAsync(user, roleName);

                if (addRoleUser.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { status = "Success", message = $"Permissão {roleName} removida do usuário {user.UserName}!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"Não foi possível remover {roleName} do usuário {user.UserName}!" });
                }
            }
            else if (user is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"Usuário {user.UserName} Não existe!" });
            }
            else if (!userPerm)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"A permissão não pode ser removida pois o usuário Não tem {roleName}" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { status = "Error", message = $"Permissão {roleName} Não existe!" });
            }
        }

        [HttpGet]
        [Route("role-all")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<AplicationRoles>>> RoleAll()
        {
            var perm = await _roleManager.Roles.AsNoTracking().ToListAsync();

            if (perm is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { status = "Error", message = "Não Existe Permissão!" });
            }
            return Ok(perm);
        }

        [HttpGet]
        [Route("role-all/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Role(string id)
        {
            if (id is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { status = "Error", message = "Não Existe Permissão!" });
            }
            var perm = await _roleManager.FindByIdAsync(id);

            if (perm is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { status = "Error", message = "Não Existe Permissão!" });
            }
            return Ok(perm);
        }

        [HttpPut]
        [Route("editar-permissao/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EditarPermissao(string id, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { status = "Error", message = "Permissão Não Existe!" });
            }

            role.Name = newRoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { status = "Error", message = "Não foi possivel alterar o nome da permissão!" });
            }

            return Ok(new Response { status = "Success", message = "Nome da permissão alterado com sucesso!" });
        }

        [HttpDelete]
        [Route("remover-permissao/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> RemoverPermissao(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { status = "Error", message = "Permissão Não Existe!" });
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { status = "Error", message = "Não foi possivel deletar a permissão!" });
            }

            return Ok(new Response { status = "Success", message = "Permissão deletada com sucesso!" });
        }
    }
}
