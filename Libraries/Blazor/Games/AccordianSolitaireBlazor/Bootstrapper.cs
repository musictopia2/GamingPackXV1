using AccordianSolitaireCP.Data;
using AccordianSolitaireCP.ViewModels;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace AccordianSolitaireBlazor
{
    public class Bootstrapper : SinglePlayerBootstrapper<AccordianSolitaireShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<AccordianSolitaireShellViewModel>();
            OurContainer!.RegisterType<DeckObservablePile<AccordianSolitaireCardInfo>>(true); //i think
            OurContainer.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
            OurContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
            return Task.CompletedTask;
        }
    }
}