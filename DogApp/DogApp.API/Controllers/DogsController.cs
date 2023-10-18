using AutoMapper;
using DogApp.API.Models;
using DogApp.BLL.DTOs;
using DogApp.BLL.Services;
using DogApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace DogApp.API.Controllers;

[ApiController]
public class DogsController : ControllerBase
{
	private const string DOG_HOUSE_SERVICE_VERSION = "Dogshouseservice.Version1.0.1";

	private readonly IDogService _dogService;
	private readonly IMapper _mapper;

	public DogsController(IDogService dogService, IMapper mapper)
	{
		_dogService = dogService;
		_mapper = mapper;
	}

	[HttpGet]
	[Route("ping")]
	public IActionResult Ping()
	{
		return Ok(DOG_HOUSE_SERVICE_VERSION);
	}

	[HttpGet]
	[Route("dogs")]
	[OutputCache(VaryByQueryKeys = new [] {"attribute", "order", "pageNumber", "pageSize"} )]
	public async Task<IActionResult> GetAll([FromQuery] string? attribute, [FromQuery] string? order, 
		[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
	{
		var dogs = await _dogService.GetAllAsync(sortBy: attribute,
			isAscending: order != "desc", pageNumber, pageSize);

		return Ok(_mapper.Map<List<DogDto>>(dogs));
	}

	[HttpPost]
	[Route("dog")]
	public async Task<IActionResult> Create([FromBody] AddDogRequest addDogRequest, IOutputCacheStore cacheStore)
	{
		var dog = _mapper.Map<Dog>(addDogRequest);

		await cacheStore.EvictByTagAsync("tag-dog", default);

		await _dogService.CreateAsync(dog);

		return Ok(_mapper.Map<DogDto>(dog));
	}
}