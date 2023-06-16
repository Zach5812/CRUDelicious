using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext db;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> allDishes = db.Dishes.ToList();
        return View("Index", allDishes);
    }

[HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("New");
    }

[HttpPost("dishes/create")]
public IActionResult CreateDish(Dish newDish)
{
    if(!ModelState.IsValid)
    {
        return View("New");
    }

    db.Dishes.Add(newDish);

    db.SaveChanges();

    return RedirectToAction("Index");
}

[HttpGet("dishes/{id}")]
public IActionResult ViewDish(int id)
{
    Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishID == id);

    if(dish == null)
    {
        return RedirectToAction("Index");
    }
    return View("Details", dish);
}

[HttpPost("dishes/{dishId}/delete")]
public IActionResult Delete(int dishId)
{
    Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishID == dishId);
    if(dish == null)
    {
        return RedirectToAction("Index");
    }
    db.Dishes.Remove(dish);
    db.SaveChanges();
    return RedirectToAction("Index");
}

[HttpGet("dishes/{dishId}/edit")]
public IActionResult UpdatePage(int dishId)
{
    Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishID == dishId);
    if(dish == null)
    {
        return RedirectToAction("Index");
    }
    return View("Edit", dish);
}

[HttpPost("dishes/{dishId}/edit")]
public IActionResult Update(int dishId, Dish updatedDish)
{
    if(!ModelState.IsValid)
    {
        return UpdatePage(dishId);
    }
    Dish? originalDish = db.Dishes.FirstOrDefault(dish=> dish.DishID == dishId);
    if(originalDish == null)
    {
        return RedirectToAction("Index");
    }
    originalDish.Chef = updatedDish.Chef;
    originalDish.Name = updatedDish.Name;
    originalDish.Calories = updatedDish.Calories;
    originalDish.Tastiness = updatedDish.Tastiness;
    originalDish.Description = updatedDish.Description;
    originalDish.UpdatedAt = DateTime.Now;
    
    db.Dishes.Update(originalDish);
    db.SaveChanges();
    return RedirectToAction("ViewDish", new {id = originalDish.DishID});
}

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
