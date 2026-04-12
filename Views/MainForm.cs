using System.ComponentModel;
using LibraryManagement.Database;
using LibraryManagement.Models;

namespace LibraryManagement.Views;

public partial class MainForm : Form
{
	private readonly BookRepository bookRepository = new();

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
			MessageBox.Show(
				this,
				$"The book could not be added.\n\n{ex.Message}",
				"Library Management",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}
	}

	private void deleteButton_Click(object? sender, EventArgs e)
	{
		if (booksGrid.CurrentRow?.DataBoundItem is not Book selectedBook)
		{
			MessageBox.Show(
				this,
				"Select a book first.",
				"Library Management",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
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
			MessageBox.Show(
				this,
				$"The book could not be deleted.\n\n{ex.Message}",
				"Library Management",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
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
			MessageBox.Show(
				this,
				$"The book could not be updated.\n\n{ex.Message}",
				"Library Management",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
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
			MessageBox.Show(
				this,
				$"The book list could not be loaded.\n\n{ex.Message}",
				"Library Management",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}
	}
}
