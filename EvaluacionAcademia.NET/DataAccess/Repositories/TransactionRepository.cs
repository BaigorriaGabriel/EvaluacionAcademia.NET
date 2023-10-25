using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.DTOs;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class TransactionRepository : Repository<Transaction>, ITransactionRepository
	{
		public TransactionRepository(ApplicationDbContext context) : base(context)
		{

		}

		public override async Task<List<Transaction>> GetAllActive()
		{
			//return await _context.Accounts.Where(s => s.IsActive == true).ToListAsync();
			var transactions = await _context.Transactions
			.Select(a => new Transaction
			{
				CodTransaction = a.CodTransaction,
				Type = a.Type,
				CodAccountSender = a.CodAccountSender,
				Amount = a.Amount,
				Timestamp = a.Timestamp,
				_FromCurrency = (a is TransactionConversion) ? ((TransactionConversion)a).FromCurrency : "",
				_ToCurrency = (a is TransactionConversion) ? ((TransactionConversion)a).ToCurrency : "",
				_CodAccountReceiver = (a is TransactionTransfer) ? ((TransactionTransfer)a).CodAccountReceiver : 0,
				_Currency = (a is TransactionTransfer) ? ((TransactionTransfer)a).Currency : null
			})
			.ToListAsync();

			return transactions;
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

		public async Task<bool> TransferFiduciary(AccountFiduciary accountSender, AccountFiduciary accountReceiver, TransactionTransferDto dto)
		{
			if(dto.Currency=="Peso" || dto.Currency == "Usd")
			{
				AccountFiduciary accountSenderFromDB = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountSender.CodAccount);
				AccountFiduciary accountReceiverFromDB = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountReceiver.CodAccount);
				if(dto.Currency == "Peso")
				{
					accountSenderFromDB.BalancePeso -= dto.Amount;
					accountReceiverFromDB.BalancePeso += dto.Amount;
				}
				if(dto.Currency == "Usd")
				{
					accountSenderFromDB.BalanceUsd -= dto.Amount;
					accountReceiverFromDB.BalanceUsd += dto.Amount;
				}
			}
			

			Transaction transaction = new TransactionTransfer
			{
				Type = "Transfer",
				CodAccountSender = accountSender.CodAccount,
				CodAccountReceiver = accountReceiver.CodAccount,
				Amount = dto.Amount,
				Currency = dto.Currency,
				Timestamp = DateTime.Now
			};

			_context.Transactions.Add(transaction);

			await _context.SaveChangesAsync();

			return true;

		}

		public async Task<bool> TransferCripto(AccountCripto accountSender, AccountCripto accountReceiver, TransactionTransferDto dto)
		{
			AccountCripto accountSenderFromDB = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountSender.CodAccount);
			AccountCripto accountReceiverFromDB = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountReceiver.CodAccount);

			accountSenderFromDB.BalanceBtc -= dto.Amount;
			accountReceiverFromDB.BalanceBtc += dto.Amount;

			
			Transaction transaction = new TransactionTransfer
			{
				Type = "Transfer",
				CodAccountSender = accountSender.CodAccount,
				CodAccountReceiver = accountReceiver.CodAccount,
				Amount = dto.Amount,
				Currency = dto.Currency,
				Timestamp = DateTime.Now
			};

			_context.Transactions.Add(transaction);

			await _context.SaveChangesAsync();

			return true;

		}

		public async Task<bool> Convert(Account accountToUpdate, decimal amount, string currencyToCurrency)
		{
			var priceUsd = 1000;
			var priceBtc = 12000;
			Transaction transaction;
			AccountFiduciary accountFiduciary;
			AccountCripto accountCripto;

			if (currencyToCurrency == "PesoToUsd")
			{
				accountFiduciary = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountToUpdate.CodAccount);
				if (accountFiduciary == null)
				{
					return false;
				}
				accountFiduciary.BalancePeso -= amount;
				accountFiduciary.BalanceUsd += (amount / priceUsd);

				transaction = new TransactionConversion
				{
					Type = "Conversion",
					CodAccountSender = accountToUpdate.CodAccount,
					Amount = amount,
					FromCurrency = "Peso",
					ToCurrency = "Usd",
					Timestamp = DateTime.Now
				};
				_context.Transactions.Add(transaction);
			}

			if (currencyToCurrency == "UsdToPeso")
			{
				accountFiduciary = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountToUpdate.CodAccount);
				if (accountFiduciary == null)
				{
					return false;
				}
				accountFiduciary.BalanceUsd -= amount;
				accountFiduciary.BalancePeso += (amount * priceUsd);

				transaction = new TransactionConversion
				{
					Type = "Conversion",
					CodAccountSender = accountToUpdate.CodAccount,
					Amount = amount,
					FromCurrency = "Usd",
					ToCurrency = "Peso",
					Timestamp = DateTime.Now
				};
				_context.Transactions.Add(transaction);
			}

			if (currencyToCurrency == "UsdToBtc")
			{
				accountFiduciary = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountToUpdate.CodAccount);
				accountCripto= await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodUser == accountToUpdate.CodUser);
				if (accountFiduciary == null || accountCripto == null)
				{
					return false;
				}
				accountFiduciary.BalanceUsd -= amount;
				accountCripto.BalanceBtc += (amount / priceBtc);

				transaction = new TransactionConversion
				{
					Type = "Conversion",
					CodAccountSender = accountToUpdate.CodAccount,
					Amount = amount,
					FromCurrency = "Usd",
					ToCurrency = "Btc",
					Timestamp = DateTime.Now
				};
				_context.Transactions.Add(transaction);
			}

			if (currencyToCurrency == "BtcToUsd")
			{
				accountFiduciary = await _context.FiduciaryAccounts.FirstOrDefaultAsync(x => x.CodUser == accountToUpdate.CodUser);
				accountCripto = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountToUpdate.CodAccount);
				if (accountFiduciary == null || accountCripto == null)
				{
					return false;
				}
				accountFiduciary.BalanceUsd += (amount * priceBtc);
				accountCripto.BalanceBtc -= amount;

				transaction = new TransactionConversion
				{
					Type = "Conversion",
					CodAccountSender = accountToUpdate.CodAccount,
					Amount = amount,
					FromCurrency = "Btc",
					ToCurrency = "Usd",
					Timestamp = DateTime.Now
				};
				_context.Transactions.Add(transaction);
			}


			await _context.SaveChangesAsync();

			return true;

		}
	}
}
