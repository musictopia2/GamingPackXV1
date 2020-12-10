using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using LittleSpiderSolitaireCP.Data;
using LittleSpiderSolitaireCP.Logic;
using LittleSpiderSolitaireCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace LittleSpiderSolitaireBlazor
{
    public class Bootstrapper : SinglePlayerBootstrapper<LittleSpiderSolitaireShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<LittleSpiderSolitaireShellViewModel>();
            OurContainer!.RegisterType<DeckObservablePile<SolitaireCard>>(true); //i think
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
            OurContainer.RegisterType<WastePiles>(); //can't do automatically because we don't know if we will do it or not.
            OurContainer.RegisterType<CustomMain>();
            return Task.CompletedTask;
        }
    }
}