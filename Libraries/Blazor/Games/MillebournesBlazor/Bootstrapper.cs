using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using MillebournesCP.Cards;
using MillebournesCP.Data;
using MillebournesCP.ViewModels;
using System.Threading.Tasks;
namespace MillebournesBlazor
{

    //public class CardSetUp : ITestCardSetUp<MillebournesCardInformation, MillebournesPlayerItem>
    //{
    //    Task ITestCardSetUp<MillebournesCardInformation, MillebournesPlayerItem>.SetUpTestHandsAsync(PlayerCollection<MillebournesPlayerItem> playerList, IListShuffler<MillebournesCardInformation> deckList)
    //    {
    //        var myPlayer = playerList.GetSelf();
    //        //var item = deckList.First(x => x.CompleteCategory == EnumCompleteCategories.PunctureProof);
    //        var item = deckList.First(item => item.CompleteCategory == EnumCompleteCategories.Roll); //the first roll is fine.  only need one.
    //        myPlayer.StartUpList.Add(item);
    //        return Task.CompletedTask;
    //    }
    //}

    public class Bootstrapper : MultiplayerBasicBootstrapper<MillebournesShellViewModel>
    {

        //only needed so i can test something.  decided to not create a new file for this.

        protected override Task RegisterTestsAsync()
        {
            //OurContainer!.RegisterType<CardSetUp>();
            return base.RegisterTestsAsync();
        }
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<MillebournesShellViewModel>();
            OurContainer!.RegisterType<BasicGameLoader<MillebournesPlayerItem, MillebournesSaveInfo>>();
            OurContainer.RegisterType<RetrieveSavedPlayers<MillebournesPlayerItem, MillebournesSaveInfo>>();
            OurContainer.RegisterType<MultiplayerOpeningViewModel<MillebournesPlayerItem>>(true);
            OurContainer.RegisterType<DeckObservablePile<MillebournesCardInformation>>(true);
            OurContainer.RegisterType<GenericCardShuffler<MillebournesCardInformation>>();
            OurContainer.RegisterSingleton<IDeckCount, MillebournesDeckCount>();
            return Task.CompletedTask;
        }
    }
}