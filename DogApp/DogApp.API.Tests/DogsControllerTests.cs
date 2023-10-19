using AutoMapper;
using DogApp.API.Controllers;
using DogApp.API.Models;
using DogApp.BLL.DTOs;
using DogApp.BLL.Services;
using DogApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Moq;

namespace DogApp.API.Tests;

public class DogsControllerTests
{
	private Mock<IDogService> _mockDogService;
	private Mock<IMapper> _mockMapper;
	private Mock<IOutputCacheStore> _mockCacheStore;
	private DogsController _controller;

	[SetUp]
	public void Setup()
	{
		_mockDogService = new Mock<IDogService>();
		_mockMapper = new Mock<IMapper>();
		_mockCacheStore = new Mock<IOutputCacheStore>();
		_controller = new DogsController(_mockDogService.Object, _mockMapper.Object);
	}

	[Test]
	public void Ping_ShouldReturnServiceVersion()
	{
		// Act
		var result = _controller.Ping();

		// Assert
		Assert.That(result, Is.InstanceOf<OkObjectResult>());
		Assert.That(((OkObjectResult)result).Value, Is.EqualTo("Dogshouseservice.Version1.0.1"));
	}

	[Test]
	public async Task GetAll_ShouldReturnAllDogs()
	{
		// Arrange
		var dogs = new List<Dog>
		{
			new Dog { Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 },
			new Dog { Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14 }
		};

		var dogDtos = new List<DogDto>
		{
			new DogDto { Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 },
			new DogDto { Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14 }
		};


		_mockDogService.Setup(ds => ds.GetAllAsync(null, true, 1, 5)).ReturnsAsync(dogs);
		_mockMapper.Setup(m => m.Map<List<DogDto>>(dogs)).Returns(dogDtos);

		// Act
		var response = await _controller.GetAll(null, null);

		// Assert
		var okResult = response as OkObjectResult;
		Assert.That(okResult, Is.Not.Null);
		Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<DogDto>>());
		Assert.That(((IEnumerable<DogDto>)okResult.Value).Count(), Is.EqualTo(2));
	}

	[Test]
	public async Task Create_ShouldCreateDog()
	{
		// Arrange
		var addDogRequest = new AddDogRequest { Name = "IceCube", Color = "black", TailLength = 20, Weight = 30 };
		var dog = new Dog { Name = "IceCube", Color = "black", TailLength = 20, Weight = 30 };

		_mockMapper.Setup(m => m.Map<Dog>(addDogRequest)).Returns(dog);
		_mockDogService.Setup(d => d.CreateAsync(dog)).ReturnsAsync(dog);
		_mockCacheStore.Setup(c => c.EvictByTagAsync(It.IsAny<string>(), default)).Returns(ValueTask.CompletedTask);

		// Act
		var result = await _controller.Create(addDogRequest, _mockCacheStore.Object);

		// Assert
		Assert.That(result, Is.InstanceOf<OkObjectResult>());
		_mockDogService.Verify(d => d.CreateAsync(dog), Times.Once);
		_mockCacheStore.Verify(c => c.EvictByTagAsync("tag-dog", default), Times.Once);
	}
}
