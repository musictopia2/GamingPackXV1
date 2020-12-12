using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using GamePackageSignalRClasses;
namespace MultiplayerGamesBlazorLoaderLibrary
{
    public class NetworkStartUp : IRegisterNetworks
    {
        void IRegisterNetworks.RegisterMultiplayerClasses(IGamePackageDIContainer container)
        {
            container.RegisterType<SignalRMessageService>();
            container.RegisterType<SignalRAzureEndPoint>();
            //once i bring in the desktop/tablets, this can change.
        }
    }
}