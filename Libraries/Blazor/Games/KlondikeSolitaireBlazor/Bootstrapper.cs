using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileObservable;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using KlondikeSolitaireCP.Data;
using KlondikeSolitaireCP.Logic;
using KlondikeSolitaireCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace KlondikeSolitaireBlazor
{
    public class Bootstrapper : SinglePlayerBootstrapper<KlondikeSolitaireShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<KlondikeSolitaireShellViewModel>();
            OurContainer!.RegisterType<DeckObservablePile<SolitaireCard>>(true); //i think
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
            OurContainer.RegisterType<WastePiles>(); //can't do automatically because we don't know if we will do it or not.
            OurContainer.RegisterType<MainPilesCP>();
            return Task.CompletedTask;
        }
    }
}