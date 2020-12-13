using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace SinisterSixCP.Data
{
    public class SinisterSixPlayerItem : SimplePlayer
    {
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
    }
}