using EvaluacionAcademia.NET.DataAccess.DataBaseSeeding;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EvaluacionAcademia.NET.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Account> Accounts { get; set; } // Cambia el nombre de la propiedad a "Accounts" para que coincida con el DbSet.
		public DbSet<Transaction> Transactions { get; set; } 

		// Agrega los DbSet para las clases hijas
		public DbSet<AccountFiduciary> FiduciaryAccounts { get; set; }
		public DbSet<AccountCripto> CriptoAccounts { get; set; }
		public DbSet<TransactionTransfer> TransactionTransfers { get; set; }
		public DbSet<TransactionConversion> TransactionConversions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var seeders = new List<IEntitySeeder>
			{
				new UserSeeder(),
				new AccountSeeder(),
                // Agrega otros seeders si los necesitas.
            };

			foreach (var seeder in seeders)
			{
				seeder.SeedDatabase(modelBuilder);
			}

			modelBuilder.Entity<Account>()
				.ToTable("Accounts")
				.HasDiscriminator<string>("Type")
				.HasValue<Account>("Account")
				.HasValue<AccountFiduciary>("Fiduciary")
				.HasValue<AccountCripto>("Cripto");

			modelBuilder.Entity<Transaction>()
				.ToTable("Transactions")
				.HasDiscriminator<string>("Type")
				.HasValue<Transaction>("Transaction")
				.HasValue<Transaction>("Deposit")
				.HasValue<TransactionTransfer>("Transfer")
				.HasValue<TransactionConversion>("Conversion");

			base.OnModelCreating(modelBuilder);
		}
	}
}