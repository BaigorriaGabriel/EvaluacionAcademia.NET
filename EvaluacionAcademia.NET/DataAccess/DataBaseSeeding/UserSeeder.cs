using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.DataBaseSeeding
{
	public class UserSeeder : IEntitySeeder
	{
		public void SeedDatabase(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
					new User
					{
						CodUser = 1,
						Name = "Gabriel Baigorria",
						Email = "gabi.2912@hotmail.com",
						Dni = "44504788",
						//RoleId = 1,
						Password = "1234",
						//Password = PasswordEncryptHelper.EncryptPassword("1234", "gabi.2912@hotmail.com"),
						IsActive = true
					}
				);
		}
	}
}
