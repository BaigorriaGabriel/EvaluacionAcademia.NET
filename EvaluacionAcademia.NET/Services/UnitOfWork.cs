using EvaluacionAcademia.NET.DataAccess;
using EvaluacionAcademia.NET.DataAccess.Repositories;

namespace EvaluacionAcademia.NET.Services
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		//public UserRepository UserRepository { get; private set; }
		public AccountFiduciaryRepository AccountFiduciaryRepository { get; private set; }
		public AccountCriptoRepository AccountCriptoRepository { get; private set; }
		public UserRepository UserRepository { get; private set; }
		public AccountRepository AccountRepository { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			//	UserRepository = new UserRepository(_context);
			AccountFiduciaryRepository = new AccountFiduciaryRepository(_context);
			AccountCriptoRepository = new AccountCriptoRepository(_context);
			UserRepository = new UserRepository(_context);
			AccountRepository = new AccountRepository(_context);
		}
		public Task<int> Complete()
		{
			return _context.SaveChangesAsync();
		}

		//public void Dispose()
		//{
		//	_context.Dispose();
		//}
	}
}
