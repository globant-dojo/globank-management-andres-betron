using BankAPI.DataAccess.Data;
using BankAPI.DataAccess.IRepositories;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
