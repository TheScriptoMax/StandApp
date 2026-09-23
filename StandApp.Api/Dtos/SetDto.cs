using StandApp.Api.Models;

namespace StandApp.Api.Dtos;


public record SetDto(
    int Id,
    string Name,
    TimeOnly Duration,
    DateOnly Date,
    string? Location,
    string Status,
    int? Rating,
    string? Notes
);