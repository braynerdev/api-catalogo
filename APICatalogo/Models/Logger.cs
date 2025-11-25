using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    public class Logger : Base
    {

        [Column("tipo", TypeName="varchar(8)")]
        public string Tipo { get; set; }

        [Column("tipo", TypeName = "interger")]
        public int StatusCode { get; set; }

        [Column("table_name", TypeName = "varchar(150)")]
        public string TableName { get; set; }

        [Column("register_id", TypeName = "interger")]
        public int RegisterId { get; set; }
    }
}
