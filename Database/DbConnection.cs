using System.Configuration;
using MySql.Data.MySqlClient;

namespace LibraryManagement.Database;

public static class DbConnection
{
	private const string ConnectionName = "LibraryDb";

	public static MySqlConnection GetOpenConnection()
	{
		string? connectionString = ConfigurationManager.ConnectionStrings[ConnectionName]?.ConnectionString;
		if (string.IsNullOrWhiteSpace(connectionString))
		{
			throw new InvalidOperationException(
				$"Connection string '{ConnectionName}' not found in App.config.");
		}

		var connection = new MySqlConnection(connectionString);
		connection.Open();
		return connection;
	}
}
