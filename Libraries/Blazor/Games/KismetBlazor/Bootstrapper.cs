using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using KismetCP.Data;
using KismetCP.Logic;
namespace KismetBlazor
{
    public class Bootstrapper : BasicYahtzeeBootstrapper<KismetDice>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override void RegisterNonSavedClasses()
        {
            OurContainer!.RegisterNonSavedClasses<YahtzeeShellViewModel<KismetDice>>();
            OurContainer!.RegisterType<KismetDetailClass>();
            OurContainer.RegisterType<KismetScoreProcesses>();
            OurContainer.RegisterType<KismetMissTurn>();
        }
    }
}