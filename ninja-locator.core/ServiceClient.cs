using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace NinjaLocator.Core
{
    public abstract class ServiceClient
    {
        protected readonly MobileServiceClient NativeClient;
        public MobileServiceAuthenticationProvider AuthenticationProvider { get; set; }
        public string AuthenticationToken { get; set; }

        protected ServiceClient()
        {
            NativeClient = new MobileServiceClient(ServiceClientConfiguration.ServiceUrl, ServiceClientConfiguration.ApplicationKey);
        }

        protected abstract Task LoginAsync(MobileServiceAuthenticationProvider authenticationProvider);
        protected abstract Task LoginAsync(string authenticationToken);

        public void LocateNinjas(Ninja requestor, Action<IEnumerable<Ninja>> ninjasLocated)
        {
            Task loginTask;
            if (String.IsNullOrEmpty(AuthenticationToken))
                loginTask = LoginAsync(AuthenticationProvider);
            else
                loginTask = LoginAsync(AuthenticationToken);

            loginTask.ContinueWith(delegate
                {
                    var ninjaTable = NativeClient.GetTable<Ninja>();
                    ninjaTable.InsertAsync(requestor)
                              .ContinueWith(insertTask => ninjaTable.ReadAsync())
                              .ContinueWith(insertTask => ninjasLocated(insertTask.Result.Result), TaskContinuationOptions.AttachedToParent);

                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
