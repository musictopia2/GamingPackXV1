using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
using SixtySix2PlayerCP.Cards;
namespace SixtySix2PlayerCP.Data
{
    public class SixtySix2PlayerPlayerItem : PlayerTrick<EnumSuitList, SixtySix2PlayerCardInformation>
    {
        private int _scoreRound;
        public int ScoreRound
        {
            get { return _scoreRound; }
            set
            {
                if (SetProperty(ref _scoreRound, value))
                {
                    
                }
            }
        }
        private int _gamePointsRound;
        public int GamePointsRound
        {
            get { return _gamePointsRound; }
            set
            {
                if (SetProperty(ref _gamePointsRound, value))
                {
                    
                }
            }
        }
        private int _gamePointsGame;
        public int GamePointsGame
        {
            get { return _gamePointsGame; }
            set
            {
                if (SetProperty(ref _gamePointsGame, value))
                {
                    
                }
            }
        }
        public EnumMarriage FirstMarriage { get; set; }
        public CustomBasicList<EnumMarriage> MarriageList { get; set; } = new CustomBasicList<EnumMarriage>();
    }
}