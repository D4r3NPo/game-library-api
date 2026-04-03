using GameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
}