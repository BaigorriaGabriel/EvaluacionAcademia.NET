namespace EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces
{
	public interface IRepository<T> where T : class
	{
		public Task<bool> Update(T entity);
	}
}
