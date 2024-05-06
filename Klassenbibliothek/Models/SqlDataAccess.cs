using System.Data;
using Dapper;
using MySqlConnector;

// NuGet Paket Dapper und MySqlConnector

namespace Adressen.Models;

public class SQLDataAccess
{
    public List<T> LoadData<T, U>(string sqlQuery, U parameters, string connectionString)
    {
        using (IDbConnection connection = new MySqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(sqlQuery, parameters).ToList();
            return rows;
        }
    }

    public void SaveData<T>(string sqlQuery, T parameters, string connectionString)
    {
        using (IDbConnection connection = new MySqlConnection(connectionString))
        {
            connection.Execute(sqlQuery, parameters);
        }
    }
}