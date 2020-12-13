using BasicGameFrameworkLibrary.ColorCards;
using BasicGamingUIBlazorLibrary.GameGraphics.Cards;
using SkipboCP.Cards;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace SkipboBlazor
{
    public class CardGraphicsBlazor : BaseColorCardsImageBlazor<SkipboCardInformation>
    {
        protected override string BackColor => cc.Red;
        protected override string BackFontColor => cc.BlanchedAlmond;
        protected override string BackText => "Skip Bo";
        protected override bool CanStartDrawing()
        {
            return DeckObject!.Display != "" && DeckObject.Color != EnumColorTypes.None;
        }
        protected override void DrawImage()
        {
            DrawValueCard(DefaultRectangle, DeckObject!.Display);
        }
    }
}