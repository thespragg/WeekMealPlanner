using FoodPlanner.DAL;
using FoodPlanner.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPlanner.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly DataContext _db;
    public RecipeController(DataContext db) => _db = db;

    [HttpGet]
    public IActionResult Get() => Ok(_db.Recipes.ToList());

    [HttpGet("{id:long}")]
    public IActionResult Get(long id) => Ok(_db.Recipes.First(x => x.Id == id));

    [HttpPost("create")]
    public IActionResult Post(Recipe recipe)
    {
        _db.Recipes.Add(recipe);
        _db.SaveChanges();
        return Ok();
    }

    [HttpPut("update")]
    public IActionResult Update(Recipe recipe)
    {
        _db.Recipes.Update(recipe);
        _db.SaveChanges();
        return Ok();
    }
}