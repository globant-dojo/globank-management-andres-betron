using BankAPI.DataAccess.IRepositories;
using System.Threading.Tasks;

namespace BankAPI.DataAccess.IConfiguration
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }
        Task CompleteAsync();
    }
}
