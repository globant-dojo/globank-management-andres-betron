using BankAPI.DataAccess;
using BankAPI.DataAccess.IConfiguration;
using BankAPI.DataAccess.IRepositories;
using BankAPI.Models;
using BankAPICore.IData;
using Microsoft.EntityFrameworkCore;

namespace BankAPICore.Data
{
    public class ClienteDataService : IClienteDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteCliente(Cliente cliente)
        {
            cliente.Estado = 0;
            _unitOfWork.ClienteRepository.Update(cliente);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Cliente?> GetCliente(int idCliente)
        {
            return await _unitOfWork.ClienteRepository.GetById(idCliente);
        }

        public async Task<bool> InsertCliente(Cliente cliente)
        {
            await _unitOfWork.ClienteRepository.Add(cliente);
            return true;
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            _unitOfWork.ClienteRepository.Update(cliente);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
