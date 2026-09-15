namespace StandApp.Api.Dtos;

public record UpdateSetDto(
    string Name,
    TimeOnly Duration,
    DateOnly Date,
    string Location,
    int? Rating,
    string? Notes
);