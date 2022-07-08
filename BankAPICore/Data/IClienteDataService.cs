using BankAPI.Models;

namespace BankAPICore.Data
{
    public interface IClienteDataService
    {
        Cliente DeleteCliente(int ClienteId);

        Cliente GetCliente(int ClienteId);

        Cliente InsertCliente(Cliente cliente);

        Cliente UpdateCliente(Cliente cliente);

    }
}