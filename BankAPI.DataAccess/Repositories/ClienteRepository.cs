using BankAPI.DataAccess.Data;
using BankAPI.DataAccess.IRepositories;
using BankAPI.Models;

namespace BankAPI.DataAccess.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(BankDbContext context)
            : base(context)
        {
        }
    }
}
