using BankAPI.Models;

namespace BankAPICore.Data
{
    public interface IClienteDataService
    {
        Cliente DeleteCliente(int ClienteId);

        Task<Cliente?> GetCliente(int ClienteId);

        Task<bool> InsertCliente(Cliente cliente);

        Cliente UpdateCliente(Cliente cliente);

    }
}