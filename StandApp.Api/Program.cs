using Microsoft.EntityFrameworkCore;
using StandApp.Api.Data;
using Npgsql;
using StandApp.Api.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string? dbConnectionString = builder.Configuration.GetConnectionString("database");

builder.Services.AddValidation();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

if (dbConnectionString is string)
{
    builder.Services.AddDbContext<StandAppContext>(options => options.UseNpgsql(dbConnectionString));
    await using var conn = new NpgsqlConnection(dbConnectionString);
    await conn.OpenAsync();

    Console.WriteLine($"The PostgreSQL version: {conn.PostgreSqlVersion}");
}


var app = builder.Build();


app.MapSetsEndpoints();

app.MigrateDb();

app.Run();
