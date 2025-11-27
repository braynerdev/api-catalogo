using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.DTOs.Roles
{
    public class RolesRequestDTO
    {
        [Required(ErrorMessage = "E-mail não pode ser nulo!")]
        [EmailAddress(ErrorMessage = "Formato do e-mail está inválido!")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Id da role não pode ser nulo!")]
        public List<string> RoleId { get; init; } = new();
    }
}
