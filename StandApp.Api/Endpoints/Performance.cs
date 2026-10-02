using Microsoft.EntityFrameworkCore;
using StandApp.Api.Data;
using StandApp.Api.Dtos;
using StandApp.Api.Models;

namespace StandApp.Api.Endpoints;

public static class PerformancesEndpoints {
    public static void MapPerformancesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/Performances");

        // GET /Performances
        group.MapGet("/", async (StandAppContext dbContext) => await dbContext.Performances.Select(Performance => new PerformanceResponseDto(
                Performance.Id,
                Performance.Name,
                Performance.Duration,
                Performance.Date,
                Performance.Location,
                Performance.Status,
                Performance.Rating,
                Performance.Notes
                )).AsNoTracking().ToListAsync());

        // GET /Performances/:id
        group.MapGet("/{id}", async (int id, StandAppContext dbContext) =>
        {
            var Performance = await dbContext.Performances.FindAsync(id);
            return Performance is null ? Results.NotFound() : Results.Ok(
                new PerformanceResponseDto(
                Performance.Id,
                Performance.Name,
                Performance.Duration,
                Performance.Date,
                Performance.Location,
                Performance.Status,
                Performance.Rating,
                Performance.Notes
                )
            );
        })
        .WithName("GetPerformance");

        // POST /Performances
        group.MapPost("/", async (CreatePerformanceDto newPerformance, StandAppContext dbContext) =>
        {
            Performance Performance = new() 
            {
                Name = newPerformance.Name,
                Duration = newPerformance.Duration,
                Date = newPerformance.Date,
                Location = newPerformance.Location
            };

            dbContext.Add(Performance);
            await dbContext.SaveChangesAsync();


            PerformanceResponseDto PerformanceDto = new(
                Performance.Id,
                Performance.Name,
                Performance.Duration,
                Performance.Date,
                Performance.Location,
                Performance.Status,
                Performance.Rating,
                Performance.Notes
            );

            return Results.CreatedAtRoute("GetPerformance", new {id = PerformanceDto.Id}, PerformanceDto);
        });

        // PUT /Performances/:id
        group.MapPut("/{id}", async (int id, UpdatePerformanceDto updatedPerformance, StandAppContext dbContext) =>
        {
            var existingPerformance = await dbContext.Performances.FindAsync(id);

            if (existingPerformance is null)
            {
                return Results.NotFound();
            }

            existingPerformance.Name = updatedPerformance.Name;
            existingPerformance.Duration = updatedPerformance.Duration;
            existingPerformance.Date = updatedPerformance.Date;
            existingPerformance.Location = updatedPerformance.Location;
            existingPerformance.Rating = updatedPerformance.Rating;
            existingPerformance.Notes = updatedPerformance.Notes;
            
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // PATCH /Performances/:id
        group.MapPatch("/{id}/complete", async (int id, CompletePerformanceDto completePerformance, StandAppContext dbContext) =>
        {
            var existingPerformance = await dbContext.Performances.FindAsync(id);

            if (existingPerformance is null)
            {
                return Results.NotFound();
            }

            if (existingPerformance.Status is PerformanceStatus.pending)
            {
                existingPerformance.Status = PerformanceStatus.completed;
            }
            
            existingPerformance.Rating = completePerformance.Rating;
            existingPerformance.Notes = completePerformance.Notes;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /Performances/:id
        group.MapDelete("/{id}", async (int id, StandAppContext dbContext) =>
        {
            await dbContext.Performances.Where(Performance => Performance.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }
}