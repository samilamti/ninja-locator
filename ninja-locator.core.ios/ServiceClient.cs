using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MonoTouch.UIKit;

namespace NinjaLocator.Core.iOS
{
    public class ServiceClient : Core.ServiceClient
    {
        private readonly UIViewController _visibleViewController;

        public ServiceClient(UIViewController visibleViewController)
        {
            _visibleViewController = visibleViewController;
        }

        protected override Task LoginAsync(MobileServiceAuthenticationProvider authenticationProvider)
        {
            return NativeClient.LoginAsync(_visibleViewController, authenticationProvider) as Task;
        }

        protected override Task LoginAsync(string authenticationToken)
        {
            return NativeClient.LoginAsync(authenticationToken) as Task;
        }
    }
}
