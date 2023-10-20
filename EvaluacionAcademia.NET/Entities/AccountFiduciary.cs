using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Accounts")]
	public class AccountFiduciary : Account
	{
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
