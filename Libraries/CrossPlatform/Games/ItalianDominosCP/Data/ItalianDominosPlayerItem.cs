using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace ItalianDominosCP.Data
{
    public class ItalianDominosPlayerItem : PlayerSingleHand<SimpleDominoInfo>
    {
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
        private bool _drewYet;
        public bool DrewYet
        {
            get { return _drewYet; }
            set
            {
                if (SetProperty(ref _drewYet, value))
                {
                    
                }
            }
        }
    }
}