using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class AccountFiduciaryRepository : Repository<AccountFiduciary>, IAccountFiduciaryRepository
	{

		public AccountFiduciaryRepository(ApplicationDbContext context) : base(context)
		{

		}

		public async Task<float> GetBalancePeso(int accountId)
		{
			var account = await _context.Accounts.OfType<AccountFiduciary>().FirstOrDefaultAsync(a => a.CodAccount == accountId);

			if (account != null)
			{
				return account.BalancePeso;
			}

			return 0;
		}

		public async Task<float> GetBalanceUsd(int accountId)
		{
			var account = await _context.Accounts.OfType<AccountFiduciary>().FirstOrDefaultAsync(a => a.CodAccount == accountId);

			if (account != null)
			{
				return account.BalanceUsd;
			}

			return 0;
		}
	}
}
