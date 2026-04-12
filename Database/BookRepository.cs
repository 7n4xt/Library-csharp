using LibraryManagement.Models;
using MySql.Data.MySqlClient;

namespace LibraryManagement.Database;

public class BookRepository
{
	public List<Book> GetAll()
	{
		const string sql = @"
SELECT Id, Titre, Auteur, ISBN, Annee, Genre, Rayon, Etagere, Dispo, CoverPath, CreatedAt, UpdatedAt
FROM Books
ORDER BY Titre;";

		var books = new List<Book>();
		using var connection = DbConnection.GetOpenConnection();
		using var command = new MySqlCommand(sql, connection);
		using var reader = command.ExecuteReader();

		while (reader.Read())
		{
			books.Add(MapBook(reader));
		}

		return books;
	}

	public List<Book> Search(string keyword)
	{
		string normalizedKeyword = keyword?.Trim() ?? string.Empty;
		if (string.IsNullOrEmpty(normalizedKeyword))
		{
			return GetAll();
		}

		const string sql = @"
SELECT Id, Titre, Auteur, ISBN, Annee, Genre, Rayon, Etagere, Dispo, CoverPath, CreatedAt, UpdatedAt
FROM Books
WHERE Titre LIKE @keyword
   OR Auteur LIKE @keyword
   OR Genre LIKE @keyword
   OR ISBN LIKE @keyword
ORDER BY Titre;";

		var books = new List<Book>();
		using var connection = DbConnection.GetOpenConnection();
		using var command = new MySqlCommand(sql, connection);
		command.Parameters.AddWithValue("@keyword", $"%{normalizedKeyword}%");
		using var reader = command.ExecuteReader();

		while (reader.Read())
		{
			books.Add(MapBook(reader));
		}

		return books;
	}

	public int Add(Book book)
	{
		const string sql = @"
INSERT INTO Books (Titre, Auteur, ISBN, Annee, Genre, Rayon, Etagere, Dispo, CoverPath)
VALUES (@Titre, @Auteur, @ISBN, @Annee, @Genre, @Rayon, @Etagere, @Dispo, @CoverPath);
SELECT LAST_INSERT_ID();";

		using var connection = DbConnection.GetOpenConnection();
		using var command = new MySqlCommand(sql, connection);
		FillParameters(command, book);

		object? result = command.ExecuteScalar();
		return Convert.ToInt32(result);
	}

	public bool Update(Book book)
	{
		const string sql = @"
UPDATE Books
SET Titre = @Titre,
	Auteur = @Auteur,
	ISBN = @ISBN,
	Annee = @Annee,
	Genre = @Genre,
	Rayon = @Rayon,
	Etagere = @Etagere,
	Dispo = @Dispo,
	CoverPath = @CoverPath
WHERE Id = @Id;";

		using var connection = DbConnection.GetOpenConnection();
		using var command = new MySqlCommand(sql, connection);
		FillParameters(command, book);
		command.Parameters.AddWithValue("@Id", book.Id);

		int changed = command.ExecuteNonQuery();
		return changed > 0;
	}

	public bool Delete(int id)
	{
		const string sql = "DELETE FROM Books WHERE Id = @Id;";

		using var connection = DbConnection.GetOpenConnection();
		using var command = new MySqlCommand(sql, connection);
		command.Parameters.AddWithValue("@Id", id);

		int changed = command.ExecuteNonQuery();
		return changed > 0;
	}

	private static Book MapBook(MySqlDataReader reader)
	{
		int idOrdinal = reader.GetOrdinal("Id");
		int titreOrdinal = reader.GetOrdinal("Titre");
		int auteurOrdinal = reader.GetOrdinal("Auteur");
		int isbnOrdinal = reader.GetOrdinal("ISBN");
		int anneeOrdinal = reader.GetOrdinal("Annee");
		int genreOrdinal = reader.GetOrdinal("Genre");
		int rayonOrdinal = reader.GetOrdinal("Rayon");
		int etagereOrdinal = reader.GetOrdinal("Etagere");
		int dispoOrdinal = reader.GetOrdinal("Dispo");
		int coverPathOrdinal = reader.GetOrdinal("CoverPath");
		int createdAtOrdinal = reader.GetOrdinal("CreatedAt");
		int updatedAtOrdinal = reader.GetOrdinal("UpdatedAt");

		return new Book
		{
			Id = reader.GetInt32(idOrdinal),
			Titre = reader.GetString(titreOrdinal),
			Auteur = reader.GetString(auteurOrdinal),
			ISBN = reader.GetString(isbnOrdinal),
			Annee = reader.IsDBNull(anneeOrdinal) ? null : reader.GetInt32(anneeOrdinal),
			Genre = reader.IsDBNull(genreOrdinal) ? null : reader.GetString(genreOrdinal),
			Rayon = reader.IsDBNull(rayonOrdinal) ? null : reader.GetString(rayonOrdinal),
			Etagere = reader.IsDBNull(etagereOrdinal) ? null : reader.GetString(etagereOrdinal),
			Dispo = reader.GetBoolean(dispoOrdinal),
			CoverPath = reader.IsDBNull(coverPathOrdinal) ? null : reader.GetString(coverPathOrdinal),
			CreatedAt = reader.GetDateTime(createdAtOrdinal),
			UpdatedAt = reader.GetDateTime(updatedAtOrdinal)
		};
	}

	private static void FillParameters(MySqlCommand command, Book book)
	{
		command.Parameters.AddWithValue("@Titre", book.Titre);
		command.Parameters.AddWithValue("@Auteur", book.Auteur);
		command.Parameters.AddWithValue("@ISBN", book.ISBN);
		command.Parameters.AddWithValue("@Annee", (object?)book.Annee ?? DBNull.Value);
		command.Parameters.AddWithValue("@Genre", (object?)book.Genre ?? DBNull.Value);
		command.Parameters.AddWithValue("@Rayon", (object?)book.Rayon ?? DBNull.Value);
		command.Parameters.AddWithValue("@Etagere", (object?)book.Etagere ?? DBNull.Value);
		command.Parameters.AddWithValue("@Dispo", book.Dispo);
		command.Parameters.AddWithValue("@CoverPath", (object?)book.CoverPath ?? DBNull.Value);
	}
}
