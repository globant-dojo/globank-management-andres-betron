using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPI.Models
{
    public class Movimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public double Valor { get; set; }
        public double Saldo { get; set; }
        public int IdCuenta { get; set; }
        [ForeignKey("IdCuenta")]
        public virtual Cuenta Cuenta { get; set; }
    }
}
