using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TourWith.Models;

namespace TourWith.Controllers;

public class DestinationController : Controller
{
    HttpClientHandler _clientHandler = new HttpClientHandler();

    private MyContext _context;

    public DestinationController(MyContext context)
    {
        _context = context;
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
    public IActionResult Result()
    {
        return View();
    }

}