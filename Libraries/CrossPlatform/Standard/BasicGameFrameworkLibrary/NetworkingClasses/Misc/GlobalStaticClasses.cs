using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
namespace BasicGameFrameworkLibrary.NetworkingClasses.Misc
{
    public static class GlobalStaticClasses
    {
        public static string LocalIPAddress
        {
            get
            {
                var ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                return ipEntry.AddressList.Last().ToString();
            }
        }
        public static bool CanConnectToHomeGameServer
        {
            get
            {
                using Ping thisPing = new Ping();
                string thisStr = "aa";
                var thisData = Encoding.ASCII.GetBytes(thisStr);
                string address = "192.168.0.150"; //i think.
                var results = thisPing.Send(address, 50, thisData);
                return results.Status == IPStatus.Success;
            }
        }
        public static string MainAzureHostAddress => "https://onlinegameserver.azurewebsites.net"; //this is default address for azure.
    }
}