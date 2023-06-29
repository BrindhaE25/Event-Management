using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventManagement.Models.Dtos;

public class EventDto
{
   public int EventId { get; set; }
   public string EventName { get; set; }
   
   // [JsonConverter(typeof(StringEnumConverter))]
   public string EventType { get; set; }
   public DateTime StartDate { get; set; }
   public DateTime EndDate { get; set; }
   public string Location { get; set; }
   
   public string Description { get; set; }

   public double Price { get; set; }
}