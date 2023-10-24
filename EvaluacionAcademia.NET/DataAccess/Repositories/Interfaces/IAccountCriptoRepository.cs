using EvaluacionAcademia.NET.Entities;

namespace EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces
{
	public interface IAccountCriptoRepository : IRepository<AccountCripto>
	{
		Task<decimal> GetBalance(int accountId);
	}
}
