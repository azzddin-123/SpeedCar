using MongoDB.Bson;


namespace SpeedCar.Models;


public class CarIssue
{
    public ObjectId Id { get; set; }
    public ObjectId ClientId { get; set; }
    public string Problem { get; set; }
    public string BreakdownTime { get; set; }
    public string Status { get; set; }
    public double Price { get; set; }
    public string ArrivalTime { get; set; }
}