using DogApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogApp.DAL.Data;

public static class DataSeedModelBuilderExtensions
{
    public static void SeedDogs(this ModelBuilder modelBuilder)
    {
        var dogs = new List<Dog>
        {
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Jessy",
                Color = "black & white",
                TailLength = 7,
                Weight = 14,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "IceCube",
                Color = "black",
                TailLength = 22,
                Weight = 38,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Snoop",
                Color = "black",
                TailLength = 22,
                Weight = 38,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Eminem",
                Color = "white",
                TailLength = 14,
                Weight = 13,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "50Cent",
                Color = "brown",
                TailLength = 14,
                Weight = 44,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Babangida",
                Color = "orange",
                TailLength = 5,
                Weight = 12,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Phife Dawg",
                Color = "black",
                TailLength = 4,
                Weight = 9,
            },
            new Dog
            {
                Id = Guid.NewGuid(),
                Name = "Kanye West",
                Color = "black & brown",
                TailLength = 10,
                Weight = 30,
            },
        };

        modelBuilder.Entity<Dog>().HasData(dogs);
    }
}
