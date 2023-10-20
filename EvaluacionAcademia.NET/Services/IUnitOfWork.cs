using EvaluacionAcademia.NET.DataAccess.Repositories;

namespace EvaluacionAcademia.NET.Services
{
	public interface IUnitOfWork
	{
		//public UserRepository UserRepository { get; }

		public AccountFiduciaryRepository AccountFiduciaryRepository { get; }
		public AccountCriptoRepository AccountCriptoRepository { get; }

		//public ProjectRepository ProjectRepository { get; }

		//public JobRepository JobRepository { get; }

		Task<int> Complete();
	}
}
