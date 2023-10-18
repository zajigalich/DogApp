using DogApp.DAL.Data;
using DogApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace DogApp.DAL.Repositories;

public abstract class BaseRepositoty<T> : IRepository<T>
	where T : Entity
{
	protected BaseRepositoty(DogAppDbContext dbContext)
    {
		DbContext=dbContext;
	}

	protected DogAppDbContext DbContext { get; }

	protected DbSet<T> Entities => DbContext.Set<T>();

	public virtual async Task<T> CreateAsync(T entity)
	{
		await Entities.AddAsync(entity);

		return entity;
	}

	public virtual async Task<IEnumerable<T>> GetAllAsync(string? sortBy = null, bool isAscending = true, 
		int pageNumber = 1, int pageSize = 5,
		bool asNoTracking = false)
	{
		var entities = asNoTracking ? Entities.AsNoTracking() : Entities;

		// Sorting
		if (!string.IsNullOrWhiteSpace(sortBy))
		{
			Type t = typeof(T);
			var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			var targetSortProp = Array.Find(props, p => string.Equals(p.Name, sortBy, StringComparison.OrdinalIgnoreCase));

			if (targetSortProp != null)
			{
				entities = ApplyOrderBy(isAscending, entities, targetSortProp.Name);
			}
		}

		// Pagination
		var skipResults = (pageNumber - 1) * pageSize;

		return await entities.Skip(skipResults).Take(pageSize).ToListAsync();
	}

	private static IQueryable<T> ApplyOrderBy(bool isAscending, IQueryable<T> entities, string propertyName)
	{
		var parameter = Expression.Parameter(typeof(T));
		var property = Expression.Property(parameter, propertyName);
		var propAsObject = Expression.Convert(property, typeof(object));

		var exp = Expression.Lambda<Func<T, object>>(propAsObject, parameter);

		entities = isAscending ?
			entities.OrderBy(exp) :
			entities.OrderByDescending(exp);

		return entities;
	}	
}
