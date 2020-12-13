using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using HuseHeartsCP.Cards;
namespace HuseHeartsCP.Data
{
    [SingletonGame]
    public class HuseHeartsSaveInfo : BasicSavedTrickGamesClass<EnumSuitList, HuseHeartsCardInformation, HuseHeartsPlayerItem>, ITrickStatusSavedClass
    {
        public DeckRegularDict<HuseHeartsCardInformation> BlindList { get; set; } = new DeckRegularDict<HuseHeartsCardInformation>();
        public DeckRegularDict<HuseHeartsCardInformation> DummyList { get; set; } = new DeckRegularDict<HuseHeartsCardInformation>();
        public EnumTrickStatus TrickStatus { get; set; }
        public int WhoLeadsTrick { get; set; }
        public int WhoWinsBlind { get; set; }
        private int _roundNumber;
        public int RoundNumber
        {
            get { return _roundNumber; }
            set
            {
                if (SetProperty(ref _roundNumber, value))
                {
                    if (_model != null)
                    {
                        _model.RoundNumber = value;
                    }
                }
            }
        }
        private EnumStatus _gameStatus;
        public EnumStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    ChangeHand();
                }
            }
        }
        private void ChangeHand()
        {
            if (GameStatus == EnumStatus.Passing)
            {
                _model!.PlayerHand1.AutoSelect = EnumHandAutoType.SelectAsMany;
            }
            else
            {
                _model!.PlayerHand1.AutoSelect = EnumHandAutoType.SelectOneOnly;
            }
            _model.GameStatus = GameStatus;
        }

        private HuseHeartsVMData? _model;
        public void LoadMod(HuseHeartsVMData model)
        {
            _model = model;
            _model.RoundNumber = RoundNumber;
            ChangeHand();
        }
    }
}