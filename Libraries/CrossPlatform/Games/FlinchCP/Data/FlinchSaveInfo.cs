using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using FlinchCP.Cards;
namespace FlinchCP.Data
{
    [SingletonGame]
    public class FlinchSaveInfo : BasicSavedCardClass<FlinchPlayerItem, FlinchCardInformation>
    {
        public CustomBasicList<BasicPileInfo<FlinchCardInformation>> PublicPileList { get; set; } = new CustomBasicList<BasicPileInfo<FlinchCardInformation>>();

        private int _cardsToShuffle;
        public int CardsToShuffle
        {
            get { return _cardsToShuffle; }
            set
            {
                if (SetProperty(ref _cardsToShuffle, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    _model.CardsToShuffle = value;
                }
            }
        }
        public int PlayerFound { get; set; }
        public EnumStatusList GameStatus { get; set; }
        public void LoadMod(FlinchVMData model)
        {
            _model = model;
            _model.CardsToShuffle = CardsToShuffle;
        }
        private FlinchVMData? _model;
    }
}