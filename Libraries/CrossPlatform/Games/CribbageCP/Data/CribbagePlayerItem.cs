using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace CribbageCP.Data
{
    public class CribbagePlayerItem : PlayerSingleHand<CribbageCard>
    {
        private int _scoreRound;
        public int ScoreRound
        {
            get { return _scoreRound; }
            set
            {
                if (SetProperty(ref _scoreRound, value))
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

        private bool _isSkunk = true; 
        public bool IsSkunk
        {
            get { return _isSkunk; }
            set
            {
                if (SetProperty(ref _isSkunk, value))
                {
                    
                }
            }
        }
        private bool _finishedLooking;
        public bool FinishedLooking
        {
            get { return _finishedLooking; }
            set
            {
                if (SetProperty(ref _finishedLooking, value))
                {
                    
                }
            }
        }
        private int _firstPosition;
        public int FirstPosition
        {
            get { return _firstPosition; }
            set
            {
                if (SetProperty(ref _firstPosition, value))
                {
                    
                }
            }
        }
        private int _secondPosition;
        public int SecondPosition
        {
            get { return _secondPosition; }
            set
            {
                if (SetProperty(ref _secondPosition, value))
                {
                    
                }
            }
        }
        private bool _choseCrib;
        public bool ChoseCrib
        {
            get { return _choseCrib; }
            set
            {
                if (SetProperty(ref _choseCrib, value))
                {
                    
                }
            }
        }
    }
}