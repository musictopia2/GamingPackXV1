using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using System.Collections.Generic;
namespace BowlingDiceGameCP.Data
{
    public class BowlingDiceGamePlayerItem : SimplePlayer
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
        public Dictionary<int, FrameInfoCP> FrameList { get; set; } = new Dictionary<int, FrameInfoCP>();
    }
}