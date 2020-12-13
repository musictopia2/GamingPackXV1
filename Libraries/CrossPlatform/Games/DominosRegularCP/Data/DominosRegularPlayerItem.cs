using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace DominosRegularCP.Data
{
    public class DominosRegularPlayerItem : PlayerSingleHand<SimpleDominoInfo>
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
        private bool _noPlay;
        public bool NoPlay
        {
            get { return _noPlay; }
            set
            {
                if (SetProperty(ref _noPlay, value))
                {
                    
                }
            }
        }
    }
}