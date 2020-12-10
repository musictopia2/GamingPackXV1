using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.TriangleClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace TriangleSolitaireCP.Data
{
    [SingletonGame]
    public class TriangleSolitaireSaveInfo : ObservableObject, IMappable
    {
        public CustomBasicList<int> DeckList { get; set; } = new CustomBasicList<int>();
        //anything else needed to save a game will be here.
        public SavedDiscardPile<SolitaireCard>? PileData { get; set; }
        public SavedTriangle? TriangleData { get; set; }
        public EnumIncreaseList Incs { get; set; }
    }
}