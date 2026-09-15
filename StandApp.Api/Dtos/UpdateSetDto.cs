using System.ComponentModel.DataAnnotations;

namespace StandApp.Api.Dtos;

public record UpdateSetDto(
    [Required][StringLength(50)] string Name,
    [Required] TimeOnly Duration,
    [Required] DateOnly Date,
    [StringLength(100)] string Location,
    [Range(1,5)] int? Rating,
    [StringLength(5000)] string? Notes
);