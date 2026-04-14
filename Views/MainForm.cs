using System.ComponentModel;
using LibraryManagement.Database;
using LibraryManagement.Models;
using MySql.Data.MySqlClient;

namespace LibraryManagement.Views;

public partial class MainForm : Form
{
	private readonly BookRepository bookRepository = new();
	private readonly BorrowingRepository borrowingRepository = new();

	public MainForm()
	{
		InitializeComponent();
		Load += MainForm_Load;
	}

	private void MainForm_Load(object? sender, EventArgs e)
	{
		LoadBooks();
	}

	private void searchButton_Click(object? sender, EventArgs e)
	{
		LoadBooks(searchTextBox.Text);
	}

	private void addButton_Click(object? sender, EventArgs e)
	{
		using var addForm = new AddEditBookForm();
		if (addForm.ShowDialog(this) != DialogResult.OK)
		{
			return;
		}

		try
		{
			int newId = bookRepository.Add(addForm.BookData);
			LoadBooks(searchTextBox.Text);
			statusLabel.Text = $"Book #{newId} added";
		}
		catch (Exception ex)
		{
			ShowOperationError("add the book", ex);
		}
	}

	private void deleteButton_Click(object? sender, EventArgs e)
	{
		if (TryGetSelectedBook(out Book? selectedBook) == false)
		{
			return;
		}

		DialogResult confirmResult = MessageBox.Show(
			this,
			$"Delete \"{selectedBook.Titre}\" by {selectedBook.Auteur}?",
			"Confirm Delete",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Warning,
			MessageBoxDefaultButton.Button2);

		if (confirmResult != DialogResult.Yes)
		{
			return;
		}

		try
		{
			bool deleted = bookRepository.Delete(selectedBook.Id);
			LoadBooks(searchTextBox.Text);
			statusLabel.Text = deleted
				? $"Book #{selectedBook.Id} deleted"
				: $"Book #{selectedBook.Id} was not deleted";
		}
		catch (Exception ex)
		{
			ShowOperationError("delete the book", ex);
		}
	}

	private void borrowButton_Click(object? sender, EventArgs e)
	{
		if (TryGetSelectedBook(out Book? selectedBook) == false)
		{
			return;
		}

		if (!selectedBook!.Dispo)
		{
			MessageBox.Show(
				this,
				"This book is already borrowed.",
				"Library Management",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			return;
		}

		DialogResult confirm = MessageBox.Show(
			this,
			$"Mark \"{selectedBook.Titre}\" as borrowed by {Environment.UserName}?",
			"Confirm Borrow",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question,
			MessageBoxDefaultButton.Button2);

		if (confirm != DialogResult.Yes)
		{
			return;
		}

		try
		{
			bool borrowed = borrowingRepository.Borrow(selectedBook.Id, Environment.UserName, DateTime.Now.AddDays(14));
			LoadBooks(searchTextBox.Text);
			statusLabel.Text = borrowed
				? $"Book #{selectedBook.Id} borrowed"
				: "Borrowing was not saved";
		}
		catch (Exception ex)
		{
			ShowOperationError("borrow the book", ex);
		}
	}

	private void returnButton_Click(object? sender, EventArgs e)
	{
		if (TryGetSelectedBook(out Book? selectedBook) == false)
		{
			return;
		}

		if (selectedBook!.Dispo)
		{
			MessageBox.Show(
				this,
				"This book is already available.",
				"Library Management",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			return;
		}

		DialogResult confirm = MessageBox.Show(
			this,
			$"Mark \"{selectedBook.Titre}\" as returned?",
			"Confirm Return",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question,
			MessageBoxDefaultButton.Button2);

		if (confirm != DialogResult.Yes)
		{
			return;
		}

		try
		{
			bool returned = borrowingRepository.Return(selectedBook.Id);
			LoadBooks(searchTextBox.Text);
			statusLabel.Text = returned
				? $"Book #{selectedBook.Id} returned"
				: "No active borrowing found";
		}
		catch (Exception ex)
		{
			ShowOperationError("return the book", ex);
		}
	}

	private void booksGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
		{
			return;
		}

		if (booksGrid.Rows[e.RowIndex].DataBoundItem is not Book selectedBook)
		{
			return;
		}

		using var editForm = new AddEditBookForm(selectedBook);
		if (editForm.ShowDialog(this) != DialogResult.OK)
		{
			return;
		}

		try
		{
			bool updated = bookRepository.Update(editForm.BookData);
			LoadBooks(searchTextBox.Text);
			statusLabel.Text = updated
				? $"Book #{editForm.BookData.Id} updated"
				: $"No changes saved for book #{editForm.BookData.Id}";
		}
		catch (Exception ex)
		{
			ShowOperationError("update the book", ex);
		}
	}

	private void searchTextBox_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Enter)
		{
			return;
		}

		e.SuppressKeyPress = true;
		LoadBooks(searchTextBox.Text);
	}

	private void LoadBooks(string? keyword = null)
	{
		try
		{
			string normalizedKeyword = keyword?.Trim() ?? string.Empty;
			List<Book> books = string.IsNullOrWhiteSpace(normalizedKeyword)
				? bookRepository.GetAll()
				: bookRepository.Search(normalizedKeyword);

			booksGrid.DataSource = new BindingList<Book>(books);
			statusLabel.Text = string.IsNullOrWhiteSpace(normalizedKeyword)
				? $"{books.Count} books loaded"
				: $"{books.Count} books found for \"{normalizedKeyword}\"";
		}
		catch (Exception ex)
		{
			statusLabel.Text = "Unable to load books";
			ShowOperationError("load the book list", ex);
		}
	}

	private void ShowOperationError(string action, Exception ex)
	{
		string userMessage = BuildFriendlyErrorMessage(action, ex);

		MessageBox.Show(
			this,
			userMessage,
			"Library Management",
			MessageBoxButtons.OK,
			MessageBoxIcon.Error);
	}

	private static string BuildFriendlyErrorMessage(string action, Exception ex)
	{
		if (ex is MySqlException mySqlEx)
		{
			if (mySqlEx.Number == 1062)
			{
				return "This ISBN already exists. Please use a different ISBN and try again.";
			}

			if (mySqlEx.Number == 1042 || mySqlEx.Number == 0)
			{
				return $"Sorry, we could not {action} because the database is unreachable. Check your connection settings and try again.";
			}

			return $"Sorry, we could not {action}. Database error: {mySqlEx.Message}";
		}

		string details = ex.InnerException?.Message ?? ex.Message;
		return $"Sorry, we could not {action}. {details}";
	}

	private bool TryGetSelectedBook(out Book? selectedBook)
	{
		selectedBook = booksGrid.CurrentRow?.DataBoundItem as Book;
		if (selectedBook != null)
		{
			return true;
		}

		MessageBox.Show(
			this,
			"Select a book first.",
			"Library Management",
			MessageBoxButtons.OK,
			MessageBoxIcon.Information);

		return false;
	}
}
