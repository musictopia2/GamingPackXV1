using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
using System.Linq; //sometimes i do use linq.
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses
{
    public class TempSetsObservable<S, C, R> : ObservableObject
        where S : Enum
        where C : Enum
        where R : IDeckObject, IRummmyObject<S, C>, new()
    {
        public event SetClickedEventHandler? SetClickedAsync;
        public delegate Task SetClickedEventHandler(int index);
        public int Spacing { get; set; }
        public int HowManySets { get; set; } = 5; // defaults at 5
        public CustomBasicList<RummyHandObservable<S, C, R>> SetList = new CustomBasicList<RummyHandObservable<S, C, R>>(); //has to be public because of data binding
        public DeckRegularDict<R> ObjectList(int index)
        {
            return SetList[index - 1].HandList; //i send in one based.
        }
        public TempSetsObservable(CommandContainer command, IGamePackageResolver resolver)
        {
            _command = command;
            _resolver = resolver;
        }
        private EventAggregator? _thisE;
        private readonly CommandContainer _command;
        private readonly IGamePackageResolver _resolver;
        public void ReportCanExecuteChange()
        {
            SetList.ForEach(x => x.ReportCanExecuteChange());
        }
        public void Init(IEnableAlways enables)
        {
            if (SetList.Count > 0)
            {
                return; //try this way.
            }
            _thisE = _resolver.Resolve<EventAggregator>();
            int x;
            var loopTo = HowManySets;
            RummyHandObservable<S, C, R> thisSet;
            for (x = 1; x <= loopTo; x++)
            {
                thisSet = new RummyHandObservable<S, C, R>(_command, _resolver);
                thisSet.AutoSelect = EnumHandAutoType.None; // somehow doing selectasmany is not working.  this means if its select all, then will be done on the event.
                thisSet.SendAlwaysEnable(enables); //i think.
                thisSet.Text = "Set"; // will always be set on this one.
                thisSet.SetClickedAsync += ThisSet_SetClickedAsync;
                SetList.Add(thisSet);
            }
        }
        private async Task ThisSet_SetClickedAsync(RummyHandObservable<S, C, R> thisSet)
        {
            if (SetClickedAsync == null)
            {
                return;
            }
            if (thisSet.DidClickObject == true)
            {
                thisSet.DidClickObject = false;
                return;
            }
            await SetClickedAsync.Invoke(SetList.IndexOf(thisSet) + 1); //wanted to make it one based.
        }
        public void ResetCards()
        {
            SetList.ForEach(Items => Items.DidClickObject = false);
        }
        public DeckRegularDict<R> ListAllObjects()
        {
            DeckRegularDict<R> output = new DeckRegularDict<R>();
            SetList.ForEach(thisTemp =>
            {
                output.AddRange(thisTemp.HandList);
            });
            return output;
        }
        public void UnselectAllCards()
        {
            SetList.ForEach(items => items.UnselectAllObjects());
        }
        public void EndTurn()
        {
            SetList.ForEach(items => items.EndTurn());
        }
        public void ClearBoard()
        {
            SetList.ForEach(items => items.ClearHand());
            PublicCount();
        }
        public void ClearBoard(int index)
        {
            SetList[index - 1].ClearHand(); //sending in one based.
            PublicCount();
        }
        public void PublicCount()
        {
            UpdateCountEventModel thisU = new UpdateCountEventModel();
            if (_thisE == null)
            {
                return;
            }
            thisU.ObjectCount = TotalObjects;
            if (_thisE!.HandlerExistsFor(typeof(UpdateCountEventModel)) == false)
            {
                return;
            }

            _thisE!.Publish(thisU);
        }
        public int TotalObjects => SetList.Sum(Items => Items.HandList.Count);
        public int HowManySelectedObjects => SetList.Sum(Items => Items.HowManySelectedObjects);
        public bool HasSelectedObject => SetList.Any(Items => Items.HowManySelectedObjects > 0);
        public int PileForSelectedObject
        {
            get
            {
                RummyHandObservable<S, C, R> thisVM = SetList.FirstOrDefault(Items => Items.HowManySelectedObjects > 0);
                if (thisVM == null)
                {
                    throw new BasicBlankException("There was no pile with only one selected card.  Find out what happened");
                }
                return SetList.IndexOf(thisVM) + 1; //returning 1 based.
            }
        }
        public int DeckForSelectedObjected(int pile)
        {
            return SetList[pile - 1].ObjectSelected();
        }
        public DeckRegularDict<R> ListObjectsRemoved()
        {
            var output = ListAllObjects();
            ClearBoard();
            return output;
        }
        public bool HasObject(int deck)
        {
            var thisList = ListAllObjects();
            return thisList.ObjectExist(deck);
        }
        public void RemoveObject(int deck)
        {
            foreach (var thisSet in SetList)
                if (thisSet.HandList.ObjectExist(deck))
                {
                    thisSet.HandList.RemoveObjectByDeck(deck);
                    PublicCount(); //because it changed now.
                    return;
                }
            throw new BasicBlankException($"There is no card with the deck {deck} to remove for tempsets");
        }
        public DeckRegularDict<R> ListSelectedObjects(bool alsoRemove = false)
        {
            DeckRegularDict<R> output = new DeckRegularDict<R>();
            SetList.ForEach(thisTemp =>
            {
                output.AddRange(thisTemp.ListSelectedObjects(alsoRemove));
            });
            if (alsoRemove == true)
            {
                PublicCount();
            }
            return output;
        }
        public DeckRegularDict<R> SelectedObjectsRemoved()
        {
            return ListSelectedObjects(true);
        }
        public R GetSelectedObject => ListSelectedObjects().Single();
        public void AddCards(int whichOne, IDeckDict<R> cardList)
        {
            var tempList = SelectedObjectsRemoved();
            cardList.AddRange(tempList);
            if (cardList.Count == 0)
            {
                return;
            }
            SetList[whichOne - 1].AddCards(cardList);
            PublicCount();
        }
    }
}