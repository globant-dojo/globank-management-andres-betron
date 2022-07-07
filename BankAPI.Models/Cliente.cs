using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        [ForeignKey("IdPersona")]
        public virtual Persona Persona { get; set; }
        public string Contraseña { get; set; }
        public int Estado { get; set; }
    }
}
