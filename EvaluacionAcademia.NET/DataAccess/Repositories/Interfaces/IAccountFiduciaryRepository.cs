using EvaluacionAcademia.NET.Entities;

namespace EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces
{
	public interface IAccountFiduciaryRepository : IRepository<AccountFiduciary>
	{
		Task<float> GetBalancePeso(int accountId);
		Task<float> GetBalanceUsd(int accountId);
	}
}
