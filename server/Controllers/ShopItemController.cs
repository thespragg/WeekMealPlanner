using FoodPlanner.DAL;
using FoodPlanner.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Controllers;

[ApiController]
[Route("api/shopitems")]
public class ShopItemController : ControllerBase
{
    private readonly DataContext _db;
    public ShopItemController(DataContext db) => _db = db;

    [HttpGet]
    public IActionResult Get() => Ok(_db.ShopItems.ToList());

    [HttpGet("{id:long}")]
    public IActionResult Get(long id) => Ok(_db.ShopItems.FirstOrDefault(x => x.Id == id));

    [HttpPost("create")]
    public IActionResult Create(ShopItem shopItem)
    {
        _db.ShopItems.Add(shopItem);
        _db.SaveChanges();
        return Ok(shopItem);
    }

    [HttpPut("update")]
    public IActionResult Update(ShopItem shopItem)
    {
        _db.ShopItems.Update(shopItem);
        _db.SaveChanges();
        return Ok(shopItem);
    }
}