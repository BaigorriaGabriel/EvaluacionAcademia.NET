using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Transactions")]
	public class TransactionTransfer : Transaction
	{
		[Required]
		[Column("codAccountReceiver", TypeName = "Int")]
		public int CodAccountReceiver { get; set; }
		[ForeignKey("CodAccountReceiver")]
		public Account? Account { get; set; }

		[Required]
		[Column("currency")]
		public string Currency { get; set; }
	}
}
