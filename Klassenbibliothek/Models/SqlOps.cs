using Adressen.Models;
using Klassenbibliothek.Models;
using System.ComponentModel.DataAnnotations;

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

    public List<Mail> GetMails(int id)
    {
        string sql = "SELECT mail.id, mail.mail FROM mail JOIN pers_mail ON mail.id = pers_mail.mail_id WHERE pers_mail.pers_id = @id";
        return _db.LoadData<Mail, dynamic>(sql, new { id }, _connectionString);
    }

    public int checkMail(string mailadr)
    {
        string sql = "SELECT id FROM mail WHERE mail = @mailadr";
        return _db.LoadData<int, dynamic>(sql, new { mailadr }, _connectionString).FirstOrDefault();
    }

    public void bindMail(int pid, int mid)
    {
        string sql = "INSERT INTO pers_mail (pers_id, mail_id) VALUES (@pid, @mid)";
        _db.SaveData(sql, new { pid, mid }, _connectionString);
    }

    public void addMail(Person p, string mailadr)
    {
        int mail_id = checkMail(mailadr);

        if (mail_id > 0)
        {
            bindMail(p.id, mail_id);
        }
        else
        {
            string sql = "INSERT INTO mail (mail) VALUES (@mailadr)";
            _db.SaveData(sql, new { mailadr }, _connectionString);

            mail_id = checkMail(mailadr);
            bindMail(p.id, mail_id);
        }
    }

    public void deleteMail(int pid, int mid)
    {
        string sql = "DELETE FROM pers_mail WHERE pers_id = @pid AND mail_id = @mid";
        _db.SaveData(sql, new { pid, mid }, _connectionString);

        sql = "SELECT count(*) FROM pers_mail WHERE mail_id = @mid";
        int count = _db.LoadData<int, dynamic>(sql, new { mid } , _connectionString).FirstOrDefault();

        if (count == 0)
        {
            sql = "DELETE FROM mail WHERE id = @mid";
            _db.SaveData(sql, new { mid }, _connectionString);
        }
    }

    public List<Tel> GetTelNr(int id)
    {
        string sql = "SELECT telefon.id, telefon.nummer FROM telefon JOIN pers_tel ON telefon.id = pers_tel.tel_id WHERE pers_tel.pers_id = @id";
        return _db.LoadData<Tel, dynamic>(sql, new { id }, _connectionString);
    }

    public int checkTelNr(string telnr)
    {
        string sql = "SELECT id FROM telefon WHERE nummer = @telnr";
        return _db.LoadData<int, dynamic>(sql, new { telnr }, _connectionString).FirstOrDefault();
    }

    public void bindTelNr(int pid, int tid)
    {
        string sql = "INSERT INTO pers_tel (pers_id, tel_id) VALUES (@pid, @tid)";
        _db.SaveData(sql, new { pid, tid }, _connectionString);
    }

    public void addTelNr(Person p, string telnr)
    {
        int tel_id = checkTelNr(telnr);

        if (tel_id > 0)
        {
            bindTelNr(p.id, tel_id);
        }
        else
        {
            string sql = "INSERT INTO telefon (nummer) VALUES (@telnr)";
            _db.SaveData(sql, new { telnr }, _connectionString);

            tel_id = checkTelNr(telnr);
            bindTelNr(p.id, tel_id);
        }
    }

    public void deleteTelNr(int pid, int tid)
    {
        string sql = "DELETE FROM pers_tel WHERE pers_id = @pid AND tel_id = @tid";
        _db.SaveData(sql, new { pid, tid }, _connectionString);

        sql = "SELECT count(*) FROM pers_tel WHERE tel_id = @tid";
        int count = _db.LoadData<int, dynamic>(sql, new { tid }, _connectionString).FirstOrDefault();

        if (count == 0)
        {
            sql = "DELETE FROM telefon WHERE id = @tid";
            _db.SaveData(sql, new { tid }, _connectionString);
        }
    }
}