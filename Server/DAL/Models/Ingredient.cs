using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodPlanner.DAL.Enums;

namespace FoodPlanner.DAL.Models;

public class Ingredient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public ShopItem Item { get; set; } = null!;
    public float Quantity { get; set; }
    public Unit Unit { get; set; }
}