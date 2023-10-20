using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.DataBaseSeeding
{
	public interface IEntitySeeder
	{
		void SeedDatabase(ModelBuilder modelBuilder);
	}
}
