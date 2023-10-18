using System.ComponentModel.DataAnnotations.Schema;

namespace DogApp.DAL.Entities;

public abstract class Entity
{
    [Column("id")]
    public Guid Id { get; set; }
}
