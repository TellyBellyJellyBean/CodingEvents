using Microsoft.AspNetCore.Mvc;
using CodingEvents.ViewModels;
namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        // GET: /<controller>/
           
        public IActionResult Index()
        {
            
            List<Event> events = new List<Event>(EventData.GetAll());
            return View(events);
    
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
        Event newEvent = new Event
        {
            Name = addEventViewModel.Name,
            Description = addEventViewModel.Description
        };

        EventData.Add(newEvent);

        return Redirect("/Events");
        }

        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }

            return Redirect("/Events");
        }
    }

}