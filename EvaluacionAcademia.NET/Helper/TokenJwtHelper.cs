﻿using EvaluacionAcademia.NET.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EvaluacionAcademia.NET.Helper
{
	public class TokenJwtHelper
	{
		private IConfiguration _configuration;
		public TokenJwtHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(User user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.NameIdentifier, user.CodUser.ToString()),
				//new Claim(ClaimTypes.Role, user.RoleId.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var securityToken = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}
	}
}
