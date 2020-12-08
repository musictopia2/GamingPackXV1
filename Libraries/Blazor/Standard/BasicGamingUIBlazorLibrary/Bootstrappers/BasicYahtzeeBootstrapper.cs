using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels;
using BasicGameFrameworkLibrary.ViewModels;
using System.Threading.Tasks;
namespace BasicGamingUIBlazorLibrary.Bootstrappers
{
    public abstract class BasicYahtzeeBootstrapper<D> : MultiplayerBasicBootstrapper<YahtzeeShellViewModel<D>>
        where D : SimpleDice, new()
    {
        protected BasicYahtzeeBootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override bool NeedExtraLocations => false;
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterType<BasicGameLoader<YahtzeePlayerItem<D>, YahtzeeSaveInfo<D>>>();

            OurContainer.RegisterType<StandardRollProcesses<D, YahtzeePlayerItem<D>>>();

            OurContainer.RegisterSingleton<IGenerateDice<int>, D>();
            OurContainer.RegisterType<YahtzeeScoresheetViewModel<D>>(false);
            OurContainer.RegisterType<YahtzeeMainViewModel<D>>(false);
            OurContainer.RegisterType<YahtzeeVMData<D>>();
            OurContainer.RegisterType<BasicYahtzeeGame<D>>();
            OurContainer.RegisterType<YahtzeeSaveInfo<D>>();
            OurContainer.RegisterType<ScoreLogic>();
            OurContainer.RegisterType<YahtzeeGameContainer<D>>();
            OurContainer.RegisterType<ScoreContainer>();
            OurContainer.RegisterType<YahtzeeMove<D>>();
            OurContainer.RegisterType<YahtzeeEndRoundLogic<D>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<YahtzeePlayerItem<D>, YahtzeeSaveInfo<D>>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<YahtzeePlayerItem<D>>>(true);
            RegisterNonSavedClasses();
            return Task.CompletedTask;
        }
        protected abstract void RegisterNonSavedClasses();
    }
}