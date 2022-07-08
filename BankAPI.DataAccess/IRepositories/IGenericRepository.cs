using System.Threading.Tasks;

namespace BankAPI.DataAccess.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task Add(T entity);
        void Update(T entity);
    }
}
