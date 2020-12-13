using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.Exceptions;
namespace MonasteryCardGameCP.Data
{
    public class MonasteryCardInfo : RegularRummyCard
    {
        public int Temp { get; set; } //this is used so it can still populate properly.
        protected override void FinishPopulatingCard()
        {
            if (Deck == 0)
            {
                throw new BasicBlankException("Deck cannot be 0 when finish populating");
            }
            if (Temp == 0)
            {
                Temp = Deck; //if temp is set, then leave it.
            }
        }
        //public override bool AlwaysDifferent
        //{
        //    get
        //    {
        //        if (Value == EnumRegularCardValueList.HighAce || Value == EnumRegularCardValueList.LowAce)
        //        {
        //            System.Console.WriteLine("Different");
        //            return true;
        //        }
        //        System.Console.WriteLine("Same");
        //        return false;
        //    }
        //}
    }
}