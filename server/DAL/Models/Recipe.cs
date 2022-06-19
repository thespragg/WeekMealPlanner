using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodPlanner.DAL.Models;

public class Recipe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [StringLength(50)] public string Name { get; set; } = null!;
    public List<Ingredient> Ingredients { get; set; } = new();
}