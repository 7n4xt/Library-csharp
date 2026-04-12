using System.Text.RegularExpressions;

namespace LibraryManagement.Services;

public class LibraryService
{
	private static readonly Regex IsbnPattern = new("^[0-9Xx]+$", RegexOptions.Compiled);

	public ValidationResult ValidateBookInput(
		string title,
		string author,
		string isbn,
		bool hasYear,
		int? year)
	{
		var result = new ValidationResult();

		if (string.IsNullOrWhiteSpace(title))
		{
			result.Errors["Title"] = "Title is required.";
		}

		if (string.IsNullOrWhiteSpace(author))
		{
			result.Errors["Author"] = "Author is required.";
		}

		string isbnCompact = CompactIsbn(isbn);
		if (string.IsNullOrWhiteSpace(isbnCompact))
		{
			result.Errors["ISBN"] = "ISBN is required.";
		}
		else if (isbnCompact.Length < 10 || isbnCompact.Length > 17 || !IsbnPattern.IsMatch(isbnCompact))
		{
			result.Errors["ISBN"] = "ISBN must be 10-17 characters (digits and optional X).";
		}

		if (hasYear && (!year.HasValue || year.Value < 1000 || year.Value > 3000))
		{
			result.Errors["Year"] = "Enter a valid year.";
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
