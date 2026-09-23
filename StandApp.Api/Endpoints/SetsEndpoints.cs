using Microsoft.EntityFrameworkCore;
using StandApp.Api.Data;
using StandApp.Api.Dtos;
using StandApp.Api.Models;

namespace StandApp.Api.Endpoints;

public static class SetsEndpoints {
    public static void MapSetsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/sets");

        // GET /sets
        group.MapGet("/", async (StandAppContext dbContext) => await dbContext.Sets.Select(set => new SetDto(
                set.Id,
                set.Name,
                set.Duration,
                set.Date,
                set.Location,
                set.Status.ToString(),
                set.Rating,
                set.Notes
                )).AsNoTracking().ToListAsync());

        // GET /sets/:id
        group.MapGet("/{id}", async (int id, StandAppContext dbContext) =>
        {
            var set = await dbContext.Sets.FindAsync(id);
            return set is null ? Results.NotFound() : Results.Ok(
                new SetDto(
                set.Id,
                set.Name,
                set.Duration,
                set.Date,
                set.Location,
                set.Status.ToString(),
                set.Rating,
                set.Notes
                )
            );
        })
        .WithName("GetSet");

        // POST /sets
        group.MapPost("/", async (CreateSetDto newSet, StandAppContext dbContext) =>
        {
            Set set = new() 
            {
                Name = newSet.Name,
                Duration = newSet.Duration,
                Date = newSet.Date,
                Location = newSet.Location
            };

            dbContext.Add(set);
            await dbContext.SaveChangesAsync();


            SetDto setDto = new(
                set.Id,
                set.Name,
                set.Duration,
                set.Date,
                set.Location,
                set.Status.ToString(),
                set.Rating,
                set.Notes
            );

            return Results.CreatedAtRoute("GetSet", new {id = setDto.Id}, setDto);
        });

        // PUT /sets/:id
        group.MapPut("/{id}", async (int id, UpdateSetDto updatedSet, StandAppContext dbContext) =>
        {
            var existingSet = await dbContext.Sets.FindAsync(id);

            if (existingSet is null)
            {
                return Results.NotFound();
            }

            existingSet.Name = updatedSet.Name;
            existingSet.Duration = updatedSet.Duration;
            existingSet.Date = updatedSet.Date;
            existingSet.Location = updatedSet.Location;
            existingSet.Rating = updatedSet.Rating;
            existingSet.Notes = updatedSet.Notes;
            
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // PATCH /sets/:id
        group.MapPatch("/{id}/complete", async (int id, CompleteSetDto completeSet, StandAppContext dbContext) =>
        {
            var existingSet = await dbContext.Sets.FindAsync(id);

            if (existingSet is null)
            {
                return Results.NotFound();
            }

            if (existingSet.Status is SetStatus.pending)
            {
                existingSet.Status = SetStatus.completed;
            }
            
            existingSet.Rating = completeSet.Rating;
            existingSet.Notes = completeSet.Notes;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /sets/:id
        group.MapDelete("/{id}", async (int id, StandAppContext dbContext) =>
        {
            await dbContext.Sets.Where(set => set.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }
}