using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Event>()
            .Property(u => u.EventType)
            .HasConversion<string>()
            .HasMaxLength(100);

        modelBuilder.Entity<Event>().HasData(new Event
        {
            EventId = 1,
           EventName = "Sample webinar",
           EventType=  EventType.SEMINAR,
            StartDate = DateTime.Today, 
            EndDate = DateTime.Today,
            Location = "Chennai",
            Description = "Sample created for demo purpose",
            Price=  240.50
        });

        modelBuilder.Entity<Event>().HasData(new Event
        {
            EventId = 2,
            EventName = "Sample conference",
            EventType = EventType.CONFERENCE,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today,
           Location = "Chennai",
           Description = "Sample created for demo purpose",
           Price = 2400.50
        });

    }
}