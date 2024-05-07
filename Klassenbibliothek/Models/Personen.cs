using System.ComponentModel.DataAnnotations;

namespace Klassenbibliothek.Models;

public class Person
{
    public int id { get; set; }
    public string vorname { get; set; }
    public string nachname { get; set; }
    public string strasse { get; set; }
    public string hausnr { get; set; }
    public string plz { get; set; }
    public string ort { get; set; }

    public List<Mail> mail { get; set; }
    public List<Tel> nummer { get; set; }
}