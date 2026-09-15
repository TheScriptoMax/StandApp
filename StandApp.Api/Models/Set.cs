namespace StandApp.Api.Models

public class Set
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required TimeOnly Duration { get; set; }

    public required DateOnly Date { get; set; }

    public string Location { get; set; }

    public int? Rating { get; set; }

    public string? Notes { get; set; }
}