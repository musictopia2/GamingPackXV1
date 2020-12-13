using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
using SixtySix2PlayerCP.Cards;
namespace SixtySix2PlayerCP.Data
{
    [SingletonGame]
    public class SixtySix2PlayerSaveInfo : BasicSavedTrickGamesClass<EnumSuitList, SixtySix2PlayerCardInformation, SixtySix2PlayerPlayerItem>
    {
        public DeckRegularDict<SixtySix2PlayerCardInformation> CardList { get; set; } = new DeckRegularDict<SixtySix2PlayerCardInformation>();
        public CustomBasicList<int> CardsForMarriage { get; set; } = new CustomBasicList<int>();
        public int LastTrickWon { get; set; }
        private SixtySix2PlayerVMData? _model;
        public void LoadMod(SixtySix2PlayerVMData model)
        {
            _model = model;
            _model.BonusPoints = BonusPoints;
        }
        private int _bonusPoints;
        public int BonusPoints
        {
            get { return _bonusPoints; }
            set
            {
                if (SetProperty(ref _bonusPoints, value))
                {
                    if (_model != null)
                    {
                        _model.BonusPoints = value;
                    }
                }
            }
        }
    }
}