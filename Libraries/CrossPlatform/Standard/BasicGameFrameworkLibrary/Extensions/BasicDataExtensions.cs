using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.Extensions
{
    public static class BasicDataExtensions
    {
        public static INetworkMessages? GetNetwork(this BasicData basicData)
        {
            if (basicData.MultiPlayer == false)
            {
                return null;
            }
            return cons!.Resolve<INetworkMessages>();
        }
        public static IMessageChecker? GetChecker(this BasicData basicData)
        {
            if (basicData.MultiPlayer == false)
            {
                return null;
            }
            return cons!.Resolve<IMessageChecker>();
        }
    }
}