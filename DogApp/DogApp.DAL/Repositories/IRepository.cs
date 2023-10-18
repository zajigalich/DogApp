using DogApp.DAL.Entities;

namespace DogApp.DAL.Repositories;

public interface IRepository<T> 
	where T : Entity
{
	Task<T> CreateAsync(T entity);
	Task<IEnumerable<T>> GetAllAsync(string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 5, bool asNoTracking = false);
}
