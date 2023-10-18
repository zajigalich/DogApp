using DogApp.DAL.Data;
using DogApp.DAL.Entities;
using DogApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DogApp.DAL.Tests;

public class DogRepositoryTests
{
	private DogAppDbContext _dbContext;
	private DogRepository _dogRepository;

	[SetUp]
	public void SetUp()
	{
		/*It would be better to use only test data
		  (I would use smth like _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.dogs;") and seed data more sutable for testing)
		*/
		var options = new DbContextOptionsBuilder<DogAppDbContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;

		_dbContext = new DogAppDbContext(options);
		_dbContext.Database.EnsureCreated();
		_dogRepository = new DogRepository(_dbContext);
	}

	[TearDown]
	public void TearDown()
	{
		_dbContext.Database.EnsureDeleted();
		_dbContext.Dispose();
	}

	[Test]
	public async Task GetAllAsync_ShouldReturnCorrectNumberOfDogs()
	{
		//Arrange
		var pageSize = 10000;

		//Act
		var dogs = await _dogRepository.GetAllAsync(pageSize: pageSize);

		//Assert
		Assert.That(dogs.Count(), Is.EqualTo(9));
	}

	[Test]
	public async Task CreateAsync_ShouldAddDogToDatabase()
	{
		//Arrange
		var pageSize = 10000;
		var dog = new Dog
		{
			Name = "DrDre",
			Color = "Brown",
			TailLength = 15,
			Weight = 30
		};

		// Act
		await _dogRepository.CreateAsync(dog);
		await _dbContext.SaveChangesAsync();

		var dogs = await _dogRepository.GetAllAsync(pageSize: pageSize);

		// Assert
		Assert.That(dogs.Count(), Is.EqualTo(10));
		Assert.That(dogs.Any(x => x.Name == "DrDre"), Is.True);
	}

	[Test]
	public async Task GetAllAsync_ShouldReturnPaginatedResults()
	{
		//Arrange
		int pageNumber = 2;
		int pageSize = 3;

		//Act
		var dogs = await _dogRepository.GetAllAsync(pageNumber: pageNumber, pageSize: pageSize);

		//Assert
		Assert.That(dogs.Count(), Is.EqualTo(3));
	}

	[Test]
	public async Task GetAllAsync_ShouldReturnOrderedResults()
	{
		//Arrange
		int pageSize = 10000;
		string sortBy = "name";
		bool isAscending = true;

		//Act
		var dogs = await _dogRepository.GetAllAsync(sortBy, isAscending, pageSize: pageSize);

		//Assert
		Assert.That(dogs.First().Name, Is.EqualTo("50Cent"));
		Assert.That(dogs.Last().Name, Is.EqualTo("Snoop"));
	}

	[Test]
	public async Task GetAllAsync_WithPaginationAndOrdering_ShouldReturnCorrectResults()
	{
		//Arrange
		int pageNumber = 2;
		int pageSize = 3;
		string sortBy = "name";
		bool isAscending = false;

		//Act
		var dogs = await _dogRepository.GetAllAsync(sortBy, isAscending, pageNumber, pageSize);

		//Assert
		Assert.That(dogs.Count(), Is.EqualTo(3));
		Assert.That(dogs.First().Name, Is.EqualTo("Kanye West"));
		Assert.That(dogs.Last().Name, Is.EqualTo("IceCube"));
	}
}