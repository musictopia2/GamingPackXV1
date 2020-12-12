using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using BasicGameFrameworkLibrary.StandardImplementations.Settings;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MultiplayerGamesBlazorLoaderLibrary
{
    public class SignalRAzureEndPoint : ISignalRInfo
    {
        Task<string> ISignalRInfo.GetEndPointAsync()
        {
            return Task.FromResult("/hubs/gamepackage/messages");
        }

        Task<string> ITCPInfo.GetIPAddressAsync()
        {
            //figure out the address now.
            if (GlobalDataModel.DataContext == null)
            {
                throw new BasicBlankException("There is no global setting.  Must have global settings in order to get the ipaddress for the signal r service");
            }
            return Task.FromResult(GlobalDataModel.DataContext.GetEndPoint());
        }

        Task<int> ITCPInfo.GetPortAsync()
        {
            return Task.FromResult(80);
        }

        Task<bool> ISignalRInfo.IsAzureAsync()
        {
            return Task.FromResult(true);
        }
    }
}
