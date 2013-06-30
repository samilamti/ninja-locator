using System.Collections.Generic;

namespace NinjaLocator.Core
{
    public abstract class UserInterfaceDefinitions<TNativeColor>
    {
        private readonly byte[] _foregroundArgb = new byte[] {255, 141, 198, 63};
        private readonly byte[] _backgroundArgb = new byte[] {255, 64, 64, 64};

        protected abstract TNativeColor TranslateColor(byte alpha, byte red, byte green, byte blue);
        
        private TNativeColor RequestTranslation(IList<byte> argbByteArray)
        {
            return TranslateColor(argbByteArray[0], argbByteArray[1], argbByteArray[2], argbByteArray[3]);
        }

        public TNativeColor ForegroundColor
        {
            get { return RequestTranslation(_foregroundArgb); }
        }

        public TNativeColor BackgroundColor
        {
            get { return RequestTranslation(_backgroundArgb); }
        }
    }
}
