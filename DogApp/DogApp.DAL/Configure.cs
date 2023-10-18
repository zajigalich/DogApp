using DogApp.DAL.Data;
using DogApp.DAL.Repositories;
using DogApp.DAL.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DogApp.DAL;

public static class Configure
{
	public static void ConfigureDataAccessLayerServices(this IServiceCollection services, string conntectionString)
	{
		// Add Db Context
		services.AddDbContextPool<DogAppDbContext>(opt =>
			opt.UseSqlServer(conntectionString));

		// Add services to container
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IDogRepository, DogRepository>(); // dont't realy need to add because I use Unit Of Pattern
	}
}
