using LibraryManagement.Models;
using LibraryManagement.Services;
using System.IO;

namespace LibraryManagement.Views;

public partial class AddEditBookForm : Form
{
	private readonly bool isEditMode;
	private readonly LibraryService libraryService = new();
	private ThemePalette currentPalette = ThemeManager.GetPalette(ThemeManager.CurrentTheme);
	private Image? previewPlaceholderImage;

	public AddEditBookForm()
	{
		InitializeComponent();
		isEditMode = false;
		ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
		FormClosed += AddEditBookForm_FormClosed;
		ApplyTheme(ThemeManager.GetPalette(ThemeManager.CurrentTheme));
		ConfigureForm();
	}

	public AddEditBookForm(Book book) : this()
	{
		isEditMode = true;
		ConfigureForm();
		LoadBook(book);
	}

	public Book BookData { get; private set; } = new();

	private void AddEditBookForm_FormClosed(object? sender, FormClosedEventArgs e)
	{
		ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;

		Image? previewImage = coverPreviewBox.Image;
		if (previewImage is not null && !ReferenceEquals(previewImage, previewPlaceholderImage))
		{
			previewImage.Dispose();
		}

		previewPlaceholderImage?.Dispose();
		previewPlaceholderImage = null;
	}

	private void ThemeManager_ThemeChanged(AppTheme theme)
	{
		ApplyTheme(ThemeManager.GetPalette(theme));
		RefreshCoverPreview();
	}

	private void ConfigureForm()
	{
		Text = isEditMode ? "Edit Book" : "Add Book";
		AcceptButton = saveButton;
		CancelButton = cancelButton;
		ClearValidationErrors();
	}

	private void ApplyTheme(ThemePalette palette)
	{
		currentPalette = palette;
		previewPlaceholderImage?.Dispose();
		previewPlaceholderImage = null;

		BackColor = palette.AppBackground;
		mainLayout.BackColor = palette.AppBackground;
		yearPanel.BackColor = palette.AppBackground;
		coverPathPanel.BackColor = palette.AppBackground;
		buttonPanel.BackColor = palette.AppBackground;

		Label[] fieldLabels =
		[
			titleLabel,
			authorLabel,
			isbnLabel,
			yearLabel,
			genreLabel,
			rayonLabel,
			etagereLabel,
			coverPathLabel
		];

		foreach (Label label in fieldLabels)
		{
			label.ForeColor = palette.TextMuted;
		}

		titleErrorLabel.ForeColor = palette.Danger;
		authorErrorLabel.ForeColor = palette.Danger;
		isbnErrorLabel.ForeColor = palette.Danger;
		yearErrorLabel.ForeColor = palette.Danger;

		StyleInput(titleTextBox, palette);
		StyleInput(authorTextBox, palette);
		StyleInput(isbnTextBox, palette);
		StyleInput(genreTextBox, palette);
		StyleInput(rayonTextBox, palette);
		StyleInput(etagereTextBox, palette);
		StyleInput(coverPathTextBox, palette);

		yearNumericUpDown.BackColor = palette.SurfaceAlt;
		yearNumericUpDown.ForeColor = palette.TextPrimary;
		yearNumericUpDown.BorderStyle = BorderStyle.FixedSingle;

		availableCheckBox.ForeColor = palette.TextPrimary;
		yearCheckBox.ForeColor = palette.TextPrimary;

		coverPreviewBox.BackColor = palette.PlaceholderBack;
		coverPreviewBox.BorderStyle = BorderStyle.FixedSingle;

		ConfigureButton(saveButton, palette.Accent, palette.Accent, Color.White);
		ConfigureButton(cancelButton, palette.SurfaceAlt, palette.Border, palette.TextPrimary);
		ConfigureButton(browseCoverButton, palette.SurfaceAlt, palette.Border, palette.TextPrimary);
	}

	private static void StyleInput(TextBox textBox, ThemePalette palette)
	{
		textBox.BackColor = palette.SurfaceAlt;
		textBox.ForeColor = palette.TextPrimary;
		textBox.BorderStyle = BorderStyle.FixedSingle;
	}

	private static void ConfigureButton(Button button, Color background, Color border, Color foreground)
	{
		button.FlatStyle = FlatStyle.Flat;
		button.FlatAppearance.BorderSize = 1;
		button.FlatAppearance.BorderColor = border;
		button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(background, 0.06f);
		button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(background, 0.08f);
		button.BackColor = background;
		button.ForeColor = foreground;
		button.Cursor = Cursors.Hand;
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
		Image? nextImage = image ?? GetCoverPreviewPlaceholderImage();
		Image? previous = coverPreviewBox.Image;
		coverPreviewBox.Image = nextImage;

		if (previous is not null &&
			!ReferenceEquals(previous, nextImage) &&
			!ReferenceEquals(previous, previewPlaceholderImage))
		{
			previous.Dispose();
		}
	}

	private Image GetCoverPreviewPlaceholderImage()
	{
		if (previewPlaceholderImage is not null)
		{
			return previewPlaceholderImage;
		}

		int width = Math.Max(72, coverPreviewBox.Width);
		int height = Math.Max(72, coverPreviewBox.Height);
		Bitmap bitmap = new(width, height);
		using Graphics graphics = Graphics.FromImage(bitmap);
		graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		graphics.Clear(currentPalette.PlaceholderBack);

		using Pen borderPen = new(currentPalette.Border, 1);
		graphics.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);

		Rectangle frame = new(10, 12, width - 20, height - 26);
		using Pen iconPen = new(currentPalette.PlaceholderFore, 2);
		graphics.DrawRectangle(iconPen, frame);
		graphics.DrawLine(iconPen, frame.Left + 6, frame.Bottom - 6, frame.Left + 18, frame.Top + 18);
		graphics.DrawLine(iconPen, frame.Left + 18, frame.Top + 18, frame.Left + 32, frame.Bottom - 6);
		graphics.DrawLine(iconPen, frame.Left + 26, frame.Bottom - 6, frame.Right - 8, frame.Top + 20);
		graphics.DrawEllipse(iconPen, frame.Right - 22, frame.Top + 8, 8, 8);

		previewPlaceholderImage = bitmap;
		return previewPlaceholderImage;
	}
}
