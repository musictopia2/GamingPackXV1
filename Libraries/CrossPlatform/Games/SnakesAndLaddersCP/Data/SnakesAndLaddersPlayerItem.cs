using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace SnakesAndLaddersCP.Data
{
    public class SnakesAndLaddersPlayerItem : SimplePlayer
    {
        private int _spaceNumber;
        public int SpaceNumber
        {
            get { return _spaceNumber; }
            set
            {
                if (SetProperty(ref _spaceNumber, value))
                {
                    
                }
            }
        }
    }
}