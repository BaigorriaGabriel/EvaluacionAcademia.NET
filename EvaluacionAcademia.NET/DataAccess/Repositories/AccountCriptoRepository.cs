using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class AccountCriptoRepository : Repository<AccountCripto>, IAccountCriptoRepository
	{

		public AccountCriptoRepository(ApplicationDbContext context) : base(context)
		{

		}

		public async Task<float> GetBalance(int accountId)
		{
			var account = await _context.Accounts.OfType<AccountCripto>().FirstOrDefaultAsync(a => a.CodAccount == accountId);

			if (account != null)
			{
				return account.BalanceBtc;
			}

			return 0;
		}

		
	}
}
