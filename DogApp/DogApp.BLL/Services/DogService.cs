using DogApp.DAL.Entities;
using DogApp.DAL.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace DogApp.BLL.Services;

public class DogService : IDogService
{
	private readonly IUnitOfWork _unitOfWork;

	public DogService(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<Dog>> GetAllAsync(string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 5)
	{
		VerifyPageNumberIsValid(pageNumber);

		VerifyPageSizeIsValid(pageSize);

		return await _unitOfWork.DogRepository.GetAllAsync(sortBy, isAscending, pageNumber, pageSize, asNoTracking: true);
	}

	public async Task<Dog> CreateAsync(Dog dog)
	{
		await _unitOfWork.DogRepository.CreateAsync(dog);

		try
		{
			await _unitOfWork.SaveAsync();
		}
		catch (DbUpdateException)
		{
			throw new DogNameAlreadyExistsException(dog.Name);
		}

		return dog;
	}

	private static void VerifyPageSizeIsValid(int pageSize)
	{
		if (pageSize < 1)
		{
			throw new ArgumentException("Page size must be greater or equal 1", nameof(pageSize));
		}
	}

	private static void VerifyPageNumberIsValid(int pageNumber)
	{
		if (pageNumber < 1)
		{
			throw new ArgumentException("Page number must be greater or equal 1", nameof(pageNumber));
		}
	}
}
