using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace YachtRaceCP.Data
{
    public class YachtRacePlayerItem : SimplePlayer
    {
        private float _time;
        public float Time
        {
            get { return _time; }
            set
            {
                if (SetProperty(ref _time, value))
                {
                    
                }
            }
        }
    }
}