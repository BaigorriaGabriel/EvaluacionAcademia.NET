using EvaluacionAcademia.NET.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionAcademia.NET.DTOs
{
	public class TransactionWithdrawalDto
    {
		public string Currency { get; set; }
		public decimal Amount { get; set; }
	}
}
