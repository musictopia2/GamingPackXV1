using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.ColorCards
{
    public class ColorCardsShuffler<C> : IDeckShuffler<C>, IListShuffler<C>,
        IAdvancedDIContainer, ISimpleList<C> where C : IColorCard, new()
    {
        public ColorCardsShuffler()
        {
            _thisShuffle = new BasicObjectShuffler<C>(_objectList);
        }
        private readonly BasicObjectShuffler<C> _thisShuffle;
        private readonly DeckRegularDict<C> _objectList = new DeckRegularDict<C>();
        public IGamePackageResolver? MainContainer { get => _thisShuffle.MainContainer; set => _thisShuffle.MainContainer = value; }
        public int Count => _objectList.Count;
        public bool NeedsToRedo { get => _thisShuffle.NeedsToRedo; set => _thisShuffle.NeedsToRedo = value; }
        public void ClearObjects()
        {
            _thisShuffle.ClearObjects();
        }
        public bool Contains(C item)
        {
            return _objectList.Contains(item);
        }
        public bool Exists(Predicate<C> match)
        {
            return _objectList.Exists(match);
        }
        public C Find(Predicate<C> match)
        {
            return _objectList.Find(match);
        }
        public ICustomBasicList<C> FindAll(Predicate<C> match)
        {
            return _objectList.FindAll(match);
        }
        public int FindFirstIndex(Predicate<C> match)
        {
            return _objectList.FindFirstIndex(match);
        }
        public int FindFirstIndex(int startIndex, Predicate<C> match)
        {
            return _objectList.FindFirstIndex(startIndex, match);
        }
        public int FindFirstIndex(int startIndex, int count, Predicate<C> match)
        {
            return _objectList.FindFirstIndex(startIndex, count, match);
        }
        public C FindLast(Predicate<C> match)
        {
            return _objectList.FindLast(match);
        }
        public int FindLastIndex(Predicate<C> match)
        {
            return _objectList.FindLastIndex(match);
        }
        public int FindLastIndex(int startIndex, Predicate<C> match)
        {
            return _objectList.FindLastIndex(startIndex, match);
        }
        public int FindLastIndex(int startIndex, int count, Predicate<C> match)
        {
            return _objectList.FindLastIndex(startIndex, count, match);
        }
        public C FindOnlyOne(Predicate<C> match)
        {
            return _objectList.FindOnlyOne(match);
        }
        public void ForConditionalItems(Predicate<C> match, Action<C> action)
        {
            _objectList.ForConditionalItems(match, action);
        }
        public Task ForConditionalItemsAsync(Predicate<C> match, ActionAsync<C> action)
        {
            return _objectList.ForConditionalItemsAsync(match, action);
        }
        public void ForEach(Action<C> action)
        {
            _objectList.ForEach(action);
        }
        public Task ForEachAsync(ActionAsync<C> action)
        {
            return _objectList.ForEachAsync(action);
        }
        public int GetDeckCount()
        {
            return _thisShuffle.GetDeckCount();
        }
        public IEnumerator<C> GetEnumerator()
        {
            return _objectList.GetEnumerator();
        }
        public Task<DeckRegularDict<C>> GetListFromJsonAsync(string jsonData)
        {
            return _thisShuffle.GetListFromJsonAsync(jsonData);
        }
        public C GetSpecificItem(int deck)
        {
            return _thisShuffle.GetSpecificItem(deck);
        }
        public int HowMany(Predicate<C> match)
        {
            return _objectList.HowMany(match);
        }
        public int IndexOf(C value)
        {
            return _objectList.IndexOf(value);
        }
        public int IndexOf(C value, int index)
        {
            return _objectList.IndexOf(value, index);
        }
        public int IndexOf(C value, int index, int count)
        {
            return _objectList.IndexOf(value, index, count);
        }
        public bool ObjectExist(int deck)
        {
            return _thisShuffle.ObjectExist(deck);
        }
        public void OrderedObjects()
        {
            _thisShuffle.OrderedObjects();
        }
        public void ReshuffleFirstObjects(IDeckDict<C> thisList, int startAt, int endAt)
        {
            _thisShuffle.ReshuffleFirstObjects(thisList, startAt, endAt);
        }
        public void ShuffleObjects()
        {
            _thisShuffle.ShuffleObjects();
        }
        public bool TrueForAll(Predicate<C> match)
        {
            return _objectList.TrueForAll(match);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _objectList.GetEnumerator();
        }
        public void RelinkObject(int oldDeck, C newObject)
        {
            _thisShuffle.RelinkObject(oldDeck, newObject);
        }

        public void PutCardOnTop(int deck)
        {
            _thisShuffle.PutCardOnTop(deck);
        }
    }
}