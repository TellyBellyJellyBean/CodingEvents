using CodingEvents.Models;
using CodingEvents.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CodingEvents.Controllers;

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
}
