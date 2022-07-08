using BankAPI.DataAccess.IRepositories;
using System.Threading.Tasks;

namespace BankAPI.DataAccess.IConfiguration
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }
        IPersonaRepository PersonaRepository { get; }
        ICuentaRepository CuentaRepository { get; }
        Task CompleteAsync();
    }
}
