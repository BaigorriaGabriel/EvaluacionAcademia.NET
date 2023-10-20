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
		//[Authorize]
		public async Task<IActionResult> GetBalancePeso(int idAccount) 
		{
			var saldo = await _unitOfWork.AccountFiduciaryRepository.GetBalancePeso(idAccount);

			//return ResponseFactory.CreateSuccessResponse(200, saldo);
			return Ok(saldo);

		}

		[HttpGet("GetBalanceUsd")]
		//[Authorize]
		public async Task<IActionResult> GetBalanceUsd(int idAccount)
		{
			var saldo = await _unitOfWork.AccountFiduciaryRepository.GetBalanceUsd(idAccount);

			return ResponseFactory.CreateSuccessResponse(200, saldo);
			//return Ok(saldo);

		}
	}
}
