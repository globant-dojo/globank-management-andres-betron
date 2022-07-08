using BankAPI.Models;

namespace BankAPICore.IData
{
    public interface IClienteDataService
    {
        Task<bool> DeleteCliente(Cliente Cliente);

        Task<Cliente?> GetCliente(int ClienteId);

        Task<bool> InsertCliente(Cliente cliente);

        Task<bool> UpdateCliente(Cliente cliente);

    }
}