#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TourWith.Models;

public class PokemonViewModel
{
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}
