using APICatalogo.DTOs;
using APICatalogo.DTOs.Roles;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Services.Roles
{
    public interface IRolesService
    {
        Task<Response<RolesResponseDTO>> AddRoleUser(RolesRequestDTO rolesRequestDTO);
        Task<Response<RolesResponseDTO>> RemoveRoleUser(RolesRequestDTO rolesRequestDTO);
        Task<Response<IEnumerable<RolesResponseDTO>>> RoleAll();
        Task<Response<RolesResponseDTO>> RoleId(string id);
    }
}
