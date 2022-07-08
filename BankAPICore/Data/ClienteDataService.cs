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

        public bool DeleteCliente(Cliente cliente)
        {
            using (var context = _contextCreator())
            {
                cliente.Estado = 0;
                context.Clientes.Update(cliente);
                return true;
            }
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

        public bool UpdateCliente(Cliente cliente)
        {
            using (var context = _contextCreator())
            {
                context.Clientes.Update(cliente);
                return true;
            }
        }
    }
}
