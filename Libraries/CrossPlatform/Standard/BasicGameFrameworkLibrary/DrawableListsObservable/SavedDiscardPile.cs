using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
namespace BasicGameFrameworkLibrary.DrawableListsObservable
{
    public class SavedDiscardPile<D> where D : IDeckObject, new() // hopefully this will work.  not sure though.
    {
        public D CurrentCard { get; set; } = new D();
        public DeckRegularDict<D> PileList { get; set; } = new DeckRegularDict<D>();
    }
}