using DogApp.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DogApp.BLL;

public static class Configure
{
	public static void ConfigureBusinessLogicLayerServices(this IServiceCollection services)
	{
		// Add services to container
		services.AddScoped<IDogService, DogService>();
	}
}