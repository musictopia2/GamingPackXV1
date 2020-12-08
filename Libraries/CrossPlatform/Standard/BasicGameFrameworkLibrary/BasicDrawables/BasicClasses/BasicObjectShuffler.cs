using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.RandomGenerator;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
using static BasicGameFrameworkLibrary.DIContainers.Helpers;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace BasicGameFrameworkLibrary.BasicDrawables.BasicClasses
{
    public class BasicObjectShuffler<D> : IDeckShuffler<D>, IAdvancedDIContainer where D : IDeckObject, new()
    {
        private readonly IDeckDict<D> _privateDict;
        private IDeckCount? _deckCount; //maybe needed. to stop overflow exceptions.
        private RandomGenerator? _rs;
        public IGamePackageResolver? MainContainer { get; set; } //decided to be this way so it can be populated automatically.
        private readonly Action<D>? _beforePopulate;
        public bool NeedsToRedo { get; set; } //realized it had to be public so tile rummy can call into it.
        public void RelinkObject(int oldDeck, D newObject)
        {
            _privateDict.RemoveObjectByDeck(oldDeck);
            _privateDict.Add(newObject);
        }
        public void UnlinkObjects()
        {
            _privateDict.ForEach(items => items.Reset());
        }
        private void EnableObjects()
        {
            _privateDict.ForEach(items => items.IsEnabled = true);
        }
        private void FixObjects()
        {
            EnableObjects();
            _privateDict.ForEach(items =>
            {
                items.Drew = false;
                items.IsSelected = false;
                items.IsUnknown = false;
                items.Visible = true;
                items.IsEnabled = true;
                items.Reset();
            });
        }
        private bool RedoList()
        {
            if (_privateDict.Count == 0)
                return true;
            return NeedsToRedo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="privateDict"></param>
        /// <param name="beforePopulate">This is extra stuff that has to happen before populating item if any</param>
        public BasicObjectShuffler(IDeckDict<D> privateDict, Action<D>? beforePopulate = null)
        {
            _privateDict = privateDict;
            _beforePopulate = beforePopulate;
        }

        private INewCard<D>? _newCard;
        public void ClearObjects()
        {
            _privateDict.Clear();
        }
        public int GetDeckCount()
        {
            if (_deckCount == null)
            {
                PopulateContainer(this);
                _deckCount = MainContainer!.Resolve<IDeckCount>(); //i think
                if (MainContainer.RegistrationExist<INewCard<D>>())
                {
                    _newCard = MainContainer.Resolve<INewCard<D>>();
                }
            }
            return _deckCount.GetDeckCount();
        }
        public async Task<DeckRegularDict<D>> GetListFromJsonAsync(string jsonData)
        {
            bool doRedo = RedoList();
            CustomBasicList<int> thisList = await js.DeserializeObjectAsync<CustomBasicList<int>>(jsonData);
            CustomBasicList<D> tempList = new CustomBasicList<D>();
            if (doRedo == true)
            {
                thisList.ForEach(Items =>
                {
                    D thisD = new D();
                    _beforePopulate!.Invoke(thisD); //sometimes something has to be done before the card can be populated.
                    thisD.Populate(Items);
                    tempList.Add(thisD);
                });
                _privateDict.AddRange(tempList, System.Collections.Specialized.NotifyCollectionChangedAction.Add);
                return new DeckRegularDict<D>(tempList);
            }
            if (_privateDict.Count == thisList.Count)
            {
                FixObjects();
                DeckRegularDict<D> newTemp = new DeckRegularDict<D>(_privateDict);
                thisList.ForEach(Items =>
                {
                    tempList.Add(newTemp.GetSpecificItem(Items));
                });
                _privateDict.ReplaceRange(tempList);
                return new DeckRegularDict<D>(tempList);
            }
            UnlinkObjects();
            thisList.ForEach(items =>
            {
                D thisD = _privateDict.GetSpecificItem(items);
                thisD.Visible = true;
                thisD.IsEnabled = true;
                thisD.IsUnknown = false;
                thisD.IsSelected = false;
                thisD.Drew = false;
                tempList.Add(thisD);
            });
            return new DeckRegularDict<D>(tempList);
        }
        public D GetSpecificItem(int deck)
        {
            return _privateDict.GetSpecificItem(deck);
        }
        public void OrderedObjects()
        {
            PrivatePopulate();
        }
        private D GetItem(int chosen)
        {
            if (_newCard == null)
            {
                return new D();
            }
            return _newCard.GetNewCard(chosen); //async should not be required here. this interface is useful for games like fluxx.
        }
        private void PrivatePopulate()
        {
            int maxs = GetDeckCount();
            for (int i = 1; i <= maxs; i++)
            {
                D thisD = GetItem(i);
                if (_beforePopulate != null)
                {
                    _beforePopulate.Invoke(thisD);
                }
                thisD.Populate(i);
                _privateDict.Add(thisD);
            }
        }
        public void ShuffleObjects()
        {
            CheckRandom(); //because the dictionary needs it.
            bool redo = RedoList();
            if (redo == false)
            {
                FixObjects();
                _privateDict.ShuffleList();
                return;
            }
            PrivatePopulate();
            _privateDict.ShuffleList();
        }
        private void CheckRandom()
        {
            if (_rs != null)
            {
                return;
            }
            PopulateContainer(this);
            _rs = MainContainer!.Resolve<RandomGenerator>();
            _privateDict.MainContainer = MainContainer;
        }
        public void ReshuffleFirstObjects(IDeckDict<D> thisList, int startAt, int endAt)
        {
            CheckRandom();
            int x = 0;
            int index;
            int ask1;
            endAt = endAt - thisList.Count + 1;
            int increasedEnd = 0;
            thisList.ForEach(items =>
            {
                index = _privateDict.IndexOf(items);
                if (index <= endAt)
                {
                    increasedEnd++;
                }
            });
            endAt += increasedEnd;
            thisList.ForEach(items =>
            {
                index = _privateDict.IndexOf(items);
                if (index == -1)
                {
                    throw new BasicBlankException("Item not found to reshuffle the card");
                }
                if (index < startAt || index > endAt)
                {
                    ask1 = _rs!.GetRandomNumber(endAt, startAt);
                    _privateDict.MoveItem(items, ask1);
                }
                x++;
                endAt--;
            });
        }
        public bool ObjectExist(int deck)
        {
            return _privateDict.ObjectExist(deck);
        }

        public void PutCardOnTop(int deck)
        {
            var card = _privateDict.GetSpecificItem(deck);
            _privateDict.RemoveObjectByDeck(deck);
            _privateDict.InsertBeginning(card);
        }
    }
}