using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Preencher campo de usuário!")]
        [StringLength(100, ErrorMessage = "Número e caracters máximo atingido para o username")]
        public string Username { get; init; }

        [Required(ErrorMessage = "Preencher campo de Senha!")]
        public string Password { get; init; }
    }
}
