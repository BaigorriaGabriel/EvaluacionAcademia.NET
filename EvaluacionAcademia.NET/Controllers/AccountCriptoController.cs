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


		[HttpGet("GetBalance")]
		[Authorize]
		public async Task<IActionResult> GetBalance(int idAccount) 
		{
			var saldo = await _unitOfWork.AccountCriptoRepository.GetBalance(idAccount);

			return ResponseFactory.CreateSuccessResponse(200, saldo);
			//return Ok(saldo);

		}

	
	}
}
