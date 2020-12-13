using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGamingUIBlazorLibrary.GameGraphics.GamePieces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using System.Drawing;
namespace GoFishBlazor
{
    public class GoFishPieceBlazor : NumberPiece
    {
        //decided to not put to global since this is only for gofish.
        [Parameter]
        public EnumRegularCardValueList Value { get; set; }
        protected override void OriginalSizeProcesses()
        {
            MainGraphics!.OriginalSize = new SizeF(45, 45);
            base.OriginalSizeProcesses();
        }
        protected override string GetValueToPrint()
        {
            if (Value == EnumRegularCardValueList.Jack)
            {
                return "J";
            }
            if (Value == EnumRegularCardValueList.Queen)
            {
                return "Q";
            }
            if (Value == EnumRegularCardValueList.King)
            {
                return "K";
            }
            if (Value == EnumRegularCardValueList.None)
            {
                return "";
            }
            if (Value == EnumRegularCardValueList.LowAce || Value == EnumRegularCardValueList.HighAce)
            {
                return "A";
            }
            return Value.FromEnum().ToString();
        }
    }
}