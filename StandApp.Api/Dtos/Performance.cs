using System.ComponentModel.DataAnnotations;
using StandApp.Api.Models;

namespace StandApp.Api.Dtos;


public record PerformanceResponseDto(
    int Id,
    string Name,
    TimeOnly Duration,
    DateOnly Date,
    string? Location,
    PerformanceStatus Status,
    int? Rating,
    string? Notes
);
public record CreatePerformanceDto(
    [Required][StringLength(50)] string Name,
    [Required] TimeOnly Duration,
    [Required] DateOnly Date,
    [StringLength(100)] string Location
);

public record UpdatePerformanceDto(
    [Required][StringLength(50)] string Name,
    [Required] TimeOnly Duration,
    [Required] DateOnly Date,
    [StringLength(100)] string Location,
    [Range(1,5)] int? Rating,
    [StringLength(5000)] string? Notes
);

public record CompletePerformanceDto(
    [Range(1,5)] int Rating,
    [StringLength(5000)] string? Notes
);
