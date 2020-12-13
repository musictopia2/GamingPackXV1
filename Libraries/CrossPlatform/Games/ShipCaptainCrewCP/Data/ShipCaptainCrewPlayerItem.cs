using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace ShipCaptainCrewCP.Data
{
    public class ShipCaptainCrewPlayerItem : SimplePlayer
    {
        private bool _wentOut;
        public bool WentOut
        {
            get { return _wentOut; }
            set
            {
                if (SetProperty(ref _wentOut, value))
                {
                    
                }
            }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    
                }
            }
        }
        private int _wins;
        public int Wins
        {
            get { return _wins; }
            set
            {
                if (SetProperty(ref _wins, value))
                {
                    
                }
            }
        }
        public bool TookTurn { get; set; }
    }
}