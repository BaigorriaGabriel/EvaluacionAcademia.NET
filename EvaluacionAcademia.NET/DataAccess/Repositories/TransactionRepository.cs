using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class TransactionRepository : Repository<Transaction>, ITransactionRepository
	{
		public TransactionRepository(ApplicationDbContext context) : base(context)
		{

		}

		public async Task<bool> Deposit(AccountFiduciary depositAccount, decimal amount)
		{
			var account = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == depositAccount.CodAccount);

			if (account == null)
			{
				return false;
			}
			account.BalancePeso += amount;

			Transaction transaction = new Transaction
			{
				Type = "Deposit",
				CodAccountSender = depositAccount.CodAccount,
				Amount = amount,
				Timestamp = DateTime.Now
			};

			_context.Transactions.Add(transaction);

			await _context.SaveChangesAsync();

			return true;

		}
	}
}
