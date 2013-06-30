using MonoTouch.UIKit;

namespace NinjaLocator.Core.iOS
{
    public class UIDefinitions : UserInterfaceDefinitions<UIColor>
    {
        protected override UIColor TranslateColor(byte alpha, byte red, byte green, byte blue)
        {
            return UIColor.FromRGBA(red, green, blue, alpha);
        }
    }
}
