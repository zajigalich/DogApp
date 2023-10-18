using DogApp.DAL.Entities;

namespace DogApp.BLL.Services;

public interface IDogService
{
	Task<Dog> CreateAsync(Dog dog);
	Task<IEnumerable<Dog>> GetAllAsync(string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 5);
}
