using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Collections.Generic;
namespace BasicGameFrameworkLibrary.BasicDrawables.Dictionary
{
    public class DeckRegularDict<D> : CustomBasicList<D>, IDeckDict<D>, IEnumerableDeck<D> where D : IDeckObject
    {
        private DictionaryBehavior<D>? _thisB; //decided to risk not even doing the observable dictionary since this is intended to use the blazor programming model.
        public DeckRegularDict() : base() { }
        public DeckRegularDict(IEnumerable<D> thisList) : base(thisList) { }
        protected override void LoadBehavior()
        {
            _thisB = new DictionaryBehavior<D>();
            Behavior = _thisB;
        }
        public D GetSpecificItem(int deck)
        {
            return _thisB!.SearchItem(deck);
        }
        public bool ObjectExist(int deck)
        {
            return _thisB!.ObjectExist(deck);
        }
        public D RemoveObjectByDeck(int deck) //i think
        {
            D thisCard = GetSpecificItem(deck);
            RemoveSpecificItem(thisCard);
            return thisCard;
        }

        public void ReplaceDictionary(int oldValue, int deck, D newValue)
        {
            _thisB!.ReplaceDictionaryValue(oldValue, deck, newValue);
        }

    }
}