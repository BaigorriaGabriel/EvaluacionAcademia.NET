using EvaluacionAcademia.NET.DataAccess.Repositories;

namespace EvaluacionAcademia.NET.Services
{
	public interface IUnitOfWork
	{
		//public UserRepository UserRepository { get; }

		public AccountFiduciaryRepository AccountFiduciaryRepository { get; }
		public AccountCriptoRepository AccountCriptoRepository { get; }

		public UserRepository UserRepository { get; }

		public AccountRepository AccountRepository { get; }
		public TransactionRepository TransactionRepository { get; }

		Task<int> Complete();
	}
}
