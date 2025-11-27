using APICatalogo.DTOs;
using APICatalogo.DTOs.Roles;
using APICatalogo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APICatalogo.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly UserManager<AplicationUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesService(ITokenService tokenService,
            UserManager<AplicationUser> userManage,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManage = userManage;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<Response<RolesResponseDTO>> AddRoleUser(RolesRequestDTO rolesRequestDTO)
        {
            var user = await _userManage.FindByEmailAsync(rolesRequestDTO.Email);

            if (user is null)
            {
                return Response<RolesResponseDTO>.Fail("Usuário informado não existe!");
            }

            var roles = await _roleManager.Roles
                .Where(r => rolesRequestDTO.RoleId.Contains(r.Id))
                .ToListAsync();

            var foundRoleIds = roles.Select(r => r.Id).ToList();

            var notFound = rolesRequestDTO.RoleId.Except(foundRoleIds).ToList();
            if (notFound.Any())
            {
                return Response<RolesResponseDTO>.Fail(
                    $"{notFound.Count} roles enviadas não existem! IDs: {string.Join(", ", notFound)}"
                );
            }

            var userRoles = await _userManage.GetRolesAsync(user);

            var roleNames = roles.Select(r => r.Name).ToList();
            var rolesToAdd = roleNames.Except(userRoles).ToList();

            if (!rolesToAdd.Any())
            {
                return Response<RolesResponseDTO>.Success(
                    $"O usuário {user.UserName} já possui todas essas permissões.",
                    null
                );
            }

            var result = await _userManage.AddToRolesAsync(user, rolesToAdd);

            if (!result.Succeeded)
            {
                return Response<RolesResponseDTO>.Fail(
                    $"Erro ao adicionar permissões ao usuário {user.UserName}!"
                );
            }

            return Response<RolesResponseDTO>.Success(
                $"Permissões adicionadas ao usuário {user.UserName}!",
                null
            );
        }

        public async Task<Response<RolesResponseDTO>> RemoveRoleUser(RolesRequestDTO rolesRequestDTO)
        {
            var user = await _userManage.FindByEmailAsync(rolesRequestDTO.Email);

            if (user is null)
            {
                return Response<RolesResponseDTO>.Fail("Usuário informado não existe!");
            }

            var roles = await _roleManager.Roles
                .Where(r => rolesRequestDTO.RoleId.Contains(r.Id))
                .ToListAsync();

            var foundRoleIds = roles.Select(r => r.Id).ToList();

            var notFound = rolesRequestDTO.RoleId.Except(foundRoleIds).ToList();

            if (notFound.Count > 0)
            {
                return Response<RolesResponseDTO>.Fail(
                    $"{notFound.Count} roles enviadas não existem! IDs: {string.Join(", ", notFound)}"
                );
            }

            var userRoles = await _userManage.GetRolesAsync(user);

            var roleNames = roles.Select(r => r.Name).ToList();

            var rolesToRemove = roleNames.Intersect(userRoles).ToList();

            if (rolesToRemove.Count == 0)
            {
                return Response<RolesResponseDTO>.Success(
                    $"O usuário {user.UserName} não possui nenhuma dessas permissões.",
                    null
                );
            }

            var result = await _userManage.RemoveFromRolesAsync(user, rolesToRemove);

            if (!result.Succeeded)
            {
                return Response<RolesResponseDTO>.Fail(
                    $"Erro ao remover permissões do usuário {user.UserName}!"
                );
            }

            return Response<RolesResponseDTO>.Success(
                $"Permissões removidas do usuário {user.UserName}!",
                null
            );
        }

        public async Task<Response<IEnumerable<RolesResponseDTO>>> RoleAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var map = _mapper.Map<IEnumerable<RolesResponseDTO>>(roles);
            return Response<IEnumerable<RolesResponseDTO>>.Success("Roles resgatadas com sucesso!", map);
        }

        public async Task<Response<RolesResponseDTO>> RoleId(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return Response<RolesResponseDTO>.Fail("Role não Existe");
            }
            var map = _mapper.Map<RolesResponseDTO>(role);
            return Response<RolesResponseDTO>.Success("Role não Existe",map);
        }
    }
}
