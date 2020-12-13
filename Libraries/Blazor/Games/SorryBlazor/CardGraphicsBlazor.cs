using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using Microsoft.AspNetCore.Components;
using SorryCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
namespace SorryBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<CardInfo>
    {
        [Parameter]
        public SorryGameContainer? Container { get; set; }
        public CardGraphicsBlazor()
        {
            RoundedRadius = 15;
        }
        protected override SizeF DefaultSize => new SizeF(82, 172);
        protected override bool NeedsToDrawBacks => false;
        protected override bool CanStartDrawing()
        {
            return Container!.SaveRoot.DidDraw;
        }
        protected override void DrawBacks()
        {

        }
        protected override void DrawImage()
        {
            string realText;
            float fontSize;
            if (DeckObject!.Value == 13)
            {
                realText = "S";
            }
            else
            {
                realText = DeckObject.Value.ToString();
            }
            if (DeckObject.Value < 10 || realText == "S")
            {
                fontSize = 140f;
            }
            else
            {
                fontSize = 70f;
            }
            Text text = new Text();
            text.Font_Size = fontSize;
            text.CenterText();
            text.Content = realText;
            MainGroup!.Children.Add(text);
        }
    }
}