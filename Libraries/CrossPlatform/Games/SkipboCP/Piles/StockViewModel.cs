using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.StockClasses;
using SkipboCP.Cards;
namespace SkipboCP.Piles
{
    public class StockViewModel : StockPileVM<SkipboCardInformation>
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