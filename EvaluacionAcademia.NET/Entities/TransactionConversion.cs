using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Transactions")]
	public class TransactionConversion : Transaction
	{
		

		[Required]
		[Column("fromCurrency")]
		public string FromCurrency { get; set; }
		
		[Required]
		[Column("toCurrency")]
		public string ToCurrency { get; set; }
	}
}
