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
	public class UserController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public UserController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		[HttpPost]
		[Route("Register")]
		[Authorize]
		public async Task<IActionResult> Register(RegisterDto dto)
		{
			if (await _unitOfWork.UserRepository.UserExByMail(dto.Email)) return ResponseFactory.CreateErrorResponse(409, $"Ya existe un usuario registrado con el mail: {dto.Email}");
			//if (dto.RoleId != 1 && dto.RoleId != 2) return ResponseFactory.CreateErrorResponse(409, $"RoleId Invalido");
			var user = new User(dto);
			await _unitOfWork.UserRepository.Insert(user);
			await _unitOfWork.Complete();


			return ResponseFactory.CreateSuccessResponse(201, "Usuario registrado con exito!");
		}
	}
}
