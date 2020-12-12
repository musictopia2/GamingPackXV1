using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.StandardImplementations.Settings;
using BasicGamingUIBlazorLibrary.LocalStorageClasses;
using CommonBasicStandardLibraries.Exceptions;
namespace MultiplayerGamesBlazorLoaderLibrary
{
    public class MainStartUp : IStartUp
    {
        void IStartUp.RegisterCustomClasses(IGamePackageDIContainer container, bool multiplayer, BasicData data)
        {
            container.RegisterType<MultiplayerAutoResumeClass>();
            if (multiplayer)
            {
                container.RegisterType<NetworkStartUp>();
            }
        }

        void IStartUp.StartVariables(BasicData data)
        {
            if (GlobalDataModel.DataContext == null)
            {
                throw new BasicBlankException("Must have the data filled out in order to get the nick names");
            }
            data.NickName = GlobalDataModel.DataContext.NickName;
        }
    }
}
