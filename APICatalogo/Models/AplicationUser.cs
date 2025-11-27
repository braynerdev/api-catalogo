using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    public class AplicationUser : IdentityUser
    {

        [Column("name",TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Column("normalized_name",TypeName = "varchar(150)")]
        public string NormalizedName { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("expired_refresh_token")]
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
