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

		public async Task<decimal> GetBalance(int accountId)
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

		public async Task<bool> AccountExByUserId(int userId)
		{
			return await _context.CriptoAccounts.AnyAsync(x => x.CodUser == userId && x.IsActive == true);
		}

		public async Task<bool> AccountExById(int id)
		{
			return await _context.CriptoAccounts.AnyAsync(x => (x.CodAccount == id && x.Type== "Cripto"));
		}

		public override async Task<AccountCripto> GetById(AccountCripto accountToGet)
		{
			var account = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountToGet.CodAccount);

			return account;
		}

		public override async Task<bool> Update(AccountCripto updateAccount)
		{
			var account = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodAccount == updateAccount.CodAccount);
			if (account == null) { return false; }
			account.Type = updateAccount.Type;
			account.CodUser = updateAccount.CodUser;
			account.IsActive = true;
			account.DirectionUUID = updateAccount.DirectionUUID;
			account.BalanceBtc = updateAccount.BalanceBtc;

			_context.CriptoAccounts.Update(account);
			return true;
		}

		public async Task<AccountCripto> GetByUUID(string UUID)
		{
			var account = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.DirectionUUID == UUID);

			return account;
		}


	}
}
