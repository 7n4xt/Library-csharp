namespace LibraryManagement.Services;

public enum AppTheme
{
	Light,
	Dark
}

public sealed class ThemePalette
{
	public required Color AppBackground { get; init; }
	public required Color Surface { get; init; }
	public required Color SurfaceAlt { get; init; }
	public required Color TextPrimary { get; init; }
	public required Color TextMuted { get; init; }
	public required Color Border { get; init; }
	public required Color Accent { get; init; }
	public required Color Danger { get; init; }
	public required Color GridLine { get; init; }
	public required Color Selection { get; init; }
	public required Color SelectionText { get; init; }
	public required Color PlaceholderBack { get; init; }
	public required Color PlaceholderFore { get; init; }
}

public static class ThemeManager
{
	public static AppTheme CurrentTheme { get; private set; } = AppTheme.Dark;

	public static event Action<AppTheme>? ThemeChanged;

	public static void ToggleTheme()
	{
		SetTheme(CurrentTheme == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark);
	}

	public static void SetTheme(AppTheme theme)
	{
		if (CurrentTheme == theme)
		{
			return;
		}

		CurrentTheme = theme;
		ThemeChanged?.Invoke(CurrentTheme);
	}

	public static ThemePalette GetPalette(AppTheme theme)
	{
		return theme == AppTheme.Dark
			? new ThemePalette
			{
				AppBackground = Color.FromArgb(32, 34, 37),
				Surface = Color.FromArgb(40, 43, 48),
				SurfaceAlt = Color.FromArgb(48, 52, 58),
				TextPrimary = Color.FromArgb(239, 240, 241),
				TextMuted = Color.FromArgb(170, 175, 182),
				Border = Color.FromArgb(70, 74, 80),
				Accent = Color.FromArgb(76, 167, 255),
				Danger = Color.FromArgb(255, 103, 98),
				GridLine = Color.FromArgb(63, 66, 72),
				Selection = Color.FromArgb(61, 95, 132),
				SelectionText = Color.FromArgb(248, 250, 252),
				PlaceholderBack = Color.FromArgb(56, 60, 66),
				PlaceholderFore = Color.FromArgb(170, 175, 182)
			}
			: new ThemePalette
			{
				AppBackground = Color.FromArgb(243, 243, 243),
				Surface = Color.White,
				SurfaceAlt = Color.FromArgb(248, 248, 248),
				TextPrimary = Color.FromArgb(24, 24, 24),
				TextMuted = Color.FromArgb(93, 93, 93),
				Border = Color.FromArgb(218, 220, 224),
				Accent = Color.FromArgb(0, 120, 212),
				Danger = Color.FromArgb(196, 43, 28),
				GridLine = Color.FromArgb(230, 230, 230),
				Selection = Color.FromArgb(224, 238, 255),
				SelectionText = Color.FromArgb(20, 20, 20),
				PlaceholderBack = Color.FromArgb(237, 239, 242),
				PlaceholderFore = Color.FromArgb(125, 125, 125)
			};
	}
}
