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
			return await _context.Accounts.Where(s => s.IsActive == true).ToListAsync();
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
