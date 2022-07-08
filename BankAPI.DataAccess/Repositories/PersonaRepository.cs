using BankAPI.DataAccess.Data;
using BankAPI.DataAccess.IRepositories;
using BankAPI.Models;

namespace BankAPI.DataAccess.Repositories
{
    public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
    {
        public PersonaRepository(BankDbContext context) : base(context)
        {
        }
    }
}
