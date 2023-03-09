using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TourWith.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace TourWith.Controllers;

public class DestinationController : Controller
{

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

}