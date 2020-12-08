using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public class RegularCardsBasicShuffler<R> : IDeckShuffler<R>,
         IAdvancedDIContainer, ISimpleList<R>, IListShuffler<R> where R : IRegularCard, new()
    {
        private readonly BasicObjectShuffler<R> _thisShuffle;
        private readonly DeckRegularDict<R> _objectList = new DeckRegularDict<R>();
        public int Count => _objectList.Count;
        public IGamePackageResolver? MainContainer { get => _thisShuffle.MainContainer; set => _thisShuffle.MainContainer = value; }
        public bool NeedsToRedo { get => _thisShuffle.NeedsToRedo; set => _thisShuffle.NeedsToRedo = value; }
        public RegularCardsBasicShuffler()
        {
            _thisShuffle = new BasicObjectShuffler<R>(_objectList,
                Items => Items.MainContainer = MainContainer);
        }
        public void ClearObjects()
        {
            _thisShuffle.ClearObjects();
        }
        public int GetDeckCount()
        {
            return _thisShuffle.GetDeckCount();
        }
        public Task<DeckRegularDict<R>> GetListFromJsonAsync(string jsonData)
        {
            return _thisShuffle.GetListFromJsonAsync(jsonData);
        }
        public R GetSpecificItem(int deck)
        {
            return _thisShuffle.GetSpecificItem(deck);
        }
        public void OrderedObjects()
        {
            _thisShuffle.OrderedObjects();
        }
        public void ReshuffleFirstObjects(IDeckDict<R> thisList, int startAt, int endAt)
        {
            _thisShuffle.ReshuffleFirstObjects(thisList, startAt, endAt);
        }
        public void ShuffleObjects()
        {
            _thisShuffle.ShuffleObjects();
        }
        public bool ObjectExist(int deck)
        {
            return _thisShuffle.ObjectExist(deck);
        }
        public Task ForEachAsync(ActionAsync<R> action)
        {
            return _objectList.ForEachAsync(action);
        }
        public void ForEach(Action<R> action)
        {
            _objectList.ForEach(action);
        }
        public void ForConditionalItems(Predicate<R> match, Action<R> action)
        {
            _objectList.ForConditionalItems(match, action);
        }
        public Task ForConditionalItemsAsync(Predicate<R> match, ActionAsync<R> action)
        {
            return _objectList.ForConditionalItemsAsync(match, action);
        }
        public bool Exists(Predicate<R> match)
        {
            return _objectList.Exists(match);
        }
        public bool Contains(R item)
        {
            return _objectList.Contains(item);
        }
        public R Find(Predicate<R> match)
        {
            return _objectList.Find(match);
        }
        public R FindOnlyOne(Predicate<R> match)
        {
            return _objectList.FindOnlyOne(match);
        }
        public ICustomBasicList<R> FindAll(Predicate<R> match)
        {
            return _objectList.FindAll(match);
        }
        public int FindFirstIndex(Predicate<R> match)
        {
            return _objectList.FindFirstIndex(match);
        }
        public int FindFirstIndex(int startIndex, Predicate<R> match)
        {
            return _objectList.FindFirstIndex(startIndex, match);
        }
        public int FindFirstIndex(int startIndex, int count, Predicate<R> match)
        {
            return _objectList.FindFirstIndex(startIndex, count, match);
        }
        public R FindLast(Predicate<R> match)
        {
            return _objectList.FindLast(match);
        }
        public int FindLastIndex(Predicate<R> match)
        {
            return _objectList.FindLastIndex(match);
        }
        public int FindLastIndex(int startIndex, Predicate<R> match)
        {
            return _objectList.FindLastIndex(startIndex, match);
        }
        public int FindLastIndex(int startIndex, int count, Predicate<R> match)
        {
            return _objectList.FindLastIndex(startIndex, count, match);
        }
        public int HowMany(Predicate<R> match)
        {
            return _objectList.HowMany(match);
        }
        public int IndexOf(R value)
        {
            return _objectList.IndexOf(value);
        }
        public int IndexOf(R value, int index)
        {
            return _objectList.IndexOf(value, index);
        }
        public int IndexOf(R value, int index, int count)
        {
            return _objectList.IndexOf(value, index, count);
        }
        public bool TrueForAll(Predicate<R> match)
        {
            return _objectList.TrueForAll(match);
        }
        public IEnumerator<R> GetEnumerator()
        {
            return _objectList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _objectList.GetEnumerator();
        }
        public void RelinkObject(int oldDeck, R newObject)
        {
            _thisShuffle.RelinkObject(oldDeck, newObject);
        }

        public void PutCardOnTop(int deck)
        {
            _thisShuffle.PutCardOnTop(deck);
        }
    }
}