using DogApp.DAL;
using DogApp.BLL;
using DogApp.API.Mappings;
using DogApp.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add controllers.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add mapper
builder.Services.AddAutoMapper(typeof(DogMappingProfile));

// Configure services from class libs
builder.Services.ConfigureBusinessLogicLayerServices();
builder.Services.ConfigureDataAccessLayerServices(builder.Configuration.GetConnectionString("DogsConnectionString")!);

// Add service for caching
builder.Services.AddOutputCache(options =>
{
	options.AddBasePolicy(builder =>
		builder
		.Tag("tag-dog")
		.Expire(TimeSpan.FromSeconds(1)));
		//.Expire(TimeSpan.FromSeconds(60))); makes impossible to test 429 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

app.UseOutputCache();

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
app.UseMiddleware(typeof(RateLimitingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
