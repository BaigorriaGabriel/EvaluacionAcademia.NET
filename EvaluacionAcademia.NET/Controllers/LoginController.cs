using EvaluacionAcademia.NET.DTOs;
using EvaluacionAcademia.NET.Helper;
using EvaluacionAcademia.NET.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionAcademia.NET.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private TokenJwtHelper _tokenJwtHelper;
		private readonly IUnitOfWork _unitOfWork;
		public LoginController(IUnitOfWork unitOfWork, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_tokenJwtHelper = new TokenJwtHelper(configuration);
		}

		/// <summary>
		/// Inicio de sesion
		/// </summary>
		/// <param name="dto"></param>
		/// <returns>Usuario con datos mas JWT</returns>
		[HttpPost]
		public async Task<IActionResult> Login(AuthenticateDto dto)
		{
			var userCredentials = await _unitOfWork.UserRepository.AuthenticateCredentials(dto);
			if (userCredentials == null) { return Unauthorized("Las credenciales son incorrectas"); }

			var token = _tokenJwtHelper.GenerateToken(userCredentials);

			var user = new UserLoginDto()
			{
				Name = userCredentials.Name,
				Email = userCredentials.Email,
				Dni = userCredentials.Dni,
				IsActive = userCredentials.IsActive,
				Token = token
			};

			return Ok(user);
		}
	}
}
