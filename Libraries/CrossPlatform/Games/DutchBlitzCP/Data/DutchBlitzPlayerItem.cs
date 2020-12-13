using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using DutchBlitzCP.Cards;
namespace DutchBlitzCP.Data
{
    public class DutchBlitzPlayerItem : PlayerSingleHand<DutchBlitzCardInformation>
    {
        private int _stockLeft;
        public int StockLeft
        {
            get { return _stockLeft; }
            set
            {
                if (SetProperty(ref _stockLeft, value))
                {
                    
                }
            }
        }
        private int _pointsRound;
        public int PointsRound
        {
            get { return _pointsRound; }
            set
            {
                if (SetProperty(ref _pointsRound, value))
                {
                    
                }
            }
        }
        private int _pointsGame;
        public int PointsGame
        {
            get { return _pointsGame; }
            set
            {
                if (SetProperty(ref _pointsGame, value))
                {
                    
                }
            }
        }
    }
}