👤 Client

Inscription et connexion

Envoi de messages au mécanicien

Réception des réponses du mécanicien

Consultation de l’historique du chat

Affichage d’un message d’alerte si le compte est bloqué

🛠️ Mécanicien (Admin)

Tableau de bord (Dashboard)

Liste des clients

Chat avec chaque client

Répondre aux messages des clients

Bloquer / Débloquer des clients

Les clients bloqués ne peuvent plus se connecter

🧱 Technologies utilisées

ASP.NET Core MVC

MongoDB (NoSQL)

MongoDB Compass (gestion de la base)

Bootstrap 5 (design)


Configuration MongoDB

Dans appsettings.json :
{
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "SpeedCarDB"
  }
}


!!!!!!!Packages OBLIGATOIRES!!!!!!!!!!:
dotnet add package MongoDB.Driver

▶️ Lancer le projet:
-dotnet restore
-dotnet run
-http://localhost:5000



CSS personnalisé

Session ASP.NET
