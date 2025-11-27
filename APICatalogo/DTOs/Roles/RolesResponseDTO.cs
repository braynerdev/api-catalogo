using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.DTOs.Roles
{
    public class RolesResponseDTO
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
