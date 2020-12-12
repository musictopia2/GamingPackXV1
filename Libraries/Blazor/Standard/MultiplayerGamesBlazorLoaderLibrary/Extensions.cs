using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.Settings;
using BasicGamingUIBlazorLibrary.StartupClasses;
using GameLoaderBlazorLibrary;
using Microsoft.Extensions.DependencyInjection;
namespace MultiplayerGamesBlazorLoaderLibrary
{
    public static class Extensions
    {
        public static void RegisterDefaultMultiplayerProcesses<V>(this IServiceCollection services)
            where V : class, ILoaderVM
        {
            services.AddTransient<ILoaderVM, V>();
            services.AddTransient<IStartUp, MainStartUp>();
            GlobalStartUp.KeysToSave.Clear(); //go ahead and clear just in case.
            //i think here is a good place to go ahead and add to list exception.
            GlobalStartUp.KeysToSave.Add(GlobalDataModel.LocalStorageKey); //if i change it, will change everywhere.
        }
        //was going to do the components here.  the only problem is the basic loader can't access though.
        //unless i have something where it can be rendered.
        //will first attempt to allow the gameloader to go ahead and have the extra component.  well see how that goes.

    }
}