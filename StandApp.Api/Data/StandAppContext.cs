using Microsoft.EntityFrameworkCore;
using StandApp.Api.Models;

namespace StandApp.Api.Data;

public class StandAppContext(DbContextOptions<StandAppContext> options) : DbContext(options)
{
    public DbSet<Performance> Performances => Set<Performance>();
}