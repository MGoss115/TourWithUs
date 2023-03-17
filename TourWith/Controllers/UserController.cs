using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TourWith.Models;

namespace TourWith.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("uid") != null)
        {
            return RedirectToAction("Success");
        }
        else
        {
            return View();
        }
    }

    [HttpPost("register")]
    public IActionResult Register(User newUser)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        else
        {
            PasswordHasher<User> hash = new PasswordHasher<User>();
            newUser.Password = hash.HashPassword(newUser, newUser.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("uid", newUser.UserId);
            HttpContext.Session.SetString("name", newUser.FirstName);
            return Redirect("/adddestination");
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUser getUser)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        else
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == getUser.LoginEmail);

            if (userInDb == null)
            {

                ModelState.AddModelError("LoginEmail", "Invalid Email");
                return View("Index");
            }
            else
            {
                PasswordHasher<LoginUser> hash = new PasswordHasher<LoginUser>();
                var result = hash.VerifyHashedPassword(getUser, userInDb.Password, getUser.LoginPassword);

                if (result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Password");
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.SetInt32("uid", userInDb.UserId);
                    HttpContext.Session.SetString("name", userInDb.FirstName);
                    return Redirect("/adddestination");
                }
            }
        }
    }

    [SessionCheck]
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? uid = context.HttpContext.Session.GetInt32("uid");

        if (uid == null)
        {
            context.Result = new RedirectToActionResult("Index", "User", null);
        }
    }
}

