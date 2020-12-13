using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using Newtonsoft.Json;
using TeeItUpCP.Cards;
using TeeItUpCP.Logic;
namespace TeeItUpCP.Data
{
    public class TeeItUpPlayerItem : PlayerSingleHand<TeeItUpCardInformation>
    {
        [JsonIgnore]
        public TeeItUpPlayerBoardCP? PlayerBoard;
        public bool FinishedChoosing { get; set; }
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
        public void LoadPlayerBoard(TeeItUpGameContainer gameContainer)
        {
            PlayerBoard = new TeeItUpPlayerBoardCP(gameContainer);
            PlayerBoard.LoadBoard(this);
        }
    }
}