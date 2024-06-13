using CodingEvents.Models;
using CodingEvents.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers;

[Route("/eventcategory")]
public class EventCategoryController : Controller
{
    private EventDbContext context;

    public EventCategoryController(EventDbContext dbContext)
    {
        context = dbContext;
    }
    public IActionResult Index() 
    {
        List<EventCategory> categories = context.Categories.ToList();
        
        return View(categories);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        AddEventCategoryViewModel addEventCategoryViewModel = new();
        return View(addEventCategoryViewModel);
    }

    [HttpPost("create")]
    public IActionResult ProcessCreateCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
    {
        if(!ModelState.IsValid)
        {
            return View("create", addEventCategoryViewModel);
        }
        
        EventCategory eventCategory = new() { Name = addEventCategoryViewModel.Name };
        context.Categories.Add(eventCategory);
        context.SaveChanges();
        return Redirect("/eventcategory");
    }

}
