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
            throw new NotImplementedException();
        }

        public async Task<bool> AddCuenta(Cuenta cuenta)
        {
            throw new NotImplementedException();
        }
    }
}
