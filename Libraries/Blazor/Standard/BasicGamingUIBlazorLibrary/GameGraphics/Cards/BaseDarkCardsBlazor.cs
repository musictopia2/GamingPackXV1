using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Cards
{
    public abstract class BaseDarkCardsBlazor<D> : BaseDeckGraphics<D>
        where D : class, IDeckObject, new()
    {
        //try to make the color cards inherit from this as well.
        protected override string SelectFillColor => cc.Black.ToWebColor();
        protected override string DrawFillColor => cc.White.ToWebColor();
        protected abstract bool IsLightColored { get; }
        protected override string GetOpacity
        {
            get
            {
                if (IsLightColored && DeckObject!.IsSelected == false)
                {
                    return ".75";
                }
                return GetDarkHighlighter().ToString();
            }
        }
        //well see if i decide to have a class to help with back of the cards (utilities).

    }
}
