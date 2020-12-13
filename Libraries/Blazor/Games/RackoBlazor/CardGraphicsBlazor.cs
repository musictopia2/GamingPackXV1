using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using RackoCP.Cards;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
namespace RackoBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<RackoCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(200, 35);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            return DeckObject.Value > 0;
        }
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.Red;
            }
            else
            {
                FillColor = cc.White;
            }
            base.BeforeFilling();
        }
        protected override void DrawBacks()
        {
            RectangleF rect = new RectangleF(2, 2, 200, 30);
            Text text = new Text();
            text.CenterText(MainGroup!, rect);
            text.Content = "Racko";
            text.Font_Size = 30;
        }
        private int _maxs;
        protected override void OnInitialized()
        {
            RackoDeckCount temps = Resolve<RackoDeckCount>();
            _maxs = temps.GetDeckCount();
            base.OnInitialized();
        }
        protected override void DrawImage()
        {
            var maxSize = 40;
            double percs = (DeckObject!.Value) / (double)_maxs;
            var maxLeft = DefaultSize.Width - maxSize;
            var lefts = maxLeft * percs;
            lefts += 5;
            float fontSize = 30;
            Text text = new Text();
            text.X = lefts.ToString();
            text.Height = "100%";
            text.Y = "50%";
            text.Dominant_Baseline = "middle";
            text.Text_Anchor = "middle";
            MainGroup!.Children.Add(text);
            text.Font_Size = fontSize;
            text.Fill = cc.Red.ToWebColor();
            text.Content = DeckObject!.Value.ToString();
        }
    }
}