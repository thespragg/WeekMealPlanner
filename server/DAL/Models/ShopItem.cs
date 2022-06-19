using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodPlanner.DAL.Enums;

namespace FoodPlanner.DAL.Models;

public class ShopItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [StringLength(50)] public string Name { get; set; } = null!;
    public float Quantity { get; set; }
    public decimal Price { get; set; }
    public Unit Unit { get; set; }
}