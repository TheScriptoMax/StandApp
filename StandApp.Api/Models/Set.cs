using System.ComponentModel.DataAnnotations;

namespace StandApp.Api.Models;

public enum SetStatus
{
    pending,
    completed
}

public class Set
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required TimeOnly Duration { get; set; }

    public required DateOnly Date { get; set; }

    public string? Location { get; set; }

    public SetStatus Status { get; set; } = SetStatus.pending;

    public int? Rating { get; set; }

    public string? Notes { get; set; }
}