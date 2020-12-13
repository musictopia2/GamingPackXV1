using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using GermanWhistCP.Cards;
using System.Drawing;
namespace GermanWhistBlazor
{
    //its there in case its needed.
    public class CardGraphicsBlazor : BaseDeckGraphics<GermanWhistCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(55, 72); //this is default but can change to anything you want.
        protected override bool NeedsToDrawBacks => true;

        protected override bool CanStartDrawing()
        {
            return true;
        }

        protected override void DrawBacks()
        {
            //any code necessary for drawing backs of cards.
        }

        protected override void DrawImage()
        {
            //code necessary to draw the card.

        }
    }
}
