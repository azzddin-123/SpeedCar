using MongoDB.Driver;
using SpeedCar.Models;
using SpeedCar.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSession();


var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseSession();


// Create default mechanic
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MongoDbService>();
    if (!db.Users.Find(u => u.Role == "Mechanic").Any())
    {
        db.Users.InsertOne(new User
        {
            Name = "Admin Mechanic",
            Email = "admin@speedcar.com",
            Password = "admin123",
            Role = "Mechanic",
            IsBlocked = false
        });
    }
}


app.MapControllerRoute(
name: "default",
pattern: "{controller=Auth}/{action=Login}/{id?}");


app.Run();