using EventManagement.Data;
using EventManagement.Models;
using EventManagement.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers;

[Route("api/events")]
[Authorize]
public class EventController : Controller
{
    private readonly AppDbContext _db;
    private readonly ResponseDto _responseDto;

    public EventController(AppDbContext db)
    {
        _db = db;
        _responseDto = new ResponseDto();
    }

    [HttpGet]
    public object get()
    {
        try
        {
            _responseDto.response = _db.Events.ToList();
            _responseDto.isSuccess = true;
            return _responseDto;
        }
        catch (Exception e)
        {
            _responseDto.message = e.Message;
            _responseDto.isSuccess = false;
        }

        return _responseDto;
    }

    [HttpGet("{id}")]
    public object getById(int id)
    {
        try
        {
            _responseDto.response = _db.Events.First(e => e.EventId == id);
            _responseDto.isSuccess = true;
            return _responseDto;
        }
        catch (Exception e)
        {
            _responseDto.message = e.Message;
            _responseDto.isSuccess = false;
        }

        return _responseDto;
    }

    [HttpGet("event-type")]
    public object getByEventType([FromQuery(Name = "eventtype")] string eventType)
    {
        try
        {
            _responseDto.response = _db.Events.Where(e => e.EventType == (EventType) Enum.Parse(typeof(EventType), eventType)).ToList();
            _responseDto.isSuccess = true;
            return _responseDto;
        }
        catch (Exception e)
        {
            _responseDto.message = e.Message;
            _responseDto.isSuccess = false;
        }

        return _responseDto;
    }

    [HttpPost]
    [Authorize(Roles="admin")]
    public object Post([FromBody] EventDto eventDto)
    {
        EventType eventType = (EventType) Enum.Parse(typeof(EventType), eventDto.EventType);
        Event events = new Event(eventDto.EventId, eventDto.EventName, eventType, eventDto.StartDate,
            eventDto.EndDate, eventDto.Description, eventDto.Location, eventDto.Price);
        _responseDto.response = events;
        _db.Events.Add(events);
        _db.SaveChanges();
        return _responseDto;
    }
    
    [HttpPut]
    [Authorize(Roles="admin")]
    public object Put([FromBody] EventDto eventDto)
    {
        EventType eventType = (EventType) Enum.Parse(typeof(EventType), eventDto.EventType);
        Event events = new Event(eventDto.EventId, eventDto.EventName, eventType, eventDto.StartDate,
            eventDto.EndDate, eventDto.Description, eventDto.Location, eventDto.Price);
        _responseDto.response = events;
        _db.Events.Update(events);
        _db.SaveChanges();
        return _responseDto;
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles="admin")]
    public object Put(int id)
    {
        try
        {
            Event events = _db.Events.First(e => e.EventId == id);
            _db.Events.Remove(events);
            _db.SaveChanges();
            return _responseDto;
        }
        catch (Exception e)
        {
            _responseDto.isSuccess = false;
            _responseDto.message = e.Message;
        }

        return _responseDto;
    }

}