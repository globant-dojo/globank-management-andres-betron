namespace BankAPI.Models
{
    public class EstadoCuenta : Cuenta
    {
        public double TotalDebitos { get; set; }
        public double TotalCreditos { get; set; }
    }
}
