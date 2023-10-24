using EvaluacionAcademia.NET.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionAcademia.NET.DTOs
{
	public class TransactionDto
	{
		public string Type { get; set; }
		public int CodAccountSender { get; set; }
		public decimal Amount { get; set; }
	}
}
