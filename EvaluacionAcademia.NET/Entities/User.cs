﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using EvaluacionAcademia.NET.DTOs;

namespace EvaluacionAcademia.NET.Entities
{
	public class User
	{
        public User()
        {
            
        }

		public User(RegisterDto dto)
		{
			Name = dto.Name;
			Dni = dto.Dni;
			Email = dto.Email;
			Password = dto.Password; //PasswordEncryptHelper.EncryptPassword(dto.Password, dto.Email);
			IsActive = true;
		}

		[Key]
		[Column("codUser")]
		public int CodUser { get; set; }

		[Required]
		[Column("name", TypeName = "VARCHAR(50)")]
		public string Name { get; set; }

		[Required]
		[Column("email", TypeName = "VARCHAR(100)")]
		public string Email { get; set; }

		[Required]
		[Column("dni", TypeName = "VARCHAR(10)")]
		public string Dni { get; set; }

		[Required]
		[Column("password", TypeName = "VARCHAR(250)")]
		public string Password { get; set; }

		[Required]
		[Column("isActive")]
		public bool IsActive { get; set; }
	}
}
