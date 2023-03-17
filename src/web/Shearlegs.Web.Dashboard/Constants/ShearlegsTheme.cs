using MudBlazor;

namespace Shearlegs.Web.Dashboard.Constants
{
    public class ShearlegsTheme
    {
        public static readonly MudTheme MudTheme = new()
        {
            Palette = new Palette()
            {
                Primary = Colors.Green.Darken3,
                Secondary = Colors.Blue.Default,
                AppbarBackground = Colors.Green.Darken3                
            },
            PaletteDark = new()
            {
                Primary = Colors.Green.Darken1,
                Secondary = Colors.Blue.Default,
                AppbarBackground = Colors.Green.Darken3,
                Background = Colors.Grey.Darken4,
                TextPrimary = Colors.Shades.White,
                DrawerBackground = Colors.Grey.Darken3,
                DrawerText = Colors.Shades.White,
                DrawerIcon = Colors.Shades.White,
                BackgroundGrey = Colors.Grey.Darken3,
                TableLines = Colors.Grey.Darken2,
                Surface = Colors.Grey.Darken3
            }
        };
    }
}
