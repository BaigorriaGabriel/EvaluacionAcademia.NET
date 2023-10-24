using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class AccountRepository : Repository<Account>, IAccountRepository
	{

		public AccountRepository(ApplicationDbContext context) : base(context)
		{

		}


		public override async Task<List<Account>> GetAllActive()
		{
			//return await _context.Accounts.Where(s => s.IsActive == true).ToListAsync();
			var accounts = await _context.Accounts
			.Include(a => a.User)
			.Where(s => s.IsActive == true)
			.Select(a => new Account
			{
				CodAccount = a.CodAccount,
				Type = a.Type,
				CodUser = a.CodUser,
				IsActive = a.IsActive,
				_BalancePeso = (a is AccountFiduciary) ? ((AccountFiduciary)a).BalancePeso : 0,
				_BalanceUsd = (a is AccountFiduciary) ? ((AccountFiduciary)a).BalanceUsd : 0,
				_BalanceBtc = (a is AccountCripto) ? ((AccountCripto)a).BalanceBtc : 0,
				_DireccionUUID = (a is AccountCripto) ? ((AccountCripto)a).DirectionUUID : null
			})
			.ToListAsync();

			return accounts;
		}

		public async Task<Account> GetAccountByType(int codUser, string type)
		{
			var account = await _context.Accounts.FirstOrDefaultAsync(x => x.CodUser == codUser && x.Type== type);

			return account;
		}

		public async Task<bool> AccountExById(int id)
		{
			return await _context.Accounts.AnyAsync(x => x.CodAccount == id);
		}

		public override async Task<Account> GetById(Account AccountToGet)
		{
			var account = await _context.Accounts.FirstOrDefaultAsync(x => x.CodAccount == AccountToGet.CodAccount);

			return account;
		}

		public override async Task<bool> Delete(Account deleteAccount)
		{
			var account = await _context.Accounts.FirstOrDefaultAsync(x => x.CodAccount == deleteAccount.CodAccount);
			if (account == null) { return false; }

			account.IsActive = false;

			_context.Accounts.Update(account);
			return true;
		}

	}
}
