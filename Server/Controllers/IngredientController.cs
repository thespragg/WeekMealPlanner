using FoodPlanner.DAL;
using FoodPlanner.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Controllers;

[ApiController]
[Route("api/ingredients")]
public class IngredientController : ControllerBase
{
    private readonly DataContext _db;
    public IngredientController(DataContext db) => _db = db;

    [HttpGet]
    public IActionResult Get() => Ok(_db.Ingredients.ToList());

    [HttpGet("{id:long}")]
    public IActionResult Get(long id) => Ok(_db.Ingredients.FirstOrDefault(x => x.Id == id));
    
    [HttpPost("create")]
    public IActionResult Post(Ingredient ingredient)
    {
        _db.Ingredients.Add(ingredient);
        _db.SaveChanges();
        return Ok();
    }

    [HttpPut("update")]
    public IActionResult Update(Ingredient ingredient)
    {
        _db.Update(ingredient);
        _db.SaveChanges();
        return Ok();
    }
}