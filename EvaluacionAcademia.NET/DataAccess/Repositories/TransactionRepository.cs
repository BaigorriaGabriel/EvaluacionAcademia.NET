﻿using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
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
	}
}
