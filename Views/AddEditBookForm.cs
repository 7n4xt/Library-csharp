using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Views;

public partial class AddEditBookForm : Form
{
	private readonly bool isEditMode;
	private readonly LibraryService libraryService = new();

	public AddEditBookForm()
	{
		InitializeComponent();
		isEditMode = false;
		ConfigureForm();
	}

	public AddEditBookForm(Book book) : this()
	{
		isEditMode = true;
		ConfigureForm();
		LoadBook(book);
	}

	public Book BookData { get; private set; } = new();

	private void ConfigureForm()
	{
		Text = isEditMode ? "Edit Book" : "Add Book";
		AcceptButton = saveButton;
		CancelButton = cancelButton;
		ClearValidationErrors();
	}

	private void LoadBook(Book book)
	{
		BookData = book;
		titleTextBox.Text = book.Titre;
		authorTextBox.Text = book.Auteur;
		isbnTextBox.Text = book.ISBN;
		yearCheckBox.Checked = book.Annee.HasValue;
		if (book.Annee.HasValue)
		{
			yearNumericUpDown.Value = book.Annee.Value;
		}
		else
		{
			yearNumericUpDown.Value = 2024;
		}
		genreTextBox.Text = book.Genre ?? string.Empty;
		rayonTextBox.Text = book.Rayon ?? string.Empty;
		etagereTextBox.Text = book.Etagere ?? string.Empty;
		availableCheckBox.Checked = book.Dispo;
		coverPathTextBox.Text = book.CoverPath ?? string.Empty;
	}

	private void yearCheckBox_CheckedChanged(object? sender, EventArgs e)
	{
		yearNumericUpDown.Enabled = yearCheckBox.Checked;
	}

	private void saveButton_Click(object? sender, EventArgs e)
	{
		if (!ValidateFormInputs())
		{
			DialogResult = DialogResult.None;
			return;
		}

		BookData = new Book
		{
			Id = BookData.Id,
			Titre = titleTextBox.Text.Trim(),
			Auteur = authorTextBox.Text.Trim(),
			ISBN = LibraryService.CompactIsbn(isbnTextBox.Text),
			Annee = yearCheckBox.Checked ? (int?)yearNumericUpDown.Value : null,
			Genre = string.IsNullOrWhiteSpace(genreTextBox.Text) ? null : genreTextBox.Text.Trim(),
			Rayon = string.IsNullOrWhiteSpace(rayonTextBox.Text) ? null : rayonTextBox.Text.Trim(),
			Etagere = string.IsNullOrWhiteSpace(etagereTextBox.Text) ? null : etagereTextBox.Text.Trim(),
			Dispo = availableCheckBox.Checked,
			CoverPath = string.IsNullOrWhiteSpace(coverPathTextBox.Text) ? null : coverPathTextBox.Text.Trim()
		};

		DialogResult = DialogResult.OK;
		Close();
	}

	private bool ValidateFormInputs()
	{
		ValidationResult validation = libraryService.ValidateBookInput(
			titleTextBox.Text,
			authorTextBox.Text,
			isbnTextBox.Text,
			yearCheckBox.Checked,
			yearCheckBox.Checked ? (int?)yearNumericUpDown.Value : null);

		ApplyValidationErrors(validation);

		if (validation.IsValid)
		{
			return true;
		}

		if (validation.Errors.ContainsKey("Title"))
		{
			titleTextBox.Focus();
		}
		else if (validation.Errors.ContainsKey("Author"))
		{
			authorTextBox.Focus();
		}
		else if (validation.Errors.ContainsKey("ISBN"))
		{
			isbnTextBox.Focus();
		}
		else if (validation.Errors.ContainsKey("Year"))
		{
			yearNumericUpDown.Focus();
		}

		return false;
	}

	private void ApplyValidationErrors(ValidationResult validation)
	{
		titleErrorLabel.Text = GetError(validation, "Title");
		titleErrorLabel.Visible = !string.IsNullOrEmpty(titleErrorLabel.Text);

		authorErrorLabel.Text = GetError(validation, "Author");
		authorErrorLabel.Visible = !string.IsNullOrEmpty(authorErrorLabel.Text);

		isbnErrorLabel.Text = GetError(validation, "ISBN");
		isbnErrorLabel.Visible = !string.IsNullOrEmpty(isbnErrorLabel.Text);

		yearErrorLabel.Text = GetError(validation, "Year");
		yearErrorLabel.Visible = !string.IsNullOrEmpty(yearErrorLabel.Text);
	}

	private void ClearValidationErrors()
	{
		titleErrorLabel.Text = string.Empty;
		titleErrorLabel.Visible = false;
		authorErrorLabel.Text = string.Empty;
		authorErrorLabel.Visible = false;
		isbnErrorLabel.Text = string.Empty;
		isbnErrorLabel.Visible = false;
		yearErrorLabel.Text = string.Empty;
		yearErrorLabel.Visible = false;
	}

	private static string GetError(ValidationResult validation, string key)
	{
		return validation.Errors.TryGetValue(key, out string? message) ? message : string.Empty;
	}
}
