using EvaluacionAcademia.NET.DTOs;
using EvaluacionAcademia.NET.Entities;
using EvaluacionAcademia.NET.Infrastructure;
using EvaluacionAcademia.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionAcademia.NET.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountFiduciaryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public AccountFiduciaryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		[HttpGet("GetBalancePeso")]
		[Authorize]
		public async Task<IActionResult> GetBalancePeso(int idAccount) 
		{
			var saldo = await _unitOfWork.AccountFiduciaryRepository.GetBalancePeso(idAccount);

			return ResponseFactory.CreateSuccessResponse(200, saldo);
			//return Ok(saldo);

		}

		[HttpGet("GetBalanceUsd")]
		[Authorize]
		public async Task<IActionResult> GetBalanceUsd(int idAccount)
		{
			var saldo = await _unitOfWork.AccountFiduciaryRepository.GetBalanceUsd(idAccount);

			return ResponseFactory.CreateSuccessResponse(200, saldo);
			//return Ok(saldo);

		}

		[HttpPost]
		[Route("Create")]
		[Authorize]
		public async Task<IActionResult> Create(AccountFiduciaryDto dto)
		{
			if (await _unitOfWork.AccountFiduciaryRepository.AccountExByCBU(dto.CBU)) return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuetna registrada con el CBU: {dto.CBU}");
			if (await _unitOfWork.AccountFiduciaryRepository.AccountExByAlias(dto.Alias)) return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuetna registrada con el Alias: {dto.Alias}");
			//if (dto.RoleId != 1 && dto.RoleId != 2) return ResponseFactory.CreateErrorResponse(409, $"RoleId Invalido");
			var accountFiduciary = new AccountFiduciary(dto);
			await _unitOfWork.AccountFiduciaryRepository.Insert(accountFiduciary);
			await _unitOfWork.Complete();


			return ResponseFactory.CreateSuccessResponse(201, "Cuenta fiduciaria registrada con exito!");
		}
	}
}
