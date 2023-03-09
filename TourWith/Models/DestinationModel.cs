#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TourWith.Models;

public class Destination
{
    [Key]
    public int DestinationId { get; set; }
    // add more attributes here
    public string Location { get; set; }
    [FutureDate]
    public DateTime Date { get; set; }
    public string Image { get; set; }
    public string Budget { get; set; }
    public string Safety { get; set; }
    public List<Schedule> Book { get; set; } = new List<Schedule>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime dt;
        if (value is DateTime)
        {
            dt = (DateTime)value;
        }
        else
        {
            return new ValidationResult("Invalid datetime");
        }
        if (dt < DateTime.Now)
        {
            return new ValidationResult("Date must be in the future");
        }
        return ValidationResult.Success;
    }
}