using APICatalogo.DTOs;
using APICatalogo.DTOs.Auth;
using APICatalogo.Models;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Services.Auth
{
    public interface IAuthService
    {
        Task<Response<LoginResponseDTO>> Login(LoginRequest loginModel);
        Task<Response<AplicationUser>> Register(RegisterRequestDTO registerRequestDTO);
        Task<Response<TokenResponse>> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        Task<Response<AplicationUser>> Revoke(string username);
    }
}
