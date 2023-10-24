using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using EvaluacionAcademia.NET.DTOs;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Accounts")]
	public class AccountCripto : Account
	{
        public AccountCripto()
        {
            
        }

		public AccountCripto(int id)
		{
			CodAccount = id;
		}

		public AccountCripto(AccountCriptoDto dto , int id)
		{
			CodAccount = id;
			Type = "Cripto";
			CodUser = dto.CodUser;
			IsActive = true;
			DirectionUUID = dto.DirectionUUID;
			BalanceBtc = dto.BalanceBtc;
		}

		public AccountCripto(AccountCriptoDto dto)
        {
            Type = "Cripto";
			CodUser = dto.CodUser;
			IsActive = true;
			DirectionUUID = dto.DirectionUUID;
			BalanceBtc = dto.BalanceBtc;
        }

        [Required]
		[Column("directionUUID", TypeName = "VARCHAR(250)")]
		public string DirectionUUID { get; set; }

		[Required]
		[Column("balanceBtc", TypeName = "decimal(18, 2)")]
		public decimal BalanceBtc { get; set; }

		// Propiedades específicas de las cuentas de criptomonedas

	}
}
