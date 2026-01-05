using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SpeedCar.Models;
using SpeedCar.Services;


namespace SpeedCar.Controllers;


public class ClientController : Controller
{
    private readonly MongoDbService _db;
    public ClientController(MongoDbService db) { _db = db; }


    public IActionResult Index()
    {
        var clientId = ObjectId.Parse(HttpContext.Session.GetString("UserId"));
        var mechanicId = _db.Users.Find(u => u.Role == "Mechanic").First().Id;

        // récupérer toute la conversation
        var messages = _db.Messages.Find(m =>
            (m.SenderId == clientId && m.ReceiverId == mechanicId) ||
            (m.SenderId == mechanicId && m.ReceiverId == clientId)
        ).SortBy(m => m.Date).ToList();

        return View(messages);
    }



    [HttpPost]
    public IActionResult SendMessage(string content)
    {
        var clientId = ObjectId.Parse(HttpContext.Session.GetString("UserId"));
        var mechanic = _db.Users.Find(u => u.Role == "Mechanic").First();

        _db.Messages.InsertOne(new Message
        {
            SenderId = clientId,
            ReceiverId = mechanic.Id,
            Content = content,
            Date = DateTime.Now
        });

        return RedirectToAction("Index");
    }
}