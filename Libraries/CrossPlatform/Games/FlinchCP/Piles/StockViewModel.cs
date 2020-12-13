using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.StockClasses;
using FlinchCP.Cards;
namespace FlinchCP.Piles
{
    public class StockViewModel : StockPileVM<FlinchCardInformation>
    {
        public StockViewModel(CommandContainer command) : base(command) { }
        public override string NextCardInStock()
        {
            if (DidGoOut() == true)
            {
                return "0";
            }
            var thisCard = GetCard();
            return thisCard.Display;
        }
    }
}