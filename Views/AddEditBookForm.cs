using LibraryManagement.Models;

namespace LibraryManagement.Views;

public partial class AddEditBookForm : Form
{
	private readonly bool isEditMode;

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
			ISBN = isbnTextBox.Text.Trim(),
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
		bool isValid = true;

		isValid &= ValidateRequiredField(titleTextBox, titleErrorLabel, "Title is required.");
		isValid &= ValidateRequiredField(authorTextBox, authorErrorLabel, "Author is required.");
		isValid &= ValidateRequiredField(isbnTextBox, isbnErrorLabel, "ISBN is required.");

		if (yearCheckBox.Checked && (yearNumericUpDown.Value < 1000 || yearNumericUpDown.Value > 3000))
		{
			yearErrorLabel.Text = "Enter a valid year.";
			yearErrorLabel.Visible = true;
			isValid = false;
		}
		else
		{
			yearErrorLabel.Text = string.Empty;
			yearErrorLabel.Visible = false;
		}

		return isValid;
	}

	private static bool ValidateRequiredField(TextBox textBox, Label errorLabel, string message)
	{
		if (!string.IsNullOrWhiteSpace(textBox.Text))
		{
			errorLabel.Text = string.Empty;
			errorLabel.Visible = false;
			return true;
		}

		errorLabel.Text = message;
		errorLabel.Visible = true;
		return false;
	}
}
