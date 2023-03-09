#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TourWith.Models;

public class Schedule
{
    [Key]
    public int ScheduleId { get; set; }
    public int DestinationId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public Destination? Destination { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}