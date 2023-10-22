using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using EvaluacionAcademia.NET.DTOs;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Accounts")]
	public class AccountFiduciary : Account
	{

        public AccountFiduciary()
        {
            
        }
		
		public AccountFiduciary(int id)
        {
			CodAccount = id;
        }

		public AccountFiduciary(AccountFiduciaryDto dto, int id)
		{
			CodAccount = id;
			Type = "Fiduciary";
			CodUser = dto.CodUser;
			IsActive = true;
			CBU=dto.CBU;
			Alias = dto.Alias;
			AccountNumber = dto.AccountNumber;
			BalancePeso = dto.BalancePeso;
			BalanceUsd = dto.BalanceUsd;
		}

		public AccountFiduciary(AccountFiduciaryDto dto)
        {
			Type = "Fiduciary";
			CodUser = dto.CodUser;
			IsActive = true;
			CBU= dto.CBU;
			Alias = dto.Alias;
			AccountNumber = dto.AccountNumber;
			BalancePeso = dto.BalancePeso;
			BalanceUsd = dto.BalanceUsd;
		}

        [Required]
		[Column("CBU", TypeName = "VARCHAR(250)")]
		public string CBU { get; set; }

		[Required]
		[Column("alias", TypeName = "VARCHAR(50)")]
		public string Alias { get; set; }

		[Required]
		[Column("accountNumber", TypeName = "VARCHAR(250)")]
		public string AccountNumber { get; set; }

		[Required]
		[Column("balancePeso")]
		public float BalancePeso { get; set; }

		[Required]
		[Column("balanceUsd")]
		public float BalanceUsd { get; set; }

		// Propiedades específicas de las cuentas fiduciarias
	}
}
