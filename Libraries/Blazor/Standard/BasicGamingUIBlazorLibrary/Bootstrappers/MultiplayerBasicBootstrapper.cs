using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.ViewModels;
namespace BasicGamingUIBlazorLibrary.Bootstrappers
{
    public abstract class MultiplayerBasicBootstrapper<TViewModel> : BasicGameBootstrapper<TViewModel>
        where TViewModel : IMainGPXShellVM
    {
        protected MultiplayerBasicBootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode) { }
        protected override void MiscRegisterFirst()
        {
            OurContainer!.RegisterType<NewRoundViewModel>(false);
            OurContainer.RegisterType<RestoreViewModel>(false);
        }
        protected override bool UseMultiplayerProcesses => true;
    }
}