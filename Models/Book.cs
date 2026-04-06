namespace LibraryManagement.Models;

public class Book
{
	public int Id { get; set; }
	public string Titre { get; set; } = string.Empty;
	public string Auteur { get; set; } = string.Empty;
	public string ISBN { get; set; } = string.Empty;
	public int? Annee { get; set; }
	public string? Genre { get; set; }
	public string? Rayon { get; set; }
	public string? Etagere { get; set; }
	public bool Dispo { get; set; } = true;
	public string? CoverPath { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}
