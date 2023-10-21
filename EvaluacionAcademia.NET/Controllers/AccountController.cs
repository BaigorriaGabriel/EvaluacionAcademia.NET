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
	public class AccountController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public AccountController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet("GetAccountByType")]
		[Authorize]
		public async Task<IActionResult> GetAccountByType(int codUser, int type)
		{
			string sType = "";
			if (type == 1) { sType = "Fiduciary"; }
			else if (type == 2) { sType = "Cripto"; }
			else { return ResponseFactory.CreateErrorResponse(404, $"No existe ningun tipo de cuenta con el Id: {type}"); }

			if (await _unitOfWork.UserRepository.UserExById(codUser))
			{
				var account = await _unitOfWork.AccountRepository.GetAccountByType(codUser, sType);

				return ResponseFactory.CreateSuccessResponse(200, account);
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ningun usuario con el Id: {codUser}");
		}

	}
}
