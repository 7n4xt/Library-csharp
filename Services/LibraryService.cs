using System.Text.RegularExpressions;

namespace LibraryManagement.Services;

public class LibraryService
{
	private const int TitleMaxLength = 255;
	private const int AuthorMaxLength = 255;
	private const int IsbnMaxLength = 20;
	private const int GenreMaxLength = 100;
	private const int RayonMaxLength = 100;
	private const int EtagereMaxLength = 100;
	private const int CoverPathMaxLength = 500;
	private static readonly Regex IsbnPattern = new("^[0-9]+[Xx]?$", RegexOptions.Compiled);

	public ValidationResult ValidateBookInput(
		string title,
		string author,
		string isbn,
		bool hasYear,
		int? year,
		string? genre,
		string? rayon,
		string? etagere,
		string? coverPath)
	{
		var result = new ValidationResult();
		int maxYear = DateTime.Now.Year + 1;

		if (string.IsNullOrWhiteSpace(title))
		{
			result.Errors["Title"] = "Title is required.";
		}
		else if (title.Trim().Length > TitleMaxLength)
		{
			result.Errors["Title"] = $"Title cannot exceed {TitleMaxLength} characters.";
		}

		if (string.IsNullOrWhiteSpace(author))
		{
			result.Errors["Author"] = "Author is required.";
		}
		else if (author.Trim().Length > AuthorMaxLength)
		{
			result.Errors["Author"] = $"Author cannot exceed {AuthorMaxLength} characters.";
		}

		string isbnCompact = CompactIsbn(isbn);
		if (string.IsNullOrWhiteSpace(isbnCompact))
		{
			result.Errors["ISBN"] = "ISBN is required.";
		}
		else if (isbnCompact.Length < 10 || isbnCompact.Length > IsbnMaxLength || !IsbnPattern.IsMatch(isbnCompact))
		{
			result.Errors["ISBN"] = "ISBN must be 10-20 characters using digits, with optional X at the end.";
		}

		if (hasYear && (!year.HasValue || year.Value < 1450 || year.Value > maxYear))
		{
			result.Errors["Year"] = $"Year must be between 1450 and {maxYear}.";
		}

		if (!string.IsNullOrWhiteSpace(genre) && genre.Trim().Length > GenreMaxLength)
		{
			result.Errors["Genre"] = $"Genre cannot exceed {GenreMaxLength} characters.";
		}

		if (!string.IsNullOrWhiteSpace(rayon) && rayon.Trim().Length > RayonMaxLength)
		{
			result.Errors["Rayon"] = $"Shelf zone cannot exceed {RayonMaxLength} characters.";
		}

		if (!string.IsNullOrWhiteSpace(etagere) && etagere.Trim().Length > EtagereMaxLength)
		{
			result.Errors["Etagere"] = $"Shelf cannot exceed {EtagereMaxLength} characters.";
		}

		if (!string.IsNullOrWhiteSpace(coverPath) && coverPath.Trim().Length > CoverPathMaxLength)
		{
			result.Errors["CoverPath"] = $"Cover path cannot exceed {CoverPathMaxLength} characters.";
		}

		return result;
	}

	public static string CompactIsbn(string isbn)
	{
		return isbn.Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
	}
}

public class ValidationResult
{
	public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
	public bool IsValid => Errors.Count == 0;
}
