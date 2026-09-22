using Microsoft.EntityFrameworkCore;
using StandApp.Api.Data;
using Npgsql;
using StandApp.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

string? dbConnectionString = builder.Configuration.GetConnectionString("database");

builder.Services.AddValidation();


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
