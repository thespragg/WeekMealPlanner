using FoodPlanner.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.DAL;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DataContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    
    public DbSet<Recipe> Recipes { get; set; } = null!;
    public DbSet<ShopItem> ShopItems { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
}