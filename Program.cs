namespace LibraryManagement;

internal static class Program
{
	private static readonly Font AppFont = CreatePreferredFont();

	[STAThread]
	private static void Main()
	{
		ApplicationConfiguration.Initialize();
		Application.SetDefaultFont(AppFont);
		Application.Run(new Views.MainForm());
	}

	private static Font CreatePreferredFont()
	{
		string[] preferredFamilies = ["Segoe UI Variable Text", "Segoe UI"];
		foreach (string familyName in preferredFamilies)
		{
			if (FontFamily.Families.Any(f => string.Equals(f.Name, familyName, StringComparison.OrdinalIgnoreCase)))
			{
				return new Font(familyName, 9.5F, FontStyle.Regular, GraphicsUnit.Point);
			}
		}

		FontFamily fallbackFamily = SystemFonts.MessageBoxFont?.FontFamily ?? FontFamily.GenericSansSerif;
		return new Font(fallbackFamily, 9.5F, FontStyle.Regular, GraphicsUnit.Point);
	}
}
