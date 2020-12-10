using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using RaglanSolitaireCP.Data;
using RaglanSolitaireCP.Logic;
using RaglanSolitaireCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace RaglanSolitaireBlazor
{
    public class Bootstrapper : SinglePlayerBootstrapper<RaglanSolitaireShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<RaglanSolitaireShellViewModel>();
            OurContainer!.RegisterType<DeckObservablePile<SolitaireCard>>(true); //i think
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
            //anything that needs to be registered will be here.
            //we have to resolve the IMain and IWaste.
            OurContainer.RegisterType<WastePiles>(); //can't do automatically because we don't know if we will do it or not.
            OurContainer.RegisterType<MainPilesCP>();
            return Task.CompletedTask;
        }
    }
}