using System.ComponentModel.DataAnnotations;

namespace FoodPlanner.DAL.Models;

public class Recipe
{
    [Key] [StringLength(50)] public string Name { get; set; } = null!;
    public List<Ingredient> Ingredients { get; set; } = new();
}