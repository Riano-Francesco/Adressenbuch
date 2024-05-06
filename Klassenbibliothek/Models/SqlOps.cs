using Adressen.Models;
using Klassenbibliothek.Models;

public class SQLOps
{
    private readonly string _connectionString;
    private SQLDataAccess _db = new SQLDataAccess();

    public SQLOps(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Person> GetPeople()
    {
        string sql = "SELECT id, vorname, nachname, strasse, hausnr, plz, ort FROM personen";
        return _db.LoadData<Person, dynamic>(sql, new { }, _connectionString);
    }

    public Person GetPerson(int id)
    {
        string sql = "SELECT id, vorname, nachname, strasse, hausnr, plz, ort FROM personen WHERE id = @id";
        Person output = _db.LoadData<Person, dynamic>(sql, new { id }, _connectionString).FirstOrDefault();

        if (output == null)
        {
            return new Person();
        }

        return output;
    }

    public void CreateContact(Person p)
    {
        string sql = "INSERT INTO personen (vorname, nachname, strasse, hausnr, plz, ort) VALUES (@vorname, @nachname, @strasse, @hausnr, @plz, @ort)";
        _db.SaveData(sql, p, _connectionString);
    }

    public bool checkID(Person p) // count(*) = wie viele datenbankeinträge sind vorhanden
    {
        string sql = "SELECT count(*) FROM personen WHERE id = @id";
        int count = _db.LoadData<int, dynamic>(sql, p, _connectionString).FirstOrDefault();
        return count > 0;
    }

    public void UpdatePerson(Person p)
    {
        string sql = "UPDATE personen SET vorname = @vorname, nachname = @nachname, strasse = @strasse, hausnr = @hausnr, plz = @plz, ort = @ort WHERE id = @id";
        _db.SaveData(sql, p, _connectionString);
    }
}