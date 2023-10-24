using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Transactions")]
	public class Transaction
	{

        [Key]
		[Column("codTransaction")]
		public int CodTransaction { get; set; }

		[Required]
		[Column("type")]
		public string Type { get; set; }

		[Required]
		[Column("codAccountSender", TypeName = "Int")]
		public int CodAccountSender { get; set; }
		[ForeignKey("CodAccountSender")]
		public Account? Account { get; set; }

		[Required]
		[Column("amount", TypeName = "decimal(18, 2)")]
		public decimal Amount { get; set; }

		[Required]
		[Column("timestamp")]
		public DateTime Timestamp { get; set; }


	}
}
