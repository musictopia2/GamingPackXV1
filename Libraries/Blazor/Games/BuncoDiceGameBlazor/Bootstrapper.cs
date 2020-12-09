using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using BuncoDiceGameCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BuncoDiceGameBlazor
{
    public class Bootstrapper : SinglePlayerBootstrapper<BuncoDiceGameShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<BuncoDiceGameShellViewModel>();
            OurContainer!.RegisterSingleton<IGenerateDice<int>, SimpleDice>();
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}