using BankAPI.Models;

namespace BankAPICore.Data
{
    public interface IClienteDataService
    {
        bool DeleteCliente(Cliente Cliente);

        Task<Cliente?> GetCliente(int ClienteId);

        Task<bool> InsertCliente(Cliente cliente);

        bool UpdateCliente(Cliente cliente);

    }
}