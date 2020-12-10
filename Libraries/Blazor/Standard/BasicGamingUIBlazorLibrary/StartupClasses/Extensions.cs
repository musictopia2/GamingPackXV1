using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
namespace BasicGamingUIBlazorLibrary.StartupClasses
{
    public static class Extensions
    {
        public static void RegisterDefaultSinglePlayerProcesses(this IGamePackageDIContainer container)
        {
            container.RegisterSingleton<IStartUp, SinglePlayerStartUpClass>(); //this is all that would be needed.  so just one line of code this time for single player games.
        }
    }
}