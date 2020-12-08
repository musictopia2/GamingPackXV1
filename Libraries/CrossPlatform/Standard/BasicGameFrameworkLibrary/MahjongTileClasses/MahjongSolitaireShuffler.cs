using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MahjongTileClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BaseMahjongTilesCP
{
    public class MahjongShuffler : IDeckShuffler<MahjongSolitaireTileInfo>,
        IAdvancedDIContainer, ISimpleList<MahjongSolitaireTileInfo>, IDeckCount
    {
        public MahjongShuffler()
        {
            _thisShuffle = new BasicObjectShuffler<MahjongSolitaireTileInfo>(_objectList);
        }
        private readonly BasicObjectShuffler<MahjongSolitaireTileInfo> _thisShuffle;
        private readonly DeckRegularDict<MahjongSolitaireTileInfo> _objectList = new DeckRegularDict<MahjongSolitaireTileInfo>();
        public IGamePackageResolver? MainContainer { get => _thisShuffle.MainContainer; set => _thisShuffle.MainContainer = value; }
        public int Count => _objectList.Count;
        public bool NeedsToRedo { get => _thisShuffle.NeedsToRedo; set => _thisShuffle.NeedsToRedo = value; }
        public void AddRelinkedTiles(IEnumerable<MahjongSolitaireTileInfo> previousList)
        {
            _objectList.AddRange(previousList);
        }
        public MahjongSolitaireTileInfo GetIndexedTile(int index)
        {
            return _objectList[index];
        }
        public void ClearObjects()
        {
            _thisShuffle.ClearObjects();
        }
        public bool Contains(MahjongSolitaireTileInfo item)
        {
            return _objectList.Contains(item);
        }
        public bool Exists(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.Exists(match);
        }
        public MahjongSolitaireTileInfo Find(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.Find(match);
        }
        public ICustomBasicList<MahjongSolitaireTileInfo> FindAll(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindAll(match);
        }
        public int FindFirstIndex(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindFirstIndex(match);
        }
        public int FindFirstIndex(int startIndex, Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindFirstIndex(startIndex, match);
        }
        public int FindFirstIndex(int startIndex, int count, Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindFirstIndex(startIndex, count, match);
        }
        public MahjongSolitaireTileInfo FindLast(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindLast(match);
        }
        public int FindLastIndex(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindLastIndex(match);
        }
        public int FindLastIndex(int startIndex, Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindLastIndex(startIndex, match);
        }
        public int FindLastIndex(int startIndex, int count, Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindLastIndex(startIndex, count, match);
        }
        public MahjongSolitaireTileInfo FindOnlyOne(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.FindOnlyOne(match);
        }
        public void ForConditionalItems(Predicate<MahjongSolitaireTileInfo> match, Action<MahjongSolitaireTileInfo> action)
        {
            _objectList.ForConditionalItems(match, action);
        }
        public Task ForConditionalItemsAsync(Predicate<MahjongSolitaireTileInfo> match, ActionAsync<MahjongSolitaireTileInfo> action)
        {
            return _objectList.ForConditionalItemsAsync(match, action);
        }
        public void ForEach(Action<MahjongSolitaireTileInfo> action)
        {
            _objectList.ForEach(action);
        }
        public Task ForEachAsync(ActionAsync<MahjongSolitaireTileInfo> action)
        {
            return _objectList.ForEachAsync(action);
        }
        public int GetDeckCount()
        {
            return 144;
        }
        public IEnumerator<MahjongSolitaireTileInfo> GetEnumerator()
        {
            return _objectList.GetEnumerator();
        }
        public Task<DeckRegularDict<MahjongSolitaireTileInfo>> GetListFromJsonAsync(string JsonData)
        {
            return _thisShuffle.GetListFromJsonAsync(JsonData);
        }
        public MahjongSolitaireTileInfo GetSpecificItem(int Deck)
        {
            return _thisShuffle.GetSpecificItem(Deck);
        }
        public int HowMany(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.HowMany(match);
        }
        public int IndexOf(MahjongSolitaireTileInfo value)
        {
            return _objectList.IndexOf(value);
        }
        public int IndexOf(MahjongSolitaireTileInfo value, int index)
        {
            return _objectList.IndexOf(value, index);
        }
        public int IndexOf(MahjongSolitaireTileInfo value, int Index, int Count)
        {
            return _objectList.IndexOf(value, Index, Count);
        }
        public bool ObjectExist(int deck)
        {
            return _thisShuffle.ObjectExist(deck);
        }
        public void OrderedObjects()
        {
            _thisShuffle.OrderedObjects();
        }
        public void ReshuffleFirstObjects(IDeckDict<MahjongSolitaireTileInfo> thisList, int startAt, int endAt)
        {
            _thisShuffle.ReshuffleFirstObjects(thisList, startAt, endAt);
        }
        public void ShuffleObjects()
        {
            _thisShuffle.ShuffleObjects();
        }
        public bool TrueForAll(Predicate<MahjongSolitaireTileInfo> match)
        {
            return _objectList.TrueForAll(match);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _objectList.GetEnumerator();
        }

        public void PutCardOnTop(int deck)
        {
            _thisShuffle.PutCardOnTop(deck);
        }
    }
}