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
	public class AccountCriptoController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public AccountCriptoController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet("GetAllActive")]
		[Authorize]
		public async Task<IActionResult> GetAllActive() //(int pageToShow = 1)
		{
			//int pageToShow = 1;

			var acounts = await _unitOfWork.AccountCriptoRepository.GetAllActive();

			//if (Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow); }

			//var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();

			//var paginateUsers = PaginateHelper.Paginate(users, pageToShow, url);

			//return ResponseFactory.CreateSuccessResponse(200, paginateUsers);
			return ResponseFactory.CreateSuccessResponse(200, acounts);

		}

		[HttpGet("GetBalance")]
		[Authorize]
		public async Task<IActionResult> GetBalance(int idAccount) 
		{
			var saldo = await _unitOfWork.AccountCriptoRepository.GetBalance(idAccount);

			return ResponseFactory.CreateSuccessResponse(200, saldo);
			//return Ok(saldo);

		}

		[HttpPost]
		[Route("Create")]
		[Authorize]
		public async Task<IActionResult> Create(AccountCriptoDto dto)
		{
			if (await _unitOfWork.AccountCriptoRepository.AccountExByUUID(dto.DirectionUUID)) return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuetna registrada con la direccion Universally Unique Identifier: {dto.DirectionUUID}");
			//if (dto.RoleId != 1 && dto.RoleId != 2) return ResponseFactory.CreateErrorResponse(409, $"RoleId Invalido");
			var accountCripto = new AccountCripto(dto);
			await _unitOfWork.AccountCriptoRepository.Insert(accountCripto);
			await _unitOfWork.Complete();


			return ResponseFactory.CreateSuccessResponse(201, "Cuenta cripto registrada con exito!");
		}


	}
}
