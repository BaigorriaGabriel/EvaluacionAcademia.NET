using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.Entities;
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

		public virtual async Task<T> GetById(T entity)
		{
			return entity;

		}

		
		public virtual Task<bool> Delete(T entity)
		{
			throw new NotImplementedException();
		}

		public virtual Task<bool> Update(T entity)
		{
			throw new NotImplementedException();
		}

		
	}
}
