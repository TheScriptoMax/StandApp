using Microsoft.EntityFrameworkCore;
using Npgsql;
using StandApp.Api.Models;

namespace StandApp.Api.Data;

public class StandAppContext(DbContextOptions<StandAppContext> options) : DbContext(options)
{
    public DbSet<Set> Sets => Set<Set>();
}