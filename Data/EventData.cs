namespace CodingEvents;

public class EventData
{
    static private Dictionary<int, Event> Events = new Dictionary<int, Event>();
    //Fetch all events
    public static IEnumerable<Event> GetAll()
    {
        return Events.Values;
    }

    //Add a new event to dictionary
    public static void Add(Event newEvent)
    {
        Events.Add(newEvent.Id, newEvent);
    }
    //Remove event from dictionary
    public static void Remove(int id)
    {
        Events.Remove(id);
    }
    //fetch specific event
    public static Event GetById(int id)
    {
        return Events[id];
    }
}
