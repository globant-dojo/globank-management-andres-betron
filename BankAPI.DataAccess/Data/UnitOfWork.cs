using BankAPI.DataAccess.IConfiguration;
using BankAPI.DataAccess.IRepositories;
using BankAPI.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace BankAPI.DataAccess.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BankDbContext _context;

        public IClienteRepository ClienteRepository { get; private set; }

        public IPersonaRepository PersonaRepository { get; private set; }

        public ICuentaRepository CuentaRepository { get; private set; }

        public UnitOfWork(BankDbContext context)
        {
            _context = context;

            ClienteRepository = new ClienteRepository(_context);
            PersonaRepository = new PersonaRepository(_context);
            CuentaRepository = new CuentaRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
