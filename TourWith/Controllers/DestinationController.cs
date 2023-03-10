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


    // [HttpGet("destination")]
    // public async Task<IActionResult> Index()
    // {
    //     var authKey = Convert.ToBase64String(Encoding.UTF8.GetBytes("a63fdddd565ec353f2451ac96b54cb46:f1c8f86af1c3050149f1e4bef5b4e5a3"));

    //     var request = new HttpRequestMessage(HttpMethod.Get, "https://api.roadgoat.com/api/v2/destinations/new-york-ny-usa");
    //     request.Headers.Add("Authorization", $"Basic {authKey}");

    //     var response = await _httpClient.SendAsync(request);

    //     if (!response.IsSuccessStatusCode)
    //     {
    //         return StatusCode((int)response.StatusCode);
    //     }

    //     var content = await response.Content.ReadAsStringAsync();
    //     var result = JsonConvert.DeserializeObject<dynamic>(content);
    //     Console.WriteLine($"here is the {result}");


    //     return View();

    // }

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
