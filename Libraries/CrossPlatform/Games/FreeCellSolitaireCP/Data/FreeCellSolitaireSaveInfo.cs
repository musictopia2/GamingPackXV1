using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace FreeCellSolitaireCP.Data
{
    [SingletonGame]
    public class FreeCellSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        public CustomBasicList<BasicPileInfo<SolitaireCard>> FreeCards = new CustomBasicList<BasicPileInfo<SolitaireCard>>();
    }
}