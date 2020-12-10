using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace RaglanSolitaireCP.Data
{
    [SingletonGame]
    public class RaglanSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        public DeckRegularDict<SolitaireCard> StockCards { get; set; } = new DeckRegularDict<SolitaireCard>();
    }
}