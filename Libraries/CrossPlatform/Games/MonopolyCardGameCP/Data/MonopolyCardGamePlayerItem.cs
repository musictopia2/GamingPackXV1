using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using MonopolyCardGameCP.Cards;
using MonopolyCardGameCP.Logic;
using Newtonsoft.Json;
namespace MonopolyCardGameCP.Data
{
    public class MonopolyCardGamePlayerItem : PlayerSingleHand<MonopolyCardGameCardInformation>
    {
        [JsonIgnore]
        public TradePile? TradePile;
        public string TradeString { get; set; } = "";
        private decimal _previousMoney;
        public decimal PreviousMoney
        {
            get { return _previousMoney; }
            set
            {
                if (SetProperty(ref _previousMoney, value))
                {
                    
                }
            }
        }
        private decimal _totalMoney;
        public decimal TotalMoney
        {
            get { return _totalMoney; }
            set
            {
                if (SetProperty(ref _totalMoney, value))
                {
                    
                }
            }
        }
    }
}