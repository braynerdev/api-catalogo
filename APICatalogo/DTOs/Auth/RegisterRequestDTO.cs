using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace APICatalogo.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Preencher campo de usuário")]
        [StringLength(100, ErrorMessage = "Número e caracters máximo atingido para o username")]
        public string Username { get; init; }

        [Required(ErrorMessage = "Preencher campo de usuário")]
        [StringLength(150, ErrorMessage = "Número e caracters máximo atingido para o nome")]
        public string Name { get; init; }

        [EmailAddress]
        [Required(ErrorMessage = "Preencher campo de E-mail")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Preencher campo de Senha")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[#@$]).{8,}$",
                            ErrorMessage = "Senha deve ter pelo menos 8 caracteres, conter pelo menos uma letra maiúscula e pelo menos um dos caracteres especiais: #, @, $.")]
        public string Password { get; init; }
    }
}
