using System.ComponentModel.DataAnnotations;
using FoodPlanner.DAL.Enums;

namespace FoodPlanner.DAL.Models;

public class ShopItem
{
    [Key] [StringLength(50)] public string Name { get; set; } = null!;
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public Unit Unit { get; set; }
}