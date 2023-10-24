using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EvaluacionAcademia.NET.Entities
{
	[Table("Accounts")]
	public class Account
	{
        public Account()
        {
            
        }

        public Account(int id)
        {
			CodAccount = id;
        }

        [Key]
		[Column("codAccount")]
		public int CodAccount { get; set; }

		[Required]
		[Column("type")]
		public string Type { get; set; }

		[Required]
		[Column("codUser", TypeName = "Int")]
		public int CodUser { get; set; }
		[ForeignKey("CodUser")]
		public User? User { get; set; }

		[Required]
		[Column("isActive")]
		public bool IsActive { get; set; }

		[NotMapped]
		public decimal? _BalancePeso { get; set; }
		[NotMapped]
		public decimal? _BalanceUsd { get; set; }
		[NotMapped]
		public decimal? _BalanceBtc { get; set; }
		[NotMapped]
		public string? _DireccionUUID { get; set; }
	}
}
