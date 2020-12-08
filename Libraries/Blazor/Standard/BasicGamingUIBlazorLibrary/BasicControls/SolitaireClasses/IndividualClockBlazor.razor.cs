using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.ClockClasses;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.SolitaireClasses
{
    public partial class IndividualClockBlazor : IDisposable, IHandle<CurrentCardEventModel>, IHandle<ClockInfo>
    {
        [CascadingParameter]
        public ClockObservable? DataContext { get; set; }
        [Parameter]
        public ClockInfo? SingleClock { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; }
        public void Dispose()
        {
            _aggregator!.Unsubscribe(this);
        }
        private SolitaireCard? _currentCard;
        private IEventAggregator? _aggregator;
        protected override void OnInitialized()
        {
            _aggregator = cons!.Resolve<IEventAggregator>();
            _aggregator.Subscribe(this);            
            base.OnInitialized();
        }
        private bool _didUpdate;
        protected override void OnParametersSet()
        {
            if (SingleClock != null && _didUpdate == false && DataContext!.CurrentClock > 0)
            {
                _didUpdate = true;
                UpdateClock(DataContext!.CurrentClock);
            }
            else if (SingleClock != null && _didUpdate == false && DataContext!.ShowCenter == false && SingleClock.CardList.Count > 0)
            {
                _didUpdate = true;
                UpdateClock(0); //try this too.
            }
        }
        private void LinkCard()
        {
            if (_currentCard == null)
            {
                return;
            }
            if (_currentCard.Deck == 0 && SingleClock!.CardList.Count > 1)
            {
                _currentCard.Deck = SingleClock.CardList.Last().Deck;
            }
            if (DataContext!.ShowCenter == true && DataContext!.ClockList.All(x => x.CardList.Count == 1))
            {
                _currentCard = null; //try this way now.
                return;
            }
            _currentCard.IsSelected = SingleClock!.IsSelected;
            _currentCard.IsEnabled = SingleClock.IsEnabled;
            if (DataContext!.CurrentCard != null && DataContext.CurrentCard.Deck == _currentCard.Deck)
            {
                _currentCard.IsUnknown = false;
            }
            else if (DataContext.ShowCenter && SingleClock.CardList.Count == 1)
            {
                _currentCard.Deck = 1000; //just so it can draw.  otherwise, can't even draw (not good).
                _currentCard.CardType = EnumRegularCardTypeList.Stop;
                _currentCard.IsUnknown = false;
            }
            else if (DataContext.ShowCenter)
            {
                _currentCard.IsUnknown = true;
            }
            else
            {
                _currentCard.IsUnknown = false;
            }
        }
        void IHandle<CurrentCardEventModel>.Handle(CurrentCardEventModel message)
        {
            if (SingleClock!.NumberGuide == message!.ThisClock!.NumberGuide)
            {
                _currentCard = new SolitaireCard();
                if (message.ThisCategory == EnumCardMessageCategory.Hidden)
                {
                    if (SingleClock.CardList.Count > 1)
                    {
                        _currentCard.Populate(SingleClock.CardList.Last().Deck);
                    }
                }
                else
                {
                    _currentCard.Populate(DataContext!.CurrentCard!.Deck);
                }
            }
        }
        private void UpdateClock(int current)
        {
            _currentCard = new SolitaireCard();
            if (current != SingleClock!.NumberGuide)
            {
                if (SingleClock!.CardList.Count > 1)
                {
                    _currentCard.Populate(SingleClock.CardList.Last().Deck);
                }
            }
            else if (DataContext!.ShowCenter == true)
            {
                _currentCard.Populate(DataContext!.CurrentCard!.Deck);
                _currentCard.IsUnknown = false;
            }
            else
            {
                _currentCard.Populate(SingleClock.CardList.Last().Deck); //try this way.
            }
        }
        void IHandle<ClockInfo>.Handle(ClockInfo message)
        {
            if (message.NumberGuide != SingleClock!.NumberGuide)
            {
                return;
            }
            UpdateClock(0);
        }
        
    }
}