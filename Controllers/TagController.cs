using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingEvents.Controllers
{
    public class TagController : Controller
    {
        private EventDbContext context;
        public TagController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        //GET: /<controller>/
        public IActionResult Index()
        {
            List<Tag> tags = context.Tags.ToList();
            return View(tags);
        }
        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View(tag);
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            if(ModelState.IsValid)
            {
                context.Tags.Add(tag);
                context.SaveChanges();
                return Redirect("/Tag/");
            }
            return View("Add", tag);
        }

        // [HttpGet] //Maybe not?
        [HttpGet("/Tag/AddEvents/{id}")]
        public IActionResult AddEvents(int id)
        {
            Event? theEvent = context.Events.Find(id);
            List<Tag> possibleTags = context.Tags.ToList();

            AddEventTagViewModel viewModel = new AddEventTagViewModel(theEvent, possibleTags);
            return View(viewModel);
        }
        //POST
        [HttpPost]
        public IActionResult AddEvents(AddEventTagViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int eventId = viewModel.EventId;
                int tagId = viewModel.TagId;

                Event theEvent = context.Events.Include(e => e.Tags).Where(e => e.Id == eventId).First();
                Tag theTag = context.Tags.Where(t => t.Id == tagId).First();

                theEvent.Tags.Add(theTag);
                
                context.SaveChanges();

                return Redirect("/Events/Detail/" + eventId);

            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Tag theTag = context.Tags.Include(e => e.Events).Where(t => t.Id == id).First();
            return View(theTag);
        }
    }
}