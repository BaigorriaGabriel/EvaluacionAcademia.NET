using EvaluacionAcademia.NET.Entities;

namespace EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces
{
	public interface IAccountCriptoRepository : IRepository<AccountCripto>
	{
		Task<float> GetBalance(int accountId);
	}
}
