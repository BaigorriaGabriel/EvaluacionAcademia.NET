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

		public override async Task<List<AccountCripto>> GetAllActive()
		{
			//return await _context.CriptoAccounts.Where(s => s.IsActive == true).ToListAsync();
			return await _context.CriptoAccounts.Include(account => account.User).Where(account => account.IsActive == true).ToListAsync();
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

		public async Task<bool> AccountExByUUID(string UUID)
		{
			return await _context.CriptoAccounts.AnyAsync(x => x.DirectionUUID == UUID);
		}


	}
}
