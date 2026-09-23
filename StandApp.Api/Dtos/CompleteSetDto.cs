using System.ComponentModel.DataAnnotations;

namespace StandApp.Api.Dtos;

public record CompleteSetDto(
    [Range(1,5)] int Rating,
    [StringLength(5000)] string? Notes
);