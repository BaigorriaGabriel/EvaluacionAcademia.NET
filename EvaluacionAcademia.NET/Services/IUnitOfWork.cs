namespace EvaluacionAcademia.NET.Services
{
	public interface IUnitOfWork
	{
		//public UserRepository UserRepository { get; }

		//public ServiceRepository ServiceRepository { get; }

		//public ProjectRepository ProjectRepository { get; }

		//public JobRepository JobRepository { get; }

		Task<int> Complete();
	}
}
