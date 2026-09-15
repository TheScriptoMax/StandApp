using StandApp.Api.Dtos;

namespace StandApp.Api.Endpoints;

public static class SetsEndpoints {
    private static readonly List<SetDto> sets = [
        new (
            1, 
            "Terminal", 
            new TimeOnly(0, 5), 
            new DateOnly(2026, 9, 2), 
            "1875 Avenue du Mont-Royal Est, Montréal, QC",
            null,
            null
            )
    ];

    public static void MapSetsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/sets");

        // GET /sets
        group.MapGet("/", () => sets);

        // GET /sets/:id
        group.MapGet("/{id}", (int id) =>
        {
            var set = sets.Find(set => set.Id == id);

            return set is null ? Results.NotFound() : Results.Ok(set);
        })
        .WithName("GetSet");

        // POST /sets
        group.MapPost("/", (CreateSetDto newSet) =>
        {
            SetDto set = new(
                sets.Count + 1,
                newSet.Name,
                newSet.Duration,
                newSet.Date,
                newSet.Location,
                null,
                null
            );

            sets.Add(set);

            return Results.CreatedAtRoute("GetSet", new {id = set.Id}, set);
        });

        // PUT /sets/:id
        group.MapPut("/{id}", (int id, UpdateSetDto updatedSet) =>
        {
            var index = sets.FindIndex(set => set.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            sets[index] = new SetDto(
                id,
                updatedSet.Name,
                updatedSet.Duration,
                updatedSet.Date,
                updatedSet.Location,
                updatedSet.Rating,
                updatedSet.Notes
            );

            return Results.NoContent();
        });

        // DELETE /sets/:id
        group.MapDelete("/{id}", (int id) =>
        {
            sets.RemoveAll(set => set.Id == id);

            return Results.NoContent();
        });
    }
}