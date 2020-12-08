using BasicGameFrameworkLibrary.ColorCards;
using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Cards
{
    public abstract class BaseColorCardsImageBlazor<C> : BaseDarkCardsBlazor<C>
       where C : class, IColorCard, new()
    {
        protected override bool IsLightColored => DeckObject!.Color == EnumColorTypes.Yellow;
        protected RectangleF DefaultRectangle => new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height);
        protected abstract string BackColor { get; }
        protected abstract string BackFontColor { get; }
        protected abstract string BackText { get; }
        protected override SizeF DefaultSize => new SizeF(55, 72);
        protected override bool NeedsToDrawBacks => true;

        protected string GetFillColor()
        {
            if (DeckObject!.IsUnknown)
            {
                return BackColor;
            }
            if (DeckObject!.Color == EnumColorTypes.ZOther || DeckObject!.Color == EnumColorTypes.None)
            {
                return cc.White; //this will get hints of problems too.
            }
            return PrivateColor();
        }
        
        protected virtual string PrivateColor()
        {
            return DeckObject!.Color.ToColor();
        }

        protected override void BeforeFilling()
        {
            FillColor = GetFillColor();
        }

        protected override void DrawBacks()
        {
            if (BackText == "")
            {
                return; //can't show runtime errors anyways.
            }
            var list = BackText.Split(" ").ToCustomBasicList();
            if (list.Count > 2)
            {
                return;
            }
            //hopefully the fills are already done (?)
            CustomBasicList<RectangleF> rectangles = new CustomBasicList<RectangleF>();
            if (list.Count == 1)
            {
                rectangles.Add(new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height)); //i think
            }
            else
            {
                RectangleF firstRect;
                firstRect = new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height / 2.1f);
                var secondRect = new RectangleF(0, DefaultSize.Height / 2, DefaultSize.Width, DefaultSize.Height / 2.1f);
                rectangles.Add(firstRect);
                rectangles.Add(secondRect);
            }
            if (rectangles.Count != list.Count)
            {
                return;
            }
            int x = 0;
            foreach (var thisRect in rectangles)
            {
                float fontSize;
                var thisText = list[x];
                if (thisText.Length >= 5)
                {
                    fontSize = DefaultSize.Height / 5f;
                }
                else
                {
                    fontSize = DefaultSize.Height / 3.6f;
                }
                Text tt = new Text();
                tt.CenterText(MainGroup!, thisRect);
                tt.Content = thisText;
                tt.Font_Size = fontSize;
                tt.Fill = BackFontColor.ToWebColor();
                if (thisText.Length >= 5)
                {
                    tt.PopulateStrokesToStyles(strokeWidth: .5f);
                }
                else
                {
                    tt.PopulateStrokesToStyles(); //i think.
                }
                x++;
            }
        }

        protected void DrawValueCard(RectangleF rectangle, string valueNeeded) //hopefully this simple (?)
        {
            var fontSize = rectangle.Height * .45; //can adjust as needed.
            //text will be white.
            Text text = new Text();
            text.CenterText(MainGroup!, rectangle);
            text.Font_Size = fontSize;
            text.Content = valueNeeded;
            text.Fill = cc.White.ToWebColor();
            text.PopulateStrokesToStyles(strokeWidth: 1.4f);
        }

    }
}
