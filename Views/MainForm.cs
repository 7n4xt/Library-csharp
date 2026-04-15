using System.ComponentModel;
using System.Drawing.Drawing2D;
using LibraryManagement.Database;
using LibraryManagement.Models;
using LibraryManagement.Services;
using MySql.Data.MySqlClient;

namespace LibraryManagement.Views;

public partial class MainForm : Form
{
	private readonly BookRepository bookRepository = new();
	private readonly BorrowingRepository borrowingRepository = new();
	private readonly List<Image> gridCoverImages = [];
	private ThemePalette currentPalette = ThemeManager.GetPalette(ThemeManager.CurrentTheme);
	private Image? coverPlaceholderImage;

	public MainForm()
	{
		InitializeComponent();
		ConfigureGridColumns();
		ThemeManager.ThemeChanged += ThemeManager_ThemeChanged;
		FormClosed += MainForm_FormClosed;
		ApplyTheme(ThemeManager.GetPalette(ThemeManager.CurrentTheme));
		Load += MainForm_Load;
	}

	private void MainForm_Load(object? sender, EventArgs e)
	{
		LoadBooks();
	}

	private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
	{
		ThemeManager.ThemeChanged -= ThemeManager_ThemeChanged;
		DisposeGridCoverImages();
		coverPlaceholderImage?.Dispose();
		coverPlaceholderImage = null;
	}

	private void ThemeManager_ThemeChanged(AppTheme theme)
	{
		ApplyTheme(ThemeManager.GetPalette(theme));
		LoadBooks(searchTextBox.Text);
	}

