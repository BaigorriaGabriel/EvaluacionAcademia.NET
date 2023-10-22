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

		public override async Task<List<AccountFiduciary>> GetAllActive()
		{
			//return await _context.FiduciaryAccounts.Where(s => s.IsActive == true).ToListAsync();
			return await _context.FiduciaryAccounts.Include(account => account.User).Where(account => account.IsActive == true).ToListAsync();
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

		public async Task<bool> AccountExByCBU(string CBU)
		{
			return await _context.FiduciaryAccounts.AnyAsync(x => x.CBU == CBU);
		}

		public async Task<bool> AccountExByAlias(string Alias)
		{
			return await _context.FiduciaryAccounts.AnyAsync(x => x.Alias == Alias);
		}
	}
}
