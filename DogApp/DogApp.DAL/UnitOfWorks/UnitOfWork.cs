using DogApp.DAL.Data;
using DogApp.DAL.Repositories;

namespace DogApp.DAL.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
	private bool _disposed;
	private IDogRepository _dogRepository;

    public UnitOfWork(DogAppDbContext dbContext)
    {
		DbContext=dbContext;
	}

    public IDogRepository DogRepository => _dogRepository ??= new DogRepository(DbContext);

	protected DogAppDbContext DbContext { get; }

	public async Task SaveAsync()
	{
		await DbContext.SaveChangesAsync();
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed && disposing)
		{
			DbContext.Dispose();
		}

		_disposed = true;
	}
}