	private void ApplyTheme(ThemePalette palette)
	{
		currentPalette = palette;
		coverPlaceholderImage?.Dispose();
		coverPlaceholderImage = null;

		BackColor = palette.AppBackground;
		searchPanel.BackColor = palette.Surface;
		statusLabel.BackColor = palette.Surface;
		statusLabel.ForeColor = palette.TextMuted;
		searchLabel.ForeColor = palette.TextMuted;

		searchTextBox.BackColor = palette.SurfaceAlt;
		searchTextBox.ForeColor = palette.TextPrimary;
		searchTextBox.BorderStyle = BorderStyle.FixedSingle;

		ConfigureButton(searchButton, palette.SurfaceAlt, palette.Border, palette.TextPrimary);
		ConfigureButton(borrowButton, palette.Accent, palette.Accent, Color.White);
		ConfigureButton(returnButton, palette.SurfaceAlt, palette.Border, palette.TextPrimary);
		ConfigureButton(addButton, palette.SurfaceAlt, palette.Border, palette.TextPrimary);
		ConfigureButton(deleteButton, palette.SurfaceAlt, palette.Danger, palette.Danger);
		ConfigureButton(themeToggleButton, palette.SurfaceAlt, palette.Border, palette.TextPrimary);
		themeToggleButton.Text = ThemeManager.CurrentTheme == AppTheme.Dark ? "Dark" : "Light";

		booksGrid.BackgroundColor = palette.Surface;
		booksGrid.BorderStyle = BorderStyle.None;
		booksGrid.GridColor = palette.GridLine;
		booksGrid.EnableHeadersVisualStyles = false;
		booksGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
		booksGrid.ColumnHeadersDefaultCellStyle.BackColor = palette.SurfaceAlt;
		booksGrid.ColumnHeadersDefaultCellStyle.ForeColor = palette.TextPrimary;
		booksGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = palette.SurfaceAlt;
		booksGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = palette.TextPrimary;
		booksGrid.ColumnHeadersDefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
		booksGrid.DefaultCellStyle.BackColor = palette.Surface;
		booksGrid.DefaultCellStyle.ForeColor = palette.TextPrimary;
		booksGrid.DefaultCellStyle.SelectionBackColor = palette.Selection;
		booksGrid.DefaultCellStyle.SelectionForeColor = palette.SelectionText;
		booksGrid.RowTemplate.Height = 58;
		booksGrid.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
		booksGrid.RowTemplate.Resizable = DataGridViewTriState.False;
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

	private void ConfigureGridColumns()
	{
		booksGrid.AutoGenerateColumns = false;
		booksGrid.Columns.Clear();

		var coverColumn = new DataGridViewImageColumn
		{
			DataPropertyName = nameof(BookGridRow.Cover),
			Name = "Cover",
			HeaderText = "Cover",
			Width = 72,
			ImageLayout = DataGridViewImageCellLayout.Zoom,
			SortMode = DataGridViewColumnSortMode.NotSortable,
			ReadOnly = true
		};
		booksGrid.Columns.Add(coverColumn);

		booksGrid.Columns.Add(CreateTextColumn("Title", nameof(BookGridRow.Titre), 200));
		booksGrid.Columns.Add(CreateTextColumn("Author", nameof(BookGridRow.Auteur), 150));
		booksGrid.Columns.Add(CreateTextColumn("ISBN", nameof(BookGridRow.ISBN), 120));
		booksGrid.Columns.Add(CreateTextColumn("Year", nameof(BookGridRow.Annee), 70));
		booksGrid.Columns.Add(CreateTextColumn("Genre", nameof(BookGridRow.Genre), 110));
		booksGrid.Columns.Add(CreateTextColumn("Rayon", nameof(BookGridRow.Rayon), 80));
		booksGrid.Columns.Add(CreateTextColumn("Shelf", nameof(BookGridRow.Etagere), 80));
		booksGrid.Columns.Add(new DataGridViewCheckBoxColumn
		{
			DataPropertyName = nameof(BookGridRow.Dispo),
			Name = "Available",
			HeaderText = "Available",
			Width = 72,
			ReadOnly = true,
			Resizable = DataGridViewTriState.False
		});
		booksGrid.Columns.Add(CreateTextColumn("Updated", nameof(BookGridRow.UpdatedAt), 140));
	}

	private static DataGridViewTextBoxColumn CreateTextColumn(string header, string propertyName, int minWidth)
	{
		return new DataGridViewTextBoxColumn
		{
			HeaderText = header,
			DataPropertyName = propertyName,
			ReadOnly = true,
			MinimumWidth = minWidth,
			AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
		};
	}

	private void searchButton_Click(object? sender, EventArgs e)
	{
		LoadBooks(searchTextBox.Text);
	}

	private void themeToggleButton_Click(object? sender, EventArgs e)
	{
		ThemeManager.ToggleTheme();
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
		if (TryGetSelectedBook(out Book? selectedBook) == false || selectedBook is null)
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
		if (TryGetSelectedBook(out Book? selectedBook) == false || selectedBook is null)
		{
			return;
		}

		if (!selectedBook.Dispo)
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
		if (TryGetSelectedBook(out Book? selectedBook) == false || selectedBook is null)
		{
			return;
		}

		if (selectedBook.Dispo)
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

		if (booksGrid.Rows[e.RowIndex].DataBoundItem is not BookGridRow selectedRow)
		{
			return;
		}

		using var editForm = new AddEditBookForm(selectedRow.Source);
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

			DisposeGridCoverImages();
			List<BookGridRow> rows = books.Select(MapBookToGridRow).ToList();
			booksGrid.DataSource = new BindingList<BookGridRow>(rows);

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

	private BookGridRow MapBookToGridRow(Book book)
	{
		return new BookGridRow
		{
			Cover = BuildCoverCellImage(book.CoverPath),
			Titre = book.Titre,
			Auteur = book.Auteur,
			ISBN = book.ISBN,
			Annee = book.Annee?.ToString() ?? "-",
			Genre = string.IsNullOrWhiteSpace(book.Genre) ? "-" : book.Genre,
			Rayon = string.IsNullOrWhiteSpace(book.Rayon) ? "-" : book.Rayon,
			Etagere = string.IsNullOrWhiteSpace(book.Etagere) ? "-" : book.Etagere,
			Dispo = book.Dispo,
			UpdatedAt = book.UpdatedAt.ToString("yyyy-MM-dd HH:mm"),
			Source = book
		};
	}

	private Image BuildCoverCellImage(string? coverPath)
	{
		if (TryResolveCoverPath(coverPath, out string? resolvedPath) && resolvedPath is not null)
		{
			try
			{
				using var stream = new FileStream(resolvedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				using var sourceImage = Image.FromStream(stream);
				Image thumbnail = CreateThumbnail(sourceImage, 42, 56, currentPalette.Surface);
				gridCoverImages.Add(thumbnail);
				return thumbnail;
			}
			catch
			{
				// Ignore invalid image files and use placeholder instead.
			}
		}

		coverPlaceholderImage ??= CreateCoverPlaceholderImage(42, 56);
		return coverPlaceholderImage;
	}

	private bool TryResolveCoverPath(string? coverPath, out string? resolvedPath)
	{
		resolvedPath = null;
		if (string.IsNullOrWhiteSpace(coverPath))
		{
			return false;
		}

		string trimmed = coverPath.Trim();
		resolvedPath = Path.IsPathRooted(trimmed)
			? trimmed
			: Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, trimmed));

		if (!File.Exists(resolvedPath))
		{
			resolvedPath = null;
			return false;
		}

		return true;
	}

	private static Image CreateThumbnail(Image source, int width, int height, Color background)
	{
		Bitmap bitmap = new(width, height);
		using Graphics graphics = Graphics.FromImage(bitmap);
		graphics.Clear(background);
		graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

		float ratio = Math.Min((float)width / source.Width, (float)height / source.Height);
		int drawWidth = Math.Max(1, (int)(source.Width * ratio));
		int drawHeight = Math.Max(1, (int)(source.Height * ratio));
		int x = (width - drawWidth) / 2;
		int y = (height - drawHeight) / 2;
		graphics.DrawImage(source, x, y, drawWidth, drawHeight);
		return bitmap;
	}

	private Image CreateCoverPlaceholderImage(int width, int height)
	{
		Bitmap bitmap = new(width, height);
		using Graphics graphics = Graphics.FromImage(bitmap);
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		graphics.Clear(currentPalette.PlaceholderBack);

		using Pen borderPen = new(currentPalette.Border, 1);
		graphics.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);

		Rectangle mountain = new(8, height / 2, width - 16, height / 3);
		using Pen iconPen = new(currentPalette.PlaceholderFore, 2);
		graphics.DrawRectangle(iconPen, mountain);
		graphics.DrawLine(iconPen, mountain.Left + 4, mountain.Bottom - 2, mountain.Left + 11, mountain.Top + 7);
		graphics.DrawLine(iconPen, mountain.Left + 11, mountain.Top + 7, mountain.Left + 18, mountain.Bottom - 2);
		graphics.DrawLine(iconPen, mountain.Left + 14, mountain.Bottom - 2, mountain.Right - 4, mountain.Top + 8);
		graphics.DrawEllipse(iconPen, mountain.Right - 14, mountain.Top + 4, 5, 5);
		return bitmap;
	}

	private void DisposeGridCoverImages()
	{
		foreach (Image image in gridCoverImages)
		{
			image.Dispose();
		}

		gridCoverImages.Clear();
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
		selectedBook = (booksGrid.CurrentRow?.DataBoundItem as BookGridRow)?.Source;
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

	private sealed class BookGridRow
	{
		public required Image Cover { get; init; }
		public required string Titre { get; init; }
		public required string Auteur { get; init; }
		public required string ISBN { get; init; }
		public required string Annee { get; init; }
		public required string Genre { get; init; }
		public required string Rayon { get; init; }
		public required string Etagere { get; init; }
		public required bool Dispo { get; init; }
		public required string UpdatedAt { get; init; }
		public required Book Source { get; init; }
	}
}
