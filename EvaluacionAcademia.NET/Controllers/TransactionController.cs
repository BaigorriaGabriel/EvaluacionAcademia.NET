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
	}
}
