using BankAPI.DataAccess;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPICore.Data
{
    public class ClienteDataService : IClienteDataService
    {
        private Func<BankDbContext> _contextCreator;

        public ClienteDataService(Func<BankDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public Cliente DeleteCliente(int idCliente)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente?> GetCliente(int idCliente)
        {
            using (var context = _contextCreator())
            {
                return await context.Clientes.AsNoTracking().Where(x => x.IdCliente == idCliente).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> InsertCliente(Cliente cliente)
        {
            using (var context = _contextCreator())
            {
                await context.Clientes.AddAsync(cliente);
                return true;
            }
        }

        public Cliente UpdateCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
