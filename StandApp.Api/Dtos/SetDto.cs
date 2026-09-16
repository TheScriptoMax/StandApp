namespace StandApp.Api.Dtos;

public record SetDto(
    int Id,
    string Name,
    TimeOnly Duration,
    DateOnly Date,
    string? Location,
    int? Rating,
    string? Notes
);