using BankAPI.DataAccess.Data;
using BankAPI.DataAccess.IConfiguration;
using BankAPI.Models;
using BankAPICore.IData;
using Microsoft.EntityFrameworkCore;

namespace BankAPICore.Data
{
    public class PersonaDataService : IPersonaDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonaDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Persona?> GetPersona(int idPersona)
        {
            return await _unitOfWork.PersonaRepository.GetById(idPersona);
        }

        public async Task<bool> InsertPersona(Persona persona)
        {
            await _unitOfWork.PersonaRepository.Add(persona);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdatePersona(Persona persona)
        {
            _unitOfWork.PersonaRepository.Update(persona);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
