using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.PileInterfaces;
namespace BasicGameFrameworkLibrary.SolitaireClasses.BasicVMInterfaces
{
    public interface IBasicSolitaireVM
    {
        DeckObservablePile<SolitaireCard> DeckPile { get; set; } //i think
        SingleObservablePile<SolitaireCard> MainDiscardPile { get; set; }
        IWaste WastePiles1 { get; set; }
        IMain MainPiles1 { get; set; }
        bool CanStartNewGameImmediately { get; set; } //games like spider solitiare, can't start right away
    }
}