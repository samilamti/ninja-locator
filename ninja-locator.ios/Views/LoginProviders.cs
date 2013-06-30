using System;
using System.Drawing;
using Microsoft.WindowsAzure.MobileServices;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NinjaLocator.iOS.Views
{
    [Register("LoginProviders")]
    public class LoginProviders : UIActionSheet
    {
        private MobileServiceAuthenticationProvider _value;

        public LoginProviders()
        {
            Initialize();
        }

        public LoginProviders(RectangleF bounds)
            : base(bounds)
        {
            Initialize();
        }

        public MobileServiceAuthenticationProvider Value
        {
            get { return _value; }
        }

        void Initialize()
        {
            foreach (var name in Enum.GetNames(typeof(MobileServiceAuthenticationProvider)))
            {
                AddButton(name.Replace("MicrosoftAccount", "Microsoft Account"));
            }
        }

        public override void DismissWithClickedButtonIndex(int buttonIndex, bool animated)
        {
            _value = (MobileServiceAuthenticationProvider) buttonIndex;
            base.DismissWithClickedButtonIndex(buttonIndex, animated);
        }
    }
}