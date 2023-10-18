using System.Text.Json.Serialization;

namespace DogApp.BLL.DTOs;

public class DogDto
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

	[JsonPropertyName("color")]
	public string Color { get; set; }

    [JsonPropertyName("tail_length")]
    public int TailLength { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }
}
