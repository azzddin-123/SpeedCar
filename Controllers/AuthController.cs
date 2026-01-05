using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SpeedCar.Models;
using SpeedCar.Services;


namespace SpeedCar.Controllers;


public class AuthController : Controller
{
    private readonly MongoDbService _db;
    public AuthController(MongoDbService db) { _db = db; }


    public IActionResult Login() => View();


    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _db.Users
            .Find(u => u.Email == email && u.Password == password)
            .FirstOrDefault();

        if (user == null)
        {
            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View();
        }

        // 🔒 UTILISATEUR BLOQUÉ
        if (user.IsBlocked)
        {
            ViewBag.Error = "🚫 Ce compte est bloqué temporairement. veuillez ";
            return View();
        }

        // ✅ Connexion autorisée
        HttpContext.Session.SetString("UserId", user.Id.ToString());
        HttpContext.Session.SetString("Role", user.Role);

        if (user.Role == "Mechanic")
            return RedirectToAction("Dashboard", "Mechanic");
        else
            return RedirectToAction("Index", "Client");
    }


    public IActionResult Register() => View();


    [HttpPost]
    public IActionResult Register(User user)
    {
        user.Role = "Client";
        user.IsBlocked = false;
        _db.Users.InsertOne(user);
        return RedirectToAction("Login");
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }
}