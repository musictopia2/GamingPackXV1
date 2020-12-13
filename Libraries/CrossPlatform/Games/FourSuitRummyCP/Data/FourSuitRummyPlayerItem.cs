using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
using FourSuitRummyCP.Logic;
using Newtonsoft.Json;
namespace FourSuitRummyCP.Data
{
    public class FourSuitRummyPlayerItem : PlayerRummyHand<RegularRummyCard>
    {
        private int _currentScore;
        public int CurrentScore
        {
            get { return _currentScore; }
            set
            {
                if (SetProperty(ref _currentScore, value))
                {
                    
                }
            }
        }
        private int _totalScore;
        public int TotalScore
        {
            get { return _totalScore; }
            set
            {
                if (SetProperty(ref _totalScore, value))
                {
                    
                }
            }
        }
        public CustomBasicList<SavedSet> SetList { get; set; } = new CustomBasicList<SavedSet>();
        [JsonIgnore]
        public MainSets? MainSets;
    }
}