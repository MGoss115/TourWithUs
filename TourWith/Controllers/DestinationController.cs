using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using TourWith.Models;
using Microsoft.EntityFrameworkCore;

namespace TourWith.Controllers;

public class DestinationController : Controller
{
    private readonly HttpClient _httpClient;

    private MyContext _context;

    public DestinationController(MyContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient();
    }
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("uid");
        }
    }
    public List<Destination> destinationList = new List<Destination>();

    [SessionCheck]
    [HttpGet("destinations")]
    public IActionResult Dashboard()
    {
        List<Destination> allTrips = _context.Destinations
        .Include(d => d.User)
        .Include(d => d.Book)
        .ToList();
        return View(allTrips);
    }

    [HttpGet("adddestination")]
    public async Task<IActionResult> CreateDestination()
    {
        var url = "https://persian-blue-hen-slip.cyclic.app/bookhotels";
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(content);
        Console.WriteLine($" here {data}");

        var destinationList = new List<Destination>();
        foreach (var item in data)
        {
            var res = new Destination
            {
                Location = item.place,
                Image = item.img,
                Budget = item.cost,
                Comment = item.title,
            };
            destinationList.Add(res);
        }
        return View(destinationList);
    }

    [HttpPost("adddestination")]
    public async Task<IActionResult> AddDestination(Destination newDestination)
    {

        newDestination.UserId = (int)uid;
        if (ModelState.IsValid)
        {
            _context.Destinations.Add(newDestination);
            await _context.SaveChangesAsync();
            return Redirect("/adddestination");
        }
        else
        {
            return View("CreateDestination");
        }
    }
}

