using StandApp.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
List<SetDto> sets = [
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

// GET /sets
app.MapGet("/sets", () => sets);

// GET /sets/:id
app.MapGet("/sets/{id}", (int id) => sets.Find(set => set.Id == id))
.WithName("GetSet");

// POST /sets
app.MapPost("/sets", (CreateSetDto newSet) =>
{
    Console.WriteLine(newSet);
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

app.Run();
