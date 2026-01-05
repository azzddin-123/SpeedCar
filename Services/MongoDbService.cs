using MongoDB.Driver;
using SpeedCar.Models;


namespace SpeedCar.Services;


public class MongoDbService
{
    private readonly IMongoDatabase _db;


    public MongoDbService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        _db = client.GetDatabase(config["MongoDB:DatabaseName"]);
    }


    public IMongoCollection<User> Users => _db.GetCollection<User>("Users");
    public IMongoCollection<CarIssue> Issues => _db.GetCollection<CarIssue>("Issues");
    public IMongoCollection<Message> Messages => _db.GetCollection<Message>("Messages");
}