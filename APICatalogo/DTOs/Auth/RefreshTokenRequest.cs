using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Auth
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "O token de acesso não pode ser nulo!")]
        public string AccessToken { get; init; }

        [Required(ErrorMessage = "O refresh token não pode ser nulo!")]
        public string TokenRefresh { get; init; }
    }
}
