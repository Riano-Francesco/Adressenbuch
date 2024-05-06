using Adressenbuch;
using Klassenbibliothek.Models;

Adresse adr = new Adresse();
adr.LoadFromFile("Adressen.txt");

bool nextPerson = false;

do
{
    Console.Clear();
    Console.WriteLine("1. Person hinzufügen");
    Console.WriteLine("2. Person anzeigen");
    Console.WriteLine("3. Person löschen");
    Console.WriteLine("4. Person bearbeiten");
    Console.WriteLine("5. Programm beenden");

    string auswahl;
    Console.Write("Bitte wählen Sie aus, was Sie tun möchten -> ");
    auswahl = Console.ReadLine();


    switch (auswahl)
    {
        case "1":
            do
            {
                adr.AddPerson(Funktionen.CreatePerson());
                Console.Write("Noch eine Person hinzufuegen? (J/N) -> ");

                if (Console.ReadLine().ToUpper() == "J")
                {
                    nextPerson = true;
                }
                else
                {
                    nextPerson = false;
                }

            } while (nextPerson);

            adr.SaveToFile("Adressen.txt");
            break;

        case "2":
            adr.ListPeople();
            adr.ShowPeople();
            Console.WriteLine("Weiter mit Enter");
            Console.ReadLine();
            break;

        case "3":
            adr.ListPeople();
            adr.DeletePerson();
            adr.SaveToFile("Adressen.txt");
            break;

        case "4":
            adr.ListPeople();
            adr.ModifyPeople();
            adr.SaveToFile("Adressen.txt");
            break;

        case "5":
            return;

        default:
            Console.WriteLine("Ungültige Eingabe - Bitte Wiederholen!");
            Console.WriteLine("Weiter mit Enter");
            Console.ReadLine();
            break;
    }
} while (true);


