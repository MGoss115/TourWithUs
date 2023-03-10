using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourWith.Models;
using Newtonsoft.Json.Linq;

namespace TourWith.Controllers;

public class DestinationController : Controller
{
    private readonly HttpClient _httpClient;

    private MyContext _context;

    public DestinationController(MyContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("uid");
        }
    }

    [SessionCheck]
    [HttpGet("destination")]
    public async Task<IActionResult> Index()
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
                Image = item.img
            };
            destinationList.Add(res);
        }

        return View(destinationList);
    }
}
