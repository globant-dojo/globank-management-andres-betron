using BankAPI.Models;

namespace BankAPICore.IData
{
    public interface ICuentaDataService
    {
        Task<Cuenta> GetCuenta(int idCuenta);
        Task<bool> AddCuenta(Cuenta cuenta);
        Task<bool> UpdateCuenta(Cuenta cuenta);
        Task<bool> DeleteCuenta(Cuenta cuenta);
    }
}