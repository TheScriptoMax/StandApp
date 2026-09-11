namespace StandApp.Api.Dtos;

public record CreateSetDto(
    string Name,
    TimeOnly Duration,
    DateOnly Date,
    string Location
);