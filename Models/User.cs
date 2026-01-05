using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace SpeedCar.Models;


public class User
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsBlocked { get; set; }
}