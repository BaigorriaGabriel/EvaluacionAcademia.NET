using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly ApplicationDbContext _context;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
		}

		public virtual async Task<List<T>> GetAllActive()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public virtual async Task<bool> Insert(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return true;
		}
	}
}
