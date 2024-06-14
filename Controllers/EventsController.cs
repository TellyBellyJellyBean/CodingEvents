using Microsoft.AspNetCore.Mvc;
using CodingEvents.ViewModels;
using CodingEvents.Models;
using CodingEvents.Data;
using Microsoft.EntityFrameworkCore;
namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;

        public EventsController (EventDbContext dbcontext)
        {
            context = dbcontext;
        }
        // GET: /<controller>/
           
        public IActionResult Index()
        {
            
            List<Event> events = context.Events.Include(e => e.Category).ToList();
            return View(events);
    
        }

        public IActionResult Detail(int id)
        {
            Event theEvent = context.Events.Include(e => e.Category).Single(e => e.Id == id);
            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<EventCategory> categories = context.Categories.ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(categories);

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if(ModelState.IsValid)
            {
                EventCategory theCategory = context.Categories.Find(addEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = theCategory
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.events = context.Events.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                Event? theEvent = context.Events.Find(eventId);
                context.Events.Remove(theEvent);
            }
            context.SaveChanges();

            return Redirect("/Events");
        }
    }

}