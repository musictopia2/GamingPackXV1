using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using YahtzeeCP.Data;
using YahtzeeCP.Logic;
namespace YahtzeeBlazor
{
    public class Bootstrapper : BasicYahtzeeBootstrapper<SimpleDice>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override void RegisterNonSavedClasses()
        {
            OurContainer!.RegisterNonSavedClasses<YahtzeeShellViewModel<SimpleDice>>();
            OurContainer!.RegisterType<YahtzeeDetailClass>();
            OurContainer.RegisterType<YahtzeeScoreProcesses>();
        }

    }
}