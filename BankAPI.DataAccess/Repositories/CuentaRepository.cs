using BankAPI.DataAccess.Data;
using BankAPI.DataAccess.IRepositories;
using BankAPI.Models;

namespace BankAPI.DataAccess.Repositories
{
    public class CuentaRepository : GenericRepository<Cuenta>, ICuentaRepository
    {
        public CuentaRepository(BankDbContext context) : base(context)
        {
        }
    }
}
