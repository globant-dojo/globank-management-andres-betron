using BankAPI.DataAccess;
using BankAPI.Models;

namespace BankAPICore.Data
{
    public class ClienteDataService : IClienteDataService
    {
        private Func<BankDbContext> _contextCreator;

        public ClienteDataService(Func<BankDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public Cliente DeleteCliente(int ClienteId)
        {
            throw new NotImplementedException();
        }

        public Cliente? GetCliente(int ClienteId)
        {
            using(var context = _contextCreator())
            {
                return context.Clientes.Where(x=>x.IdCliente == ClienteId).FirstOrDefault();
            }
        }

        public Cliente InsertCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Cliente UpdateCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
