using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BasicGameFrameworkLibrary.SolitaireClasses.GraphicsObservable
{
    public class PileInfoCP : ObservableObject
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (SetProperty(ref _isSelected, value)) { }
            }
        }
        public DeckRegularDict<SolitaireCard> TempList = new DeckRegularDict<SolitaireCard>(); //this is needed because otherwise the piles has performance problems.
        public DeckRegularDict<SolitaireCard> CardList = new DeckRegularDict<SolitaireCard>(); //iffy if we really need both now.
    }
}