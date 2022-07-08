using BankAPI.Models;

namespace BankAPICore.Data
{
    public interface IPersonaDataService
    {
        Task<Persona?> GetPersona(int idPersona);
        Task<bool> InsertPersona(Persona persona);
        bool UpdatePersona(Persona persona);
    }
}