using System;
using CodingEvents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace CodingEvents.Data;



public class EventDbContext : DbContext
{

    
    public DbSet<Event> Events { get; set; }
    public DbSet<EventCategory> Categories { get; set; }


    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
    {

    }
}
