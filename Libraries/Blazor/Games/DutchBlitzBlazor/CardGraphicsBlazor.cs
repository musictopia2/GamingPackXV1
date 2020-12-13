using BasicGameFrameworkLibrary.ColorCards;
using BasicGamingUIBlazorLibrary.GameGraphics.Cards;
using DutchBlitzCP.Cards;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace DutchBlitzBlazor
{
    public class CardGraphicsBlazor : BaseColorCardsImageBlazor<DutchBlitzCardInformation>
    {
        protected override string BackColor => cc.Aqua;
        protected override string BackFontColor => cc.DarkGreen;
        protected override string BackText => "Dutch Blitz";
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            return DeckObject!.Display != "" && DeckObject!.Color != EnumColorTypes.None;
        }
        protected override void DrawImage()
        {
            DrawValueCard(DefaultRectangle, DeckObject!.Display);
        }
    }
}