using MongoDB.Bson;


namespace SpeedCar.Models;


public class Message
{
    public ObjectId Id { get; set; }
    public ObjectId SenderId { get; set; }
    public ObjectId ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}