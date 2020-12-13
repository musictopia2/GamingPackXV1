using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.Messenging;
namespace DominosMexicanTrainCP.Data
{
    public class DominosMexicanTrainPlayerItem : PlayerSingleHand<MexicanDomino>, IHandle<UpdateCountEventModel>
    {
        private DeckRegularDict<MexicanDomino> _longestTrainList = new DeckRegularDict<MexicanDomino>();
        public DeckRegularDict<MexicanDomino> LongestTrainList
        {
            get { return _longestTrainList; }
            set
            {
                if (SetProperty(ref _longestTrainList, value))
                {
                    
                }
            }
        }
        private int _previousScore;
        public int PreviousScore
        {
            get { return _previousScore; }
            set
            {
                if (SetProperty(ref _previousScore, value))
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
        private int _previousLeft;
        public int PreviousLeft
        {
            get { return _previousLeft; }
            set
            {
                if (SetProperty(ref _previousLeft, value))
                {
                    
                }
            }
        }
        private int _tempCards;
        public override int ObjectCount => base.ObjectCount + _tempCards;
        public void Handle(UpdateCountEventModel message)
        {
            _tempCards = message.ObjectCount;
        }
    }
}