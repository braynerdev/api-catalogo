using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    public class AplicationRoles : IdentityRole
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Column("name", TypeName = "varchar(150)")]
        public string Name { get; set; }

        [Column("normalized_name", TypeName = "varchar(150)")]
        public string NormalizedName { get; set; }
    }
}
