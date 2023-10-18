using DogApp.BLL.Exceptions;
using DogApp.BLL.Services;
using DogApp.DAL.Entities;
using DogApp.DAL.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DogApp.BLL.Tests;

public class DogServiceTests
{
	private Mock<IUnitOfWork> _mockUnitOfWork;
	private DogService _dogService;

	[SetUp]
	public void Setup()
	{
		_mockUnitOfWork = new Mock<IUnitOfWork>();
		_dogService = new DogService(_mockUnitOfWork.Object);
	}

	[Test]
	public async Task GetAllAsync_ShouldReturnAllDogs()
	{
		// Arrange
		var testDogs = new List<Dog>
		{
			new Dog { Name = "Neo", Color = "Red&Amber", TailLength = 22, Weight = 32 },
			new Dog { Name = "Jessy", Color = "Black&White", TailLength = 7, Weight = 14 }
		};

		_mockUnitOfWork.Setup(u => u.DogRepository.GetAllAsync(null, true, 1, 5, true))
			.ReturnsAsync(testDogs);

		// Act
		var dogs = await _dogService.GetAllAsync();

		// Assert
		Assert.That(dogs.Count(), Is.EqualTo(2));
	}

	[Test]
	public async Task CreateAsync_ShouldCreateNewDog()
	{
		// Arrange
		var dogs = new Dog { Name = "NewDog", Color = "Brown", TailLength = 10, Weight = 20 };
		_mockUnitOfWork.Setup(u => u.DogRepository.CreateAsync(dogs))
			.ReturnsAsync(dogs);
		_mockUnitOfWork.Setup(u => u.SaveAsync()).Returns(Task.CompletedTask);

		// Act
		var dog = await _dogService.CreateAsync(dogs);

		// Assert
		Assert.That(dog.Name, Is.EqualTo("NewDog"));
	}

	[Test]
	public void CreateAsync_ShouldThrowException_WhenDogNameAlreadyExists()
	{
		// Arrange
		var fakeDog = new Dog { Name = "ExistingDog", Color = "Brown", TailLength = 10, Weight = 20 };

		_mockUnitOfWork.Setup(u => u.DogRepository.CreateAsync(fakeDog)).ReturnsAsync(fakeDog);
		_mockUnitOfWork.Setup(u => u.SaveAsync()).Throws(new DbUpdateException());

		// Act
		var ex = Assert.ThrowsAsync<DogNameAlreadyExistsException>(() => _dogService.CreateAsync(fakeDog));

		// Assert
		Assert.That(ex.Message, Is.EqualTo($"Dog with name {fakeDog.Name} alredy exists"));
	}
}
