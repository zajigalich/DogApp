using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DogApp.API.Models;

public class AddDogRequest
{
	[JsonPropertyName("name")]
	[Required]
	[MaxLength(20, ErrorMessage = "Dog name must not exceed 20 characters limit")]
	[MinLength(2, ErrorMessage = "Dog name must be at least 2 characters long")]
	public string Name { get; set; }

	[JsonPropertyName("color")]
	[Required]
	[MaxLength(30, ErrorMessage = "Color must not exceed 30 characters limit")]
	[MinLength(2, ErrorMessage = "Color must be at least 2 characters long")]
	public string Color { get; set; }

	[JsonPropertyName("tail_length")]
	[Required]
	[Min(1, ErrorMessage = "Minimum tail length is 1")]
	[Max(300, ErrorMessage = "Maximum tail length is 300")]
	public short TailLength { get; set; }

	[JsonPropertyName("weight")]
	[Required]
	[Min(1, ErrorMessage = "Minimum weight is 1")]
	[Max(300, ErrorMessage = "Maximum weight is 300")]
	public short Weight { get; set; }
}
