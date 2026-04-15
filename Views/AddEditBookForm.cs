using LibraryManagement.Models;
using LibraryManagement.Services;
using System.IO;

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
		RefreshCoverPreview();
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

		string? persistedCoverPath = PersistCoverPath(coverPathTextBox.Text);

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
			CoverPath = persistedCoverPath
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
			yearCheckBox.Checked ? (int?)yearNumericUpDown.Value : null,
			genreTextBox.Text,
			rayonTextBox.Text,
			etagereTextBox.Text,
			coverPathTextBox.Text);

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
		else if (validation.Errors.ContainsKey("Genre"))
		{
			genreTextBox.Focus();
		}
		else if (validation.Errors.ContainsKey("Rayon"))
		{
			rayonTextBox.Focus();
		}
		else if (validation.Errors.ContainsKey("Etagere"))
		{
			etagereTextBox.Focus();
		}
		else if (validation.Errors.ContainsKey("CoverPath"))
		{
			coverPathTextBox.Focus();
		}

		MessageBox.Show(
			this,
			"Please correct the invalid fields and try again.",
			"Validation",
			MessageBoxButtons.OK,
			MessageBoxIcon.Warning);

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

	private void browseCoverButton_Click(object? sender, EventArgs e)
	{
		using var fileDialog = new OpenFileDialog
		{
			Title = "Select cover image",
			Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp|All Files|*.*",
			CheckFileExists = true,
			Multiselect = false
		};

		if (fileDialog.ShowDialog(this) != DialogResult.OK)
		{
			return;
		}

		coverPathTextBox.Text = fileDialog.FileName;
		RefreshCoverPreview();
	}

	private void coverPathTextBox_TextChanged(object? sender, EventArgs e)
	{
		RefreshCoverPreview();
	}

	private void RefreshCoverPreview()
	{
		string path = coverPathTextBox.Text.Trim();
		if (string.IsNullOrWhiteSpace(path))
		{
			SetCoverPreviewImage(null);
			return;
		}

		string absolutePath = ResolveCoverAbsolutePath(path);
		if (!File.Exists(absolutePath))
		{
			SetCoverPreviewImage(null);
			return;
		}

		try
		{
			using var stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			using var sourceImage = Image.FromStream(stream);
			SetCoverPreviewImage(new Bitmap(sourceImage));
		}
		catch
		{
			SetCoverPreviewImage(null);
		}
	}

	private string? PersistCoverPath(string rawPath)
	{
		string trimmedPath = rawPath.Trim();
		if (string.IsNullOrWhiteSpace(trimmedPath))
		{
			return null;
		}

		if (!Path.IsPathRooted(trimmedPath) && trimmedPath.Replace('\\', '/').StartsWith("Resources/Covers/", StringComparison.OrdinalIgnoreCase))
		{
			return trimmedPath.Replace('\\', '/');
		}

		string sourcePath = ResolveCoverAbsolutePath(trimmedPath);
		if (!File.Exists(sourcePath))
		{
			return trimmedPath;
		}

		string coversDirectory = Path.Combine(AppContext.BaseDirectory, "Resources", "Covers");
		Directory.CreateDirectory(coversDirectory);

		string extension = Path.GetExtension(sourcePath);
		if (string.IsNullOrWhiteSpace(extension))
		{
			extension = ".img";
		}

		string destinationFileName = $"cover_{DateTime.Now:yyyyMMdd_HHmmss_fff}{extension.ToLowerInvariant()}";
		string destinationPath = Path.Combine(coversDirectory, destinationFileName);
		File.Copy(sourcePath, destinationPath, true);

		return Path.Combine("Resources", "Covers", destinationFileName).Replace('\\', '/');
	}

	private static string ResolveCoverAbsolutePath(string path)
	{
		if (Path.IsPathRooted(path))
		{
			return path;
		}

		return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, path));
	}

	private void SetCoverPreviewImage(Image? image)
	{
		Image? previous = coverPreviewBox.Image;
		coverPreviewBox.Image = image;
		previous?.Dispose();
	}
}
