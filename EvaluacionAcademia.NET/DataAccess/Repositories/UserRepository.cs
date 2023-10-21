using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.DTOs;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<User?> AuthenticateCredentials(AuthenticateDto dto)
		{
			//return await _context.Users.Include(x => x.Role).SingleOrDefaultAsync(x => (x.Email == dto.Email && x.Password == PasswordEncryptHelper.EncryptPassword(dto.Password, dto.Email)) && x.IsActive == true);
			return await _context.Users.SingleOrDefaultAsync(x => (x.Email == dto.Email && x.Password == dto.Password) && x.IsActive == true);
		}
	}
}
