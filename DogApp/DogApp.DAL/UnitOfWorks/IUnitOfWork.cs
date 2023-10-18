using DogApp.DAL.Repositories;

namespace DogApp.DAL.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IDogRepository DogRepository { get; }

    Task SaveAsync();
}
