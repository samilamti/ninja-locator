using System.Windows.Media;

namespace NinjaLocator.Core.WindowsPhone
{
    public class UIDefinitions : UserInterfaceDefinitions<Color>
    {
        protected override Color TranslateColor(byte alpha, byte red, byte green, byte blue)
        {
            return Color.FromArgb(alpha, red, green, blue);
        }
    }
}
