using BankAPI.DataAccess.IConfiguration;
using BankAPI.Models;
using BankAPICore.IData;

namespace BankAPICore.Data
{
    public class CuentaDataService : ICuentaDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CuentaDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Cuenta> GetCuenta(int idCuenta)
        {
            return await _unitOfWork.CuentaRepository.GetById(idCuenta);
        }

        public async Task<bool> AddCuenta(Cuenta cuenta)
        {
            await _unitOfWork.CuentaRepository.Add(cuenta);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateCuenta(Cuenta cuenta)
        {
            _unitOfWork.CuentaRepository.Update(cuenta);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteCuenta(Cuenta cuenta)
        {
            cuenta.Estado = 0;
            _unitOfWork.CuentaRepository.Update(cuenta);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
