using Klassenbibliothek.Models;
using Microsoft.Extensions.Configuration;
using System;

// MS Json NuGet

namespace Adressenbuch;

public static class Funktionen
{
    public static string GetConnectionString(string conStrName = "Default")
    {
        string output = "";
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory
                .GetCurrentDirectory())
            .AddJsonFile("appsettings.json"); // Hier wird die Datei geladen

        var config = builder.Build(); // Hier wird die Konfiguration erstellt
        output = config.GetConnectionString(conStrName);

        return output;
    }
    public static Person CreatePerson()
    {
        Person p = new Person();

        Console.Write("Vorname: ");
        p.vorname = Console.ReadLine();

        Console.Write("Nachname: ");
        p.nachname = Console.ReadLine();

        Console.Write("Strasse und Hausnummer: ");
        p.strasse = Console.ReadLine();

        Console.Write("Postleitzahl: ");
        p.plz = Console.ReadLine();

        Console.Write("Stadt: ");
        p.ort = Console.ReadLine();

        return p;
    }
}