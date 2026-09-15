using System.ComponentModel.DataAnnotations;

namespace StandApp.Api.Dtos;

public record CreateSetDto(
    [Required][StringLength(50)] string Name,
    TimeOnly Duration,
    [Required] DateOnly Date,
    [StringLength(100)] string Location
);