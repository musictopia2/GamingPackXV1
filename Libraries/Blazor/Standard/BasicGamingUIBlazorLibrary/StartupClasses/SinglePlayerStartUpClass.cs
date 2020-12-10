using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGamingUIBlazorLibrary.LocalStorageClasses;
using CommonBasicStandardLibraries.Exceptions;
namespace BasicGamingUIBlazorLibrary.StartupClasses
{
    public class SinglePlayerStartUpClass : IStartUp
    {
        void IStartUp.RegisterCustomClasses(IGamePackageDIContainer container, bool multiplayer, BasicData data)
        {
            if (multiplayer == true)
            {
                throw new BasicBlankException("Only single player games are supported for this implementation");
            }
            container.RegisterType<SinglePlayerAutoResumeClass>();
        }
        void IStartUp.StartVariables(BasicData data) { }
    }
}