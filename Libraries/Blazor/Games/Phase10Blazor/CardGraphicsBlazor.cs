using BasicGameFrameworkLibrary.ColorCards;
using BasicGamingUIBlazorLibrary.GameGraphics.Cards;
using Phase10CP.Cards;
using Phase10CP.Data;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace Phase10Blazor
{
    public class CardGraphicsBlazor : BaseColorCardsImageBlazor<Phase10CardInformation>
    {
        protected override string BackColor => cc.Aqua;
        protected override string BackFontColor => cc.Purple;
        protected override string BackText => "Phase 10";
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.Color == EnumColorTypes.ZOther || DeckObject!.Color == EnumColorTypes.None)
            {
                return false;
            }
            return DeckObject!.CardCategory != EnumCardCategory.Blank;
        }
        protected override void DrawImage()
        {
            DrawValueCard(DefaultRectangle, DeckObject!.Display);
        }
    }
}
