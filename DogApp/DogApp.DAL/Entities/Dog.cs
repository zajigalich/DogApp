using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DogApp.DAL.Entities;

[Table("dogs")]
[PrimaryKey(nameof(Id))]
[Index(nameof(Name), IsUnique = true)]
public class Dog : Entity
{

    [Column("name")]
    [MaxLength(20)]
    [NotNull]
    public string Name { get; set; } = string.Empty;

    [Column("color")]
    [MaxLength(30)]
    [NotNull]
    public string Color { get; set; } = string.Empty;

    [Column("tail_length")]
    [NotNull]
    public short TailLength { get; set; }

	[Column("weight")]
	[NotNull]
	public short Weight { get; set; }

}
