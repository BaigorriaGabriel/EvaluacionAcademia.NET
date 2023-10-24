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

		[HttpGet("GetAllActive")]
		[Authorize]
		public async Task<IActionResult> GetAllActive() //(int pageToShow = 1)
		{
			//int pageToShow = 1;

			var acounts = await _unitOfWork.AccountFiduciaryRepository.GetAllActive();

			//if (Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow); }

			//var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();

			//var paginateUsers = PaginateHelper.Paginate(users, pageToShow, url);

			//return ResponseFactory.CreateSuccessResponse(200, paginateUsers);
			return ResponseFactory.CreateSuccessResponse(200, acounts);

		}

		[HttpGet("GetBalancePeso")]
		[Authorize]
		public async Task<IActionResult> GetBalancePeso(int idAccount) 
		{
			var account = await _unitOfWork.AccountFiduciaryRepository.GetById(new AccountFiduciary(idAccount));
			if (account != null && account.Type == "Fiduciary" && account.IsActive == true)
			{
				var saldo = await _unitOfWork.AccountFiduciaryRepository.GetBalancePeso(idAccount);

				return ResponseFactory.CreateSuccessResponse(200, saldo);
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna cuenta fiduciaria activa con el Id: {idAccount}");

		}

		[HttpGet("GetBalanceUsd")]
		[Authorize]
		public async Task<IActionResult> GetBalanceUsd(int idAccount)
		{
			var account = await _unitOfWork.AccountFiduciaryRepository.GetById(new AccountFiduciary(idAccount));
			if (account != null && account.Type == "Fiduciary" && account.IsActive == true)
			{
				var saldo = await _unitOfWork.AccountFiduciaryRepository.GetBalanceUsd(idAccount);

				return ResponseFactory.CreateSuccessResponse(200, saldo);
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna cuenta fiduciaria activa con el Id: {idAccount}");

		}

		[HttpPost]
		[Route("Create")]
		[Authorize]
		public async Task<IActionResult> Create(AccountFiduciaryDto dto)
		{
			if (await _unitOfWork.UserRepository.UserExById(dto.CodUser))
			{

				if (await _unitOfWork.AccountFiduciaryRepository.AccountExByCBU(dto.CBU)) return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuetna registrada con el CBU: {dto.CBU}");
				if (await _unitOfWork.AccountFiduciaryRepository.AccountExByAlias(dto.Alias)) return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuetna registrada con el Alias: {dto.Alias}");
				//if (dto.RoleId != 1 && dto.RoleId != 2) return ResponseFactory.CreateErrorResponse(409, $"RoleId Invalido");
				var accountFiduciary = new AccountFiduciary(dto);
				await _unitOfWork.AccountFiduciaryRepository.Insert(accountFiduciary);
				await _unitOfWork.Complete();


				return ResponseFactory.CreateSuccessResponse(201, "Cuenta fiduciaria registrada con exito!");
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ningun Usuario con el Id: {dto.CodUser}");
		}

		[HttpPut("Update/{id}")]
		[Authorize]
		public async Task<IActionResult> Update([FromRoute] int id, AccountFiduciaryDto dto)
		{
			if (await _unitOfWork.UserRepository.UserExById(dto.CodUser))
			{

				if (await _unitOfWork.AccountFiduciaryRepository.AccountExById(id))
				{

					var existingAccount = await _unitOfWork.AccountFiduciaryRepository.GetByAlias(dto.Alias);

					if (existingAccount != null && existingAccount.CodAccount != id)
					{
						return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuenta fiduciaria registrada con el Alias: {dto.Alias}");
					}

					existingAccount = await _unitOfWork.AccountFiduciaryRepository.GetByCBU(dto.CBU);

					if (existingAccount != null && existingAccount.CodAccount != id)
					{
						return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuenta fiduciaria registrada con el CBU: {dto.CBU}");
					}

					existingAccount = await _unitOfWork.AccountFiduciaryRepository.GetByAccountNumber(dto.AccountNumber);

					if (existingAccount != null && existingAccount.CodAccount != id)
					{
						return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuenta fiduciaria registrada con el numero de cuenta: {dto.AccountNumber}");
					}

					//if (dto.RoleId != 1 && dto.RoleId != 2) return ResponseFactory.CreateErrorResponse(409, $"RoleId Invalido");
					var result = await _unitOfWork.AccountFiduciaryRepository.Update(new AccountFiduciary(dto, id));
					await _unitOfWork.Complete();
					return ResponseFactory.CreateSuccessResponse(201, "Cuenta fiduciaria actualizada con exito!");
				}
				return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna Cuenta fiduciaria con el Id: {id}");
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ningun Usuario con el Id: {dto.CodUser}");
		}
	}
}
