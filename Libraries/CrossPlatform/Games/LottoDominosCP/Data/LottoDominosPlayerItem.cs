using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace LottoDominosCP.Data
{
    public class LottoDominosPlayerItem : SimplePlayer
    {
        private int _numberChosen = -1;
        public int NumberChosen
        {
            get { return _numberChosen; }
            set
            {
                if (SetProperty(ref _numberChosen, value))
                {
                    
                }
            }
        }
        private int _numberWon;
        public int NumberWon
        {
            get { return _numberWon; }
            set
            {
                if (SetProperty(ref _numberWon, value))
                {
                    
                }
            }
        }
    }
}