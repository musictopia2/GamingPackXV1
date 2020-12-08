using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
namespace BasicGamingUIBlazorLibrary.Bootstrappers
{
    public abstract class SinglePlayerBootstrapper<TViewModel> : BasicGameBootstrapper<TViewModel>
        where TViewModel : IMainGPXShellVM
    {
        protected override bool UseMultiplayerProcesses => false;
        protected SinglePlayerBootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode) { }
    }
}