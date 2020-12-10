using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using PyramidSolitaireCP.Data;
using PyramidSolitaireCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace PyramidSolitaireBlazor
{
    public class Bootstrapper : SinglePlayerBootstrapper<PyramidSolitaireShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<PyramidSolitaireShellViewModel>();
            OurContainer!.RegisterType<DeckObservablePile<SolitaireCard>>(true); //i think
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
            //anything that needs to be registered will be here.
            return Task.CompletedTask;
        }
    }
}