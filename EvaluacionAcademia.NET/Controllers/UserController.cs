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

		[HttpGet("GetAllActive")]
		[Authorize]
		public async Task<IActionResult> GetAllActive() //(int pageToShow = 1)
		{
			//int pageToShow = 1;

			var users = await _unitOfWork.UserRepository.GetAllActive();

			//if (Request.Query.ContainsKey("page")) { int.TryParse(Request.Query["page"], out pageToShow); }

			//var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();

			//var paginateUsers = PaginateHelper.Paginate(users, pageToShow, url);

			//return ResponseFactory.CreateSuccessResponse(200, paginateUsers);
			return ResponseFactory.CreateSuccessResponse(200, users);

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

		[HttpPut("Update/{id}")]
		[Authorize]
		public async Task<IActionResult> Update([FromRoute] int id, RegisterDto dto)
		{
			if (await _unitOfWork.UserRepository.UserExById(id))
			{
				if (await _unitOfWork.UserRepository.UserExByMail(dto.Email))
				{
					var existingUser = await _unitOfWork.UserRepository.GetByEmail(dto.Email);
					if (existingUser.CodUser != id)
					{
						return ResponseFactory.CreateErrorResponse(409, $"Ya existe un usuario registrado con el mail: {dto.Email}");
					}
				}
				var result = await _unitOfWork.UserRepository.Update(new User(dto, id));
				await _unitOfWork.Complete();
				return ResponseFactory.CreateSuccessResponse(201, "Usuario actualizado con exito!");
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ningun usuario con el ID: {id}");
		}

		[HttpDelete("Delete/{id}")]
		[Authorize]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (await _unitOfWork.UserRepository.UserExById(id))
			{
				var user = await _unitOfWork.UserRepository.GetById(new User(id));
				if (user.IsActive)
				{
					var result = await _unitOfWork.UserRepository.Delete(new User(id));
					await _unitOfWork.Complete();
					return ResponseFactory.CreateSuccessResponse(201, "Usuario eliminado con exito!");
				}
				else
				{
					return ResponseFactory.CreateErrorResponse(409, $"El Usuario con Id: {id} ya se encuentra eliminado");
				}
			}
			return ResponseFactory.CreateErrorResponse(404, $"No existe ningun usuario con el Id: {id}");
		}
	}
}
