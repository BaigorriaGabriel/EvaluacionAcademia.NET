using EvaluacionAcademia.NET.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionAcademia.NET.DTOs
{
	public class TransactionConversionDto
	{

		public string FromCurrency { get; set; }
		public string ToCurrency { get; set; }
		public decimal Amount { get; set; }
	}
}
