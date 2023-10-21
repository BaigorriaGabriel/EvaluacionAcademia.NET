using EvaluacionAcademia.NET.Entities;

namespace EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces
{
	public interface IAccountRepository : IRepository<Account>
	{
		Task<Account> GetAccountByType(int codUser, string type);
	}
}
