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

        /// <summary>
        /// Obtiene todas las cuentas
        /// </summary>
        /// <returns>Status 200 mas listado de cuentas</returns>
        [HttpGet("GetAllActive")]
		[Authorize]
		public async Task<IActionResult> GetAllActive() 
		{
			var acounts = await _unitOfWork.AccountRepository.GetAllActive();

			return ResponseFactory.CreateSuccessResponse(200, acounts);

		}

        /// <summary>
        /// Obtiene cuenta segun codigo de usuario y tipo de cuenta
        /// </summary>
        /// <param name="codUser"></param>
        /// <param name="type"></param>
        /// <returns>Status 200 mas cuenta</returns>
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


        /// <summary>
        /// Eliminacion logica de cuenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 mas mensaje de confirmacion</returns>
        [HttpDelete("Delete/{id}")]
		[Authorize]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (await _unitOfWork.AccountRepository.AccountExById(id))
			{
				var account = await _unitOfWork.AccountRepository.GetById(new Account(id));
				if (account.IsActive)
				{
					var result = await _unitOfWork.AccountRepository.Delete(new Account(id));
					await _unitOfWork.Complete();
					return ResponseFactory.CreateSuccessResponse(201, "Cuenta eliminada con exito!");
				}
				else
				{
					return ResponseFactory.CreateErrorResponse(409, $"La cuenta con Id: {id} ya se encuentra eliminada");
				}
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna cuenta con el Id: {id}");
		}

	}
}
