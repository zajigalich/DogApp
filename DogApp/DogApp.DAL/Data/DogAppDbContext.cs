using DogApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogApp.DAL.Data;

public class DogAppDbContext : DbContext
{
    public DogAppDbContext(DbContextOptions<DogAppDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public virtual DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Add constaints to table that weight and tail length greater than zero
		modelBuilder.Entity<Dog>()
			.ToTable(d => d.HasCheckConstraint("CK_Dogs_TailLength_GreaterThanZero", "[tail_length] > 0"));
		modelBuilder.Entity<Dog>()
			.ToTable(d => d.HasCheckConstraint("CK_Dogs_Weight_GreaterThanZero", "[weight] > 0"));

		// Seed Data
		modelBuilder.SeedDogs();
	}
}
