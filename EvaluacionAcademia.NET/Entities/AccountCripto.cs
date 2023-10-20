using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Accounts")]
	public class AccountCripto : Account
	{
		[Required]
		[Column("directionUUID", TypeName = "VARCHAR(250)")]
		public string DirectionUUID { get; set; }

		[Required]
		[Column("balanceBtc")]
		public float BalanceBtc { get; set; }

		// Propiedades específicas de las cuentas de criptomonedas

	}
}
