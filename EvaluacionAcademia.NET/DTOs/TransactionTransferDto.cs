using EvaluacionAcademia.NET.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionAcademia.NET.DTOs
{
	public class TransactionTransferDto
	{
		public decimal Amount { get; set; }
		public string Currency { get; set; }

	}
}
