using System.ComponentModel.DataAnnotations;

namespace StandApp.Api.Models;

public enum PerformanceStatus
{
    pending,
    completed
}

public class Performance
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required TimeOnly Duration { get; set; }

    public required DateOnly Date { get; set; }

    public string? Location { get; set; }

    public PerformanceStatus Status { get; set; } = PerformanceStatus.pending;

    public int? Rating { get; set; }

    public string? Notes { get; set; }
}