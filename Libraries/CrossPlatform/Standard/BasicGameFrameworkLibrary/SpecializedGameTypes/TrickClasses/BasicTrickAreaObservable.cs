using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses
{
    public abstract class BasicTrickAreaObservable<S, T> : SimpleControlObservable
        where S : Enum
        where T : class, ITrickCard<S>, new()
    {
        private readonly IEventAggregator _aggregator;
        public ControlCommand CardSingleClickCommand { get; set; }
        protected async Task PrivateCardClickedAsync(T card)
        {
            if (card == null)
            {
                throw new BasicBlankException("Card can't be Nothing");
            }
            if (CardList.IndexOf(card) == -1)
            {
                throw new BasicBlankException("Can't be -1 for index");
            }
            await ProcessCardClickAsync(card);
        }
        protected abstract Task ProcessCardClickAsync(T thisCard);
        public int MaxPlayers { get; set; }
        protected async Task AnimateWinAsync()
        {
            if (WinCard == null)
            {
                throw new BasicBlankException("Can't animate win because no card sent");
            }
            await _aggregator.PublishAsync(new AnimateTrickEventModel()); //hopefully this simple this time.
        }
        protected override void EnableChange()
        {
            CardSingleClickCommand.ReportCanExecuteChange();
        }
        public BasicTrickAreaObservable(CommandContainer container, IEventAggregator aggregator) : base(container) //this still needs the ieventaggregator for trick animations.
        {
            _aggregator = aggregator;
            MethodInfo method = this.GetPrivateMethod(nameof(PrivateCardClickedAsync));
            CardSingleClickCommand = new ControlCommand(this, method, container);

        }
        public DeckRegularDict<T> CardList = new DeckRegularDict<T>();
        public T? WinCard;
        protected override void PrivateEnableAlways() { }

        public void TradeCard(int index, T newCard)
        {
            PointF oldLocation;
            oldLocation = CardList[index].Location;
            newCard.Location = oldLocation;
            newCard.IsSelected = false;
            newCard.Drew = false;
            var oldCard = CardList[index];
            CardList.ReplaceItem(oldCard, newCard); // hopefully that simple.
        }

        private bool _visible; //looks like for this one, i need the visible still.
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (SetProperty(ref _visible, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
        public void HideCards()
        {
            CardList.ForEach(x => x.Visible = false);
        }
    }
}