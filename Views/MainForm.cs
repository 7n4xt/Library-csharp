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

	private void LoadBooks()
	{
		try
		{
			List<Book> books = bookRepository.GetAll();
			booksGrid.DataSource = new BindingList<Book>(books);
			statusLabel.Text = $"{books.Count} books loaded";
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
