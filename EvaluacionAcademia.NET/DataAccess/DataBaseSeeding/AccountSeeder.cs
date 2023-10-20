using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.DataBaseSeeding
{
	public class AccountSeeder : IEntitySeeder
	{
		public void SeedDatabase(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AccountFiduciary>().HasData(
				new AccountFiduciary
				{
					CodAccount=1,
					Type= "Fiduciary",
					CodUser=1,
					IsActive=true,
					CBU="111111",
					Alias="gabriel.baigorria.cw",
					AccountNumber="123",
					BalancePeso=250,
					BalanceUsd=10
				}
				);
			modelBuilder.Entity<AccountCripto>().HasData(
				new AccountCripto
				{
					CodAccount = 2,
					Type = "Cripto",
					CodUser = 1,
					IsActive = true,
					DirectionUUID="asd123",
					BalanceBtc= 5
				}
				);
		}
	}
}
