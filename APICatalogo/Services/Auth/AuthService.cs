using APICatalogo.DTOs;
using APICatalogo.DTOs.Auth;
using APICatalogo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalogo.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AplicationUser> _userManage;
        private readonly IConfiguration _configuration;

        public AuthService(ITokenService tokenService,
            UserManager<AplicationUser> userManage,
            IConfiguration configuration)
        {
            _tokenService = tokenService;
            _userManage = userManage;
            _configuration = configuration;
        }

        public async Task<Response<LoginResponseDTO>> Login(LoginRequest loginModel)
        {

            var user = await _userManage.FindByNameAsync(loginModel.Username!);

            if (user is not null && await _userManage.CheckPasswordAsync(user, loginModel.Password!))
            {
                var perms = await _userManage.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var perm in perms)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, perm));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
                var refreshToken = _tokenService.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

                await _userManage.UpdateAsync(user);


                LoginResponseDTO loginResponse = new(
                    new(
                        user.Id,
                        user.UserName
                    ),
                    new(
                        new JwtSecurityTokenHandler().WriteToken(token),
                        refreshToken,
                        token.ValidTo
                    )
                );
                return Response<LoginResponseDTO>.Success("Login efetuado com sucesso!", loginResponse);
            }

            return Response<LoginResponseDTO>.Fail("Erro ao efetuar login!");
        }

        public async Task<Response<TokenResponse>> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {

            var accessToken = refreshTokenRequest.AccessToken;
            var refreshToken = refreshTokenRequest.TokenRefresh;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);

            if (principal is null)
            {
                return Response<TokenResponse>.Fail("Informações inválidas!");
            }

            string username = principal.Identity.Name;

            var user = await _userManage.FindByNameAsync(username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Response<TokenResponse>.Fail("Token inválido!");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

            TokenResponse tokenResponse = new TokenResponse(new JwtSecurityTokenHandler().WriteToken(newAccessToken), refreshTokenRequest.TokenRefresh, user.RefreshTokenExpiryTime);

            return Response<TokenResponse>.Success("Token atualizado!", tokenResponse);
        }

        public async Task<Response<AplicationUser>> Register(RegisterRequestDTO registerRequestDTO)
        {
            var userExits = await _userManage.FindByNameAsync(registerRequestDTO.Username);

            if (userExits is not null)
            {
                return Response<AplicationUser>.Fail("Username já está sendo usado!");
            }

            AplicationUser user = new AplicationUser
            {
                Email = registerRequestDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerRequestDTO.Username,
            };

            var result = await _userManage.CreateAsync(user, registerRequestDTO.Password);

            if (!result.Succeeded)
            {
                return Response<AplicationUser>.Fail("Falha na criação do usuário!");
            }
            return Response<AplicationUser>.Success("Usuário criado com sucesso", null);
        }

        public async Task<Response<AplicationUser>> Revoke(string username)
        {
            var user = await _userManage.FindByNameAsync(username);

            if (user is null)
            {
                return Response<AplicationUser>.Fail("Usuário não encontrado!");
            }
            user.RefreshToken = null;
            await _userManage.UpdateAsync(user);

            return Response<AplicationUser>.Success("Logout efetuado com sucesso!", null); ;
        }
    }
}
