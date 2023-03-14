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
        .Include(u => u.User)
        .Include(u => u.Book)
        .Where(u => u.UserId == (int)uid)
        .ToList();
        return View(allTrips);
    }

    [SessionCheck]
    [HttpGet("destinations/{destinationId}")]
    public IActionResult Details(int destinationId)
    {
        Destination? destination = _context.Destinations
        .FirstOrDefault(d => d.DestinationId == destinationId);
        if (destination == null)
        {
            return RedirectToAction("All");
        }

        return View(destination);
    }

    [SessionCheck]
    [HttpGet("adddestination")]
    public async Task<IActionResult> CreateDestination()
    {
        var url = "https://persian-blue-hen-slip.cyclic.app/bookhotels";
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(content);
        // Console.WriteLine($" here {data}");

        var destinationList = new List<Destination>();
        foreach (var item in data)
        {
            var res = new Destination
            {
                Location = item.place,
                Image = item.img,
                Budget = item.cost,
                Comment = item.title,
                Days = item.days,
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

    [SessionCheck]
    [HttpPost("destinations/{destinatonId}/book")]
    public IActionResult BookedNow(int destinationId)
    {
        Schedule? alreadyBooked = _context.Scheduled
        .FirstOrDefault(b => b.UserId == (int)uid && b.DestinationId == destinationId);

        if (alreadyBooked != null)
        {
            _context.Scheduled.Remove(alreadyBooked);
        }
        else
        {
            Schedule newBook = new Schedule()
            {
                DestinationId = destinationId,
                UserId = (int)uid
            };
            _context.Scheduled.Add(newBook);
        }
        _context.SaveChanges();
        return Redirect("/destinations");
    }

    [SessionCheck]
    [HttpPost("destinations/{destinationId}/delete")]
    public IActionResult DeleteDestination(int destinationId)
    {
        Destination destination = _context.Destinations
        .SingleOrDefault(d => d.DestinationId == destinationId);

        if (destination != null && destination.UserId == (int)uid)
        {
            _context.Destinations.Remove(destination);
            _context.SaveChanges();
        }
        return Redirect("/destinations");
    }

}

