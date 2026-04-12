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
