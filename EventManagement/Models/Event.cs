using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventManagement.Models;

public class Event
{
    public Event()
    {
    }
    public Event(int eventId, string eventName, EventType eventType, DateTime startDate, DateTime endDate, string location, string description, double price)
    {
        EventId = eventId;
        EventName = eventName;
        EventType = eventType;
        StartDate = startDate;
        EndDate = endDate;
        Location = location;
        Description = description;
        Price = price;
    }

    [Key] [Required] [NotNull] public int EventId { get; set; }

    [Required] [NotNull] public string EventName { get; set; }

    [Required] [NotNull] [JsonConverter(typeof(StringEnumConverter))] public EventType EventType { get; set; }
    
    [Required] [NotNull] public DateTime StartDate { get; set; }

    [Required] [NotNull] public DateTime EndDate { get; set; }

    [Required] [NotNull] public string Location { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }
    
}