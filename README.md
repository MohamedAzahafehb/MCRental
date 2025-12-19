# MCRental
## ASP.NET (webapplicatie)

Voor het opstarten en testen van de webapplicatie is er verijst:
- dotnet versie 9
- Visual Studio
- Microsoft SQL Server

Eerst moet deze github repo gecloned worden en geopend worden met Visual Studio. Build vervolgens de hele solution. Zodra dit gebeurt is, klikt u met de rechtermuisklik op het project `MCRental_Models` en dan kiest u voor `Manage user secrets`.
Dan wordt er een bestand `secrets.json` geopend waar u uw ConnectionString kan zetten zodat u connectie kan maken met de database. De json file moet het volgende formaat hebben:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=[server]; Database=[Database naam]; User Id=[gebruikersnaam]; password=[wachtwoord]; MultipleActiveResultSets=true;TrustServerCertificate=True;"
    }
}
```

Indien u de applicatie eerst wil testen op een locale databank. Gelieve de ConnectionString dan aan te passen en de Migrations uit te voeren:
- In Visual Studio klikt u bovenaan op de tab `Tools` > `NuGet Package Manager` > `Package Manager Console`
- Dan wordt Console geopend. Selecteer als `Default project`: `MCRental_Models` en voer het volgende commando uit:
    ```bash
    PM> update-database
    ```
Nu zijn de Migraties succesvol toegepast en kunt u de web applicatie runnen, de seeding gebeurt automatisch.
## WPF
Documentatie
Beschrijving
De bedoeling van dit project is het ontwikkelen van een desktopapplicatie in .NET (WPF) voor het beheer van een autoverhuurbedrijf.
De huidige website van MCRentalCars is erg beperkt: klanten kunnen niet online reserveren of inloggen, en er is geen overzicht van de beschikbare auto’s. Volgens hun Facebookpagina verloopt alle communicatie rondom reserveringen momenteel via WhatsApp.
Met de nieuwe desktopapplicatie wordt het voor dit bedrijf een stuk makkelijker om klanten aan te trekken, de reserveringen te beheren en het verhuurproces te vergemakkelijken.
Start van de applicatie
userSecrets klaar te zetten zodat de migraties en seeding in uw locale database gezet kunnen worden 
Zorg er eerst voor het commando update-database uit te voeren

Wanneer de applicatie gestart wordt, komt u terecht in de Home-pagina. Hier kan de bezoeker direct de data invullen waarin hij een auto wenst te huren en bij welk filiaal. Dan worden alle beschikbare auto’s voor deze data weergegeven. Maar vooraleer er een reservatie gemaakt kan worden, moet er eerst ingelogd worden of de bezoeker moet zich registreren. Dit kan ook in de Homepagina.
Eenmaal ingelogd, als de gebruiker een klant is, kan hij:
-	Auto’s reserveren
-	Zijn reservaties bekijken
-	Zijn profiel bekijken en aanpassen
Als de gebruiker een beheerder is, kan hij:
-	Auto’s aanpassen, toevoegen en deactiveren
-	Reservaties koppelen aan een andere auto
-	Filialen aanpassen, toevoegen
-	Rollen toekennen aan gebruikers
Licentie-overeenkomsten
Welke externe bronnen heb ik gebruikt in mijn project:
-	StackOverflow: https://stackoverflow.com
-	Microsoft Learn: https://learn.microsoft.com/nl-be/
-	PostSharp Blog: https://blog.postsharp.net/inotifypropertychanged
-	Europcar: https://www.europcar.be/nl-be
-	Repo Agenda: https://github.com/WaldoHeudens/Agenda
Geleende code
Models/Stad, Reservatie, MCRentalDBContext, Gebruiker, Filiaal, Auto: uit repo,
Seeding Stad: https://www.youtube.com/watch?v=ufHlJLPK5CA&t=614s + POST request van Microsoft Learn
Seeding Gebruiker, Filiaal, Auto: AI
Het gebruiken van UserSecrets: uit repo

MCRental_Client
Uit repo: App, Resource, LogIn, Registreer, datagrids, rol wijzigen van gebruiker, tabellen updaten
Uit AI: Profiel, sorteermethode, filtermethode
Eenmaan dit geleerd te hebben en gebruikt te hebben met behulp van externe bronnen, heb ik de schermen, paginas, gemeenschappelijke methodes gekopieerd en aangepast naar wat het moest worden
