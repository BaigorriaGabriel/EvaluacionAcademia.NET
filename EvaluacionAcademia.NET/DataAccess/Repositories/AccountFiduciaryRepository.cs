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

		public async Task<decimal> GetBalancePeso(int accountId)
		{
			var account = await _context.Accounts.OfType<AccountFiduciary>().FirstOrDefaultAsync(a => a.CodAccount == accountId);

			if (account != null)
			{
				return account.BalancePeso;
			}

			return 0;
		}

		public async Task<decimal> GetBalanceUsd(int accountId)
		{
			var account = await _context.Accounts.OfType<AccountFiduciary>().FirstOrDefaultAsync(a => a.CodAccount == accountId);

			if (account != null)
			{
				return account.BalanceUsd;
			}

			return 0;
		}

        public async Task<Account> GetByIdUser(int userId)
        {
            var account = await _context.Accounts.OfType<AccountFiduciary>().FirstOrDefaultAsync(a => a.CodUser == userId);

            if (account != null)
            {
                return account;
            }

            return null;
        }


        public async Task<bool> AccountExByCBU(string CBU)
		{
			return await _context.FiduciaryAccounts.AnyAsync(x => x.CBU == CBU);
		}

		public async Task<bool> AccountExByAlias(string Alias)
		{
			return await _context.FiduciaryAccounts.AnyAsync(x => x.Alias == Alias);
		}

		public async Task<bool> AccountExById(int id)
		{
			return await _context.FiduciaryAccounts.AnyAsync(x => (x.CodAccount == id && x.Type == "Fiduciary"));
		}

		public async Task<bool> AccountExByUserId(int userId)
		{
			return await _context.FiduciaryAccounts.AnyAsync(x => x.CodUser == userId && x.IsActive == true);
		}

		public override async Task<bool> Update(AccountFiduciary updateAccount)
		{
			var account = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == updateAccount.CodAccount);
			if (account == null) { return false; }
			account.Type = updateAccount.Type;
			account.CodUser = updateAccount.CodUser;
			account.IsActive = true;
			account.CBU = updateAccount.CBU;
			account.Alias = updateAccount.Alias;
			account.AccountNumber = updateAccount.AccountNumber;
			account.BalancePeso = updateAccount.BalancePeso;
			account.BalanceUsd = updateAccount.BalanceUsd;

			_context.FiduciaryAccounts.Update(account);
			return true;
		}

		public override async Task<AccountFiduciary> GetById(AccountFiduciary accountFiduciary)
		{
			var account = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountFiduciary.CodAccount);

			return account;
		}

		public async Task<AccountFiduciary> GetByAlias(string alias)
		{
			var account = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.Alias == alias);

			return account;
		}

		public async Task<AccountFiduciary> GetByCBU(string CBU)
		{
			var account = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CBU == CBU);

			return account;
		}

		public async Task<AccountFiduciary> GetByAccountNumber(string accountNumber)
		{
			var account = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);

			return account;
		}
	}
}
