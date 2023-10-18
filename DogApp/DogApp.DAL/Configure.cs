using DogApp.DAL.Data;
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
	}
}
