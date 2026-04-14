using LibraryManagement.Models;
using MySql.Data.MySqlClient;

namespace LibraryManagement.Database;

public class BorrowingRepository
{
	public bool Borrow(int bookId, string borrowerName, DateTime? dueDate = null, string? notes = null)
	{
		const string lockBookSql = @"
UPDATE Books
SET Dispo = 0
WHERE Id = @BookId AND Dispo = 1;";

		const string insertBorrowingSql = @"
INSERT INTO Borrowings (BookId, BorrowerName, DueDate, Notes)
VALUES (@BookId, @BorrowerName, @DueDate, @Notes);";

		using var connection = DbConnection.GetOpenConnection();
		using var transaction = connection.BeginTransaction();

		using (var lockBookCommand = new MySqlCommand(lockBookSql, connection, transaction))
		{
			lockBookCommand.Parameters.AddWithValue("@BookId", bookId);
			int updated = lockBookCommand.ExecuteNonQuery();
			if (updated == 0)
			{
				transaction.Rollback();
				return false;
			}
		}

		using (var insertBorrowingCommand = new MySqlCommand(insertBorrowingSql, connection, transaction))
		{
			insertBorrowingCommand.Parameters.AddWithValue("@BookId", bookId);
			insertBorrowingCommand.Parameters.AddWithValue("@BorrowerName", borrowerName);
			insertBorrowingCommand.Parameters.AddWithValue("@DueDate", (object?)dueDate ?? DBNull.Value);
			insertBorrowingCommand.Parameters.AddWithValue("@Notes", (object?)notes ?? DBNull.Value);
			insertBorrowingCommand.ExecuteNonQuery();
		}

		transaction.Commit();
		return true;
	}

	public bool Return(int bookId)
	{
		const string closeBorrowingSql = @"
UPDATE Borrowings
SET ReturnDate = CURRENT_TIMESTAMP
WHERE BookId = @BookId
  AND ReturnDate IS NULL
ORDER BY BorrowDate DESC
LIMIT 1;";

		const string unlockBookSql = @"
UPDATE Books
SET Dispo = 1
WHERE Id = @BookId;";

		using var connection = DbConnection.GetOpenConnection();
		using var transaction = connection.BeginTransaction();

		int closed;
		using (var closeBorrowingCommand = new MySqlCommand(closeBorrowingSql, connection, transaction))
		{
			closeBorrowingCommand.Parameters.AddWithValue("@BookId", bookId);
			closed = closeBorrowingCommand.ExecuteNonQuery();
			if (closed == 0)
			{
				transaction.Rollback();
				return false;
			}
		}

		using (var unlockBookCommand = new MySqlCommand(unlockBookSql, connection, transaction))
		{
			unlockBookCommand.Parameters.AddWithValue("@BookId", bookId);
			unlockBookCommand.ExecuteNonQuery();
		}

		transaction.Commit();
		return true;
	}
}
