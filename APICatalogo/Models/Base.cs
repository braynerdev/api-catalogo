using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    public abstract class Base
    {
        [Key]
        [Required]
        [Column("id", TypeName = "interger")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("update_at")]
        public DateTime UpdateAt { get; set; }

        [Column("active", TypeName = "boolean")]
        public bool Active { get; set; }
    }
}
