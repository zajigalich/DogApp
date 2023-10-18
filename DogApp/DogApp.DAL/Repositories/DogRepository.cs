using DogApp.DAL.Data;
using DogApp.DAL.Entities;

namespace DogApp.DAL.Repositories;

public class DogRepository : BaseRepositoty<Dog>, IDogRepository
{
	public DogRepository(DogAppDbContext dbContext) 
		: base(dbContext)
	{
	}
}
