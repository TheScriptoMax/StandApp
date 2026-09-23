using StandApp.Api.Models;

namespace StandApp.Api.Dtos;


public record SetDto(
    int Id,
    string Name,
    TimeOnly Duration,
    DateOnly Date,
    string? Location,
    SetStatus Status,
    int? Rating,
    string? Notes
);