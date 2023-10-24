using EvaluacionAcademia.NET.Entities;

namespace EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces
{
	public interface IAccountFiduciaryRepository : IRepository<AccountFiduciary>
	{
		Task<decimal> GetBalancePeso(int accountId);
		Task<decimal> GetBalanceUsd(int accountId);
	}
}
