using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace NinjaLocator.Core.WindowsPhone
{
    public class ServiceClient : Core.ServiceClient
    {
        protected override Task LoginAsync(string authenticationToken)
        {
            return NativeClient.LoginAsync(authenticationToken);
        }

        protected override Task LoginAsync(MobileServiceAuthenticationProvider authenticationProvider)
        {
            return NativeClient.LoginAsync(authenticationProvider);
        }
    }
}
