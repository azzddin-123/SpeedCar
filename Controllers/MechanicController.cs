using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SpeedCar.Models;
using SpeedCar.Services;


namespace SpeedCar.Controllers;


public class MechanicController : Controller
{
    private readonly MongoDbService _db;
    public MechanicController(MongoDbService db) { _db = db; }


    public IActionResult Dashboard()
    {
        var clients = _db.Users.Find(u => u.Role == "Client").ToList();
        return View(clients);
    }


    public IActionResult Chat(string id)
    {
        var mechanicId = _db.Users.Find(u => u.Role == "Mechanic").First().Id;
        var clientId = ObjectId.Parse(id);


        var messages = _db.Messages.Find(m =>
        (m.SenderId == clientId && m.ReceiverId == mechanicId) ||
        (m.SenderId == mechanicId && m.ReceiverId == clientId)
        ).SortBy(m => m.Date).ToList();


        ViewBag.ClientId = id;
        return View(messages);
    }


    [HttpPost]
    public IActionResult Reply(string clientId, string content)
    {
        var mechanic = _db.Users.Find(u => u.Role == "Mechanic").First();


        _db.Messages.InsertOne(new Message
        {
            SenderId = mechanic.Id,
            ReceiverId = ObjectId.Parse(clientId),
            Content = content,
            Date = DateTime.Now
        });
        return RedirectToAction("Chat", new { id = clientId });
    }

    public IActionResult Block(string id)
    {
        var clientId = ObjectId.Parse(id);

        var update = Builders<User>.Update.Set(u => u.IsBlocked, true);
        _db.Users.UpdateOne(u => u.Id == clientId, update);

        return RedirectToAction("Dashboard");
    }

    public IActionResult Unblock(string id)
    {
        var clientId = ObjectId.Parse(id);

        var update = Builders<User>.Update.Set(u => u.IsBlocked, false);
        _db.Users.UpdateOne(u => u.Id == clientId, update);

        return RedirectToAction("Dashboard");
    }
}