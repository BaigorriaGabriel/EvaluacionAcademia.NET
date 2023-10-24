using EvaluacionAcademia.NET.DTOs;
using EvaluacionAcademia.NET.Entities;
using EvaluacionAcademia.NET.Infrastructure;
using EvaluacionAcademia.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace EvaluacionAcademia.NET.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public TransactionController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		[HttpGet("GetAll")]
		[Authorize]
		public async Task<IActionResult> GetAll()
		{

			var transactions = await _unitOfWork.TransactionRepository.GetAllActive();

			return ResponseFactory.CreateSuccessResponse(200, transactions);

		}


		[HttpPost]
		[Route("Deposit/{id}")]
		[Authorize]
		public async Task<IActionResult> Deposit([FromRoute]int id, TransactionDepositDto dto)
		{
			if (dto.Amount > 0)
			{
				var account = await _unitOfWork.AccountRepository.GetById(new Account(id));
				if (account!=null && account.Type == "Fiduciary" && account.IsActive==true) 
				{ 
					var accountFiduciary = await _unitOfWork.AccountFiduciaryRepository.GetById(new AccountFiduciary(id)); 
					var result = await _unitOfWork.TransactionRepository.Deposit(accountFiduciary, dto.Amount);
					await _unitOfWork.Complete();
					return ResponseFactory.CreateSuccessResponse(201, "Deposito realizado con exito!");
				}
			
				return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna Cuenta fiduciaria activa con el Id: {id}");
			}

			return ResponseFactory.CreateErrorResponse(406, "debe ingresar un monto mayor a 0");

		}

		[HttpPost]
		[Route("Transfer/{idSender}/{idReceiver}")]
		[Authorize]
		public async Task<IActionResult> Transfer([FromRoute] int idSender, [FromRoute] int idReceiver, TransactionTransferDto dto)
		{
			if(idSender==idReceiver) return ResponseFactory.CreateErrorResponse(406, "Debe ingresar dos cuentas distintas");
			if (dto.Amount > 0)
			{
				Account accountSender;
				Account accountReceiver;

				if (dto.Currency == "Peso" || dto.Currency == "Usd")
				{
					//cuenta Sender
					accountSender = await _unitOfWork.AccountRepository.GetById(new Account(idSender));
					if (accountSender == null || accountSender.Type!="Fiduciary" || accountSender.IsActive != true) { return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna Cuenta fiduciaria activa con el Id: {idSender}"); }
					//cuenta Receiver
					accountReceiver = await _unitOfWork.AccountRepository.GetById(new Account(idReceiver));
					if (accountReceiver == null || accountReceiver.Type != "Fiduciary" || accountReceiver.IsActive != true) { return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna Cuenta fiduciaria activa con el Id: {idReceiver}"); }

					var accountSenderFiduciary = await _unitOfWork.AccountFiduciaryRepository.GetById(new AccountFiduciary(idSender));
					 var accountReceiverFiduciary = await _unitOfWork.AccountFiduciaryRepository.GetById(new AccountFiduciary(idReceiver));

					if (dto.Currency == "Peso" && accountSenderFiduciary.BalancePeso < dto.Amount)
						return ResponseFactory.CreateErrorResponse(400, "Saldo insuficiente");

					if (dto.Currency == "Usd" && accountSenderFiduciary.BalanceUsd < dto.Amount)
						return ResponseFactory.CreateErrorResponse(400, "Saldo insuficiente");

					var result = await _unitOfWork.TransactionRepository.TransferFiduciary(accountSenderFiduciary, accountReceiverFiduciary, dto);
					await _unitOfWork.Complete();
					if(result) return ResponseFactory.CreateSuccessResponse(201, "Transferencia realizada con exito!");
					else ResponseFactory.CreateErrorResponse(400, "error al intentar transferir");
				}

				if (dto.Currency == "Btc")
				{
					//cuenta Sender
					accountSender = await _unitOfWork.AccountRepository.GetById(new Account(idSender));
					if (accountSender == null || accountSender.Type != "Cripto" || accountSender.IsActive != true) { return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna Cuenta Cripto activa con el Id: {idSender}"); }
					//cuenta Receiver
					accountReceiver = await _unitOfWork.AccountRepository.GetById(new Account(idReceiver));
					if (accountReceiver == null || accountReceiver.Type != "Cripto" || accountReceiver.IsActive != true) { return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna Cuenta Cripto activa con el Id: {idReceiver}"); }

					var accountSenderCripto = await _unitOfWork.AccountCriptoRepository.GetById(new AccountCripto(idSender));
					var accountReceiverCripto = await _unitOfWork.AccountCriptoRepository.GetById(new AccountCripto(idReceiver));

					if (accountSenderCripto.BalanceBtc < dto.Amount)
						return ResponseFactory.CreateErrorResponse(400, "Saldo insuficiente");

					var result = await _unitOfWork.TransactionRepository.TransferCripto(accountSenderCripto, accountReceiverCripto, dto);
					await _unitOfWork.Complete();
					if (result) return ResponseFactory.CreateSuccessResponse(201, "Transferencia realizada con exito!");
					else ResponseFactory.CreateErrorResponse(400, "error al intentar transferir");
				}

			}

			return ResponseFactory.CreateErrorResponse(406, "debe ingresar un monto mayor a 0");

		}
	}
}
