using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using FluxxCP.Cards;
using FluxxCP.UICP;
using System.Threading.Tasks;
namespace FluxxCP.Containers
{
    [SingletonGame]
    [AutoReset]
    public class FluxxVMData : ObservableObject, IBasicCardGamesData<FluxxCardInformation>
    {
        public FluxxVMData(CommandContainer command, TestOptions test, IAsyncDelayer delayer)
        {
            Deck1 = new DeckObservablePile<FluxxCardInformation>(command);
            Pile1 = new SingleObservablePile<FluxxCardInformation>(command);
            PlayerHand1 = new HandObservable<FluxxCardInformation>(command);
            Keeper1 = new HandObservable<KeeperCard>(command);
            Goal1 = new HandObservable<GoalCard>(command);
            Goal1.Text = "Goal Cards";
            Goal1.Maximum = 3;
            Goal1.AutoSelect = EnumHandAutoType.SelectOneOnly;
            Keeper1.AutoSelect = EnumHandAutoType.SelectAsMany;
            Keeper1.Text = "Your Keepers";
            CardDetail = new DetailCardObservable();
            _test = test;
            _delayer = delayer;
        }
        public DetailCardObservable CardDetail;
        public HandObservable<KeeperCard> Keeper1;
        public HandObservable<GoalCard> Goal1;
        public DeckObservablePile<FluxxCardInformation> Deck1 { get; set; }
        public SingleObservablePile<FluxxCardInformation> Pile1 { get; set; }
        public HandObservable<FluxxCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<FluxxCardInformation>? OtherPile { get; set; }
        private string _normalTurn = "";
        [VM]
        public string NormalTurn
        {
            get { return _normalTurn; }
            set
            {
                if (SetProperty(ref _normalTurn, value))
                {
                    
                }
            }
        }

        private string _status = "";
        [VM] //use this tag to transfer to the actual view model.  this is being done to avoid overflow errors.
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value))
                {
                    
                }
            }
        }
        private int _playsLeft;
        [VM]
        public int PlaysLeft
        {
            get
            {
                return _playsLeft;
            }
            set
            {
                if (SetProperty(ref _playsLeft, value) == true)
                {
                }
            }
        }
        private int _handLimit;
        [VM]
        public int HandLimit
        {
            get
            {
                return _handLimit;
            }
            set
            {
                if (SetProperty(ref _handLimit, value) == true)
                {
                }
            }
        }
        private int _keeperLimit;
        [VM]
        public int KeeperLimit
        {
            get
            {
                return _keeperLimit;
            }
            set
            {
                if (SetProperty(ref _keeperLimit, value) == true)
                {
                }
            }
        }
        private int _playLimit;
        [VM]
        public int PlayLimit
        {
            get
            {
                return _playLimit;
            }
            set
            {
                if (SetProperty(ref _playLimit, value) == true)
                {
                }
            }
        }
        private bool _anotherTurn;
        [VM]
        public bool AnotherTurn
        {
            get
            {
                return _anotherTurn;
            }
            set
            {
                if (SetProperty(ref _anotherTurn, value) == true)
                {
                }
            }
        }
        private int _drawBonus;
        [VM]
        public int DrawBonus
        {
            get
            {
                return _drawBonus;
            }
            set
            {
                if (SetProperty(ref _drawBonus, value) == true)
                {
                }
            }
        }
        private int _playBonus;
        [VM]
        public int PlayBonus
        {
            get
            {
                return _playBonus;
            }
            set
            {
                if (SetProperty(ref _playBonus, value) == true)
                {
                }
            }
        }
        private int _cardsDrawn;
        [VM]
        public int CardsDrawn
        {
            get
            {
                return _cardsDrawn;
            }
            set
            {
                if (SetProperty(ref _cardsDrawn, value) == true)
                {
                }
            }
        }
        private int _drawRules;
        [VM]
        public int DrawRules
        {
            get
            {
                return _drawRules;
            }
            set
            {
                if (SetProperty(ref _drawRules, value) == true)
                {
                }
            }
        }
        private int _previousBonus;
        [VM]
        public int PreviousBonus
        {
            get
            {
                return _previousBonus;
            }
            set
            {
                if (SetProperty(ref _previousBonus, value) == true)
                {
                }
            }
        }
        private int _cardsPlayed;
        [VM]
        public int CardsPlayed
        {
            get
            {
                return _cardsPlayed;
            }
            set
            {
                if (SetProperty(ref _cardsPlayed, value) == true)
                {
                }
            }
        }
        private string _otherTurn = "";
        private readonly TestOptions _test;
        private readonly IAsyncDelayer _delayer;
        [VM]
        public string OtherTurn
        {
            get { return _otherTurn; }
            set
            {
                if (SetProperty(ref _otherTurn, value))
                {
                    
                }
            }
        }
        public async Task ShowPlayCardAsync(FluxxCardInformation card)
        {
            if (card.Deck != CardDetail!.CurrentCard.Deck)
            {
                CardDetail.ShowCard(card);
                if (_test.NoAnimations == false)
                {
                    await _delayer.DelaySeconds(1);
                }
            }
            CardDetail.ResetCard();
        }
        internal void UnselectAllCards()
        {
            PlayerHand1!.UnselectAllObjects();
            Keeper1!.UnselectAllObjects();
            Goal1!.UnselectAllObjects();
        }
    }
}