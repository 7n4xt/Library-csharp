namespace LibraryManagement.Models;

public class Borrowing
{
	public int Id { get; set; }
	public int BookId { get; set; }
	public string BorrowerName { get; set; } = string.Empty;
	public DateTime BorrowDate { get; set; }
	public DateTime? DueDate { get; set; }
	public DateTime? ReturnDate { get; set; }
	public string? Notes { get; set; }
}
