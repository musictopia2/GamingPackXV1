using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using Pinochle2PlayerCP.Cards;
namespace Pinochle2PlayerCP.Data
{
    public class Pinochle2PlayerPlayerItem : PlayerTrick<EnumSuitList, Pinochle2PlayerCardInformation>
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
        public DeckRegularDict<Pinochle2PlayerCardInformation> AdditionalCards { get; set; } = new DeckRegularDict<Pinochle2PlayerCardInformation>();
        public override int ObjectCount => MainHandList.Count + _tempCards;
        private int _tempCards;
        public void Handle(UpdateCountEventModel message)
        {
            _tempCards = message.ObjectCount;
        }
    }
}