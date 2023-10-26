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


		/// <summary>
		/// Obtiene todas las cuentas de tipo cripto
		/// </summary>
		/// <returns>Status 200 mas Listado de cuentas Cripto</returns>
		[HttpGet("GetAllActive")]
		[Authorize]
		public async Task<IActionResult> GetAllActive() //(int pageToShow = 1)
		{
			var acounts = await _unitOfWork.AccountCriptoRepository.GetAllActive();

			return ResponseFactory.CreateSuccessResponse(200, acounts);
		}


        /// <summary>
        /// Obtiene el saldo de cuenta
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns>Status 200 mas Saldo</returns>
        [HttpGet("GetBalance")]
		[Authorize]
		public async Task<IActionResult> GetBalance(int idAccount) 
		{
			var account= await _unitOfWork.AccountCriptoRepository.GetById(new AccountCripto(idAccount));
			if(account != null && account.Type=="Cripto" && account.IsActive==true) 
			{
				var saldo = await _unitOfWork.AccountCriptoRepository.GetBalance(idAccount);

				return ResponseFactory.CreateSuccessResponse(200, saldo);
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna cuenta cripto activa con el Id: {idAccount}");

		}


        /// <summary>
        /// Creacion de cuenta cripto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Status 200 mas mensaje de confirmacion</returns>
        [HttpPost]
		[Route("Create")]
		[Authorize]
		public async Task<IActionResult> Create(AccountCriptoDto dto)
		{
			if (!await _unitOfWork.AccountCriptoRepository.AccountExByUserId(dto.CodUser))
			{
				if (await _unitOfWork.UserRepository.UserExById(dto.CodUser))
				{

					if (await _unitOfWork.AccountCriptoRepository.AccountExByUUID(dto.DirectionUUID)) return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuetna registrada con la direccion Universally Unique Identifier: {dto.DirectionUUID}");
					//if (dto.RoleId != 1 && dto.RoleId != 2) return ResponseFactory.CreateErrorResponse(409, $"RoleId Invalido");
					var accountCripto = new AccountCripto(dto);
					await _unitOfWork.AccountCriptoRepository.Insert(accountCripto);
					await _unitOfWork.Complete();


					return ResponseFactory.CreateSuccessResponse(201, "Cuenta cripto registrada con exito!");
				}
				return ResponseFactory.CreateErrorResponse(404, $"No existe ningun Usuario con el Id: {dto.CodUser}");
			}
            return ResponseFactory.CreateErrorResponse(406, $"Ya existe una cuenta Fiduciaria del Usuario con el Id: {dto.CodUser}");
        }


        /// <summary>
        /// Actualiza cuenta cripto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Status 200 mas mensaje de confirmacion</returns>
        [HttpPut("Update/{id}")]
		[Authorize]
		public async Task<IActionResult> Update([FromRoute] int id, AccountCriptoDto dto)
		{
			if (await _unitOfWork.UserRepository.UserExById(dto.CodUser))
			{

				var existingAccount = await _unitOfWork.AccountCriptoRepository.GetByUUID(dto.DirectionUUID);

				if (existingAccount != null && existingAccount.CodAccount != id)
				{
					return ResponseFactory.CreateErrorResponse(409, $"Ya existe una cuenta cripto registrada con la direccion Universally Unique Identifier: {dto.DirectionUUID}");
				}

				if (await _unitOfWork.AccountCriptoRepository.AccountExById(id))
				{
					existingAccount = await _unitOfWork.AccountCriptoRepository.GetById(new AccountCripto(id));

					//if (dto.RoleId != 1 && dto.RoleId != 2) return ResponseFactory.CreateErrorResponse(409, $"RoleId Invalido");
					var result = await _unitOfWork.AccountCriptoRepository.Update(new AccountCripto(dto, id));
					await _unitOfWork.Complete();
					return ResponseFactory.CreateSuccessResponse(201, "Cuenta cripto actualizada con exito!");
				}
				return ResponseFactory.CreateErrorResponse(404, $"No existe ninguna Cuenta cripto con el Id: {id}");
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ningun Usuario con el Id: {dto.CodUser}");
		}


	}
}
