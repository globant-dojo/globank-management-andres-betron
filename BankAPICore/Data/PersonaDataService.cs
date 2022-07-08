using BankAPI.DataAccess;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPICore.Data
{
    public class PersonaDataService : IPersonaDataService
    {
        private Func<BankDbContext> _contextCreator;

        public PersonaDataService(Func<BankDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<Persona?> GetPersona(int idPersona)
        {
            using (var context = _contextCreator())
            {
                return await context.Personas.AsNoTracking().Where(x => x.IdPersona == idPersona).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> InsertPersona(Persona persona)
        {
            using (var context = _contextCreator())
            {
                await context.Personas.AddAsync(persona);
                return true;
            }
        }

        public bool UpdatePersona(Persona persona)
        {
            using (var context = _contextCreator())
            {
                context.Personas.Update(persona);
                return true;
            }
        }
    }
}
