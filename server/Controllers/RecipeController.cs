using FoodPlanner.DAL;
using FoodPlanner.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodPlanner.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly DataContext _db;
    public RecipeController(DataContext db) => _db = db;

    [HttpGet]
    public IActionResult Get()
    {
        var recipes = _db.Recipes.Include(x=>x.Ingredients).ThenInclude(x=>x.Item).ToList();
        return Ok(recipes);
    }

    [HttpGet("{id:long}")]
    public IActionResult Get(long id) => Ok(_db.Recipes.Include(x=>x.Ingredients).ThenInclude(x=>x.Item).First(x => x.Id == id));

    [HttpGet("names")]
    public IActionResult GetNames() => Ok(_db.Recipes.ToDictionary(x => x.Id, x => x.Name));
    
    [HttpPost("create")]
    public IActionResult Post(Recipe recipe)
    {
        var ingredients = new List<Ingredient>(recipe.Ingredients);
        recipe.Ingredients = new List<Ingredient>();
        foreach (var ingredient in ingredients)
        {
            recipe.Ingredients.Add(_db.Ingredients.FirstOrDefault((x=>x.Id == ingredient.Id))!);
        }
        _db.Recipes.Add(recipe);
        _db.SaveChanges();
        return Ok(recipe);
    }

    [HttpPut("update")]
    public IActionResult Update(Recipe recipe)
    {
        _db.Recipes.Update(recipe);
        _db.SaveChanges();
        return Ok(recipe);
    }
}