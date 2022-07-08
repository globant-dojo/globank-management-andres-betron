using BankAPI.Models;

namespace BankAPICore.IData
{
    public interface IPersonaDataService
    {
        Task<Persona?> GetPersona(int idPersona);
        Task<bool> InsertPersona(Persona persona);
        Task<bool> UpdatePersona(Persona persona);
    }
}