using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using MillebournesCP.Cards;
using MillebournesCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace MillebournesBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<MillebournesCardInformation>
    {
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
        protected override SizeF DefaultSize => new SizeF(60, 77);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown)
            {
                return true;
            }
            return DeckObject!.CompleteCategory != EnumCompleteCategories.None;
        }
        protected override void DrawBacks() { }
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            switch (DeckObject.CompleteCategory)
            {
                case EnumCompleteCategories.Distance25:
                    DrawDistanceCard("25");
                    break;
                case EnumCompleteCategories.Distance50:
                    DrawDistanceCard("50");
                    break;
                case EnumCompleteCategories.Distance75:
                    DrawDistanceCard("75");
                    break;
                case EnumCompleteCategories.Distance100:
                    DrawDistanceCard("100");
                    break;
                case EnumCompleteCategories.Distance200:
                    DrawDistanceCard("200");
                    break;
                default:
                    DrawPiece(DeckObject.CompleteCategory);
                    break;
            }
            DrawHighlighters(); //good news is redrawing the highlighters fixed the problem with the not being able to highlight the puncture proof.
        }
        private void DrawPiece(EnumCompleteCategories category)
        {
            Image image = new Image();
            image.PopulateFullExternalImage(this, $"{category}.svg");
            RectangleF rect = new RectangleF(0, 0, 55, 72);
            image.PopulateImagePositionings(rect);
            MainGroup!.Children.Add(image);
        }
        private void DrawDistanceCard(string value)
        {
            string textColor;
            string borderColor;
            switch (value)
            {
                case "25":
                    textColor = cc.Red;
                    borderColor = cc.Green;
                    break;
                case "50":
                    textColor = cc.Red;
                    borderColor = cc.Blue;
                    break;
                case "75":
                    textColor = cc.Green;
                    borderColor = cc.Red;
                    break;
                case "100":
                    textColor = cc.Green;
                    borderColor = cc.Blue;
                    break;
                case "200":
                    textColor = cc.Blue;
                    borderColor = cc.Green;
                    break;
                default:
                    return;
            }
            Path path = new Path();
            path.D = "M6 22.5Q6 22.1195 6.0265 21.7395Q6.053 21.3594 6.10594 20.9807Q6.15887 20.6021 6.23812 20.2257Q6.31736 19.8493 6.42272 19.4761Q6.52809 19.1029 6.65931 18.7338Q6.79054 18.3647 6.94731 18.0006Q7.10409 17.6365 7.28603 17.2782Q7.46797 16.9199 7.67465 16.5684Q7.88132 16.2169 8.11224 15.8729Q8.34314 15.5289 8.59773 15.1933Q8.85232 14.8578 9.12997 14.5314Q9.40762 14.205 9.70767 13.8887Q10.0077 13.5723 10.3294 13.2667Q10.6512 12.961 10.9938 12.6669Q11.3364 12.3728 11.6991 12.0908Q12.0618 11.8089 12.4437 11.5398Q12.8255 11.2708 13.2257 11.0153Q13.6259 10.7597 14.0433 10.5183Q14.4608 10.2769 14.8946 10.0503Q15.3284 9.82362 15.7775 9.61222Q16.2265 9.40082 16.6897 9.20521Q17.153 9.00959 17.6293 8.83022Q18.1056 8.65085 18.5938 8.48816Q19.082 8.32548 19.581 8.17987Q20.0799 8.03425 20.5884 7.90607Q21.0969 7.77788 21.6137 7.66742Q22.1306 7.55697 22.6544 7.46452Q23.1783 7.37206 23.708 7.29783Q24.2377 7.2236 24.7719 7.16776Q25.3062 7.11193 25.8436 7.07464Q26.3811 7.03734 26.9205 7.01867Q27.4599 7 28 7Q28.5401 7 29.0795 7.01867Q29.6189 7.03734 30.1564 7.07464Q30.6938 7.11193 31.2281 7.16776Q31.7623 7.2236 32.292 7.29783Q32.8217 7.37206 33.3456 7.46452Q33.8694 7.55697 34.3863 7.66743Q34.9031 7.77788 35.4116 7.90607Q35.9201 8.03426 36.419 8.17987Q36.918 8.32548 37.4062 8.48817Q37.8944 8.65085 38.3707 8.83022Q38.847 9.00959 39.3103 9.20521Q39.7735 9.40082 40.2225 9.61222Q40.6716 9.82362 41.1054 10.0503Q41.5392 10.2769 41.9567 10.5183Q42.3741 10.7597 42.7743 11.0153Q43.1745 11.2708 43.5563 11.5398Q43.9382 11.8089 44.3009 12.0908Q44.6636 12.3728 45.0062 12.6669Q45.3488 12.961 45.6706 13.2667Q45.9923 13.5723 46.2923 13.8887Q46.5924 14.205 46.87 14.5314Q47.1477 14.8578 47.4023 15.1933Q47.6569 15.5289 47.8878 15.8729Q48.1187 16.2169 48.3253 16.5684Q48.532 16.9199 48.714 17.2782Q48.8959 17.6365 49.0527 18.0006Q49.2095 18.3647 49.3407 18.7338Q49.4719 19.1029 49.5773 19.4761Q49.6826 19.8493 49.7619 20.2257Q49.8411 20.6021 49.8941 20.9807Q49.947 21.3594 49.9735 21.7395Q50 22.1195 50 22.5L50 65L6 65L6 22.5Z";
            path.PopulateStrokesToStyles(borderColor.ToWebColor(), 2.75f);
            MainGroup!.Children.Add(path);
            var rect = new RectangleF(0, 0, 55, 72);
            Text controlText = new Text();
            controlText.Content = value;
            controlText.Font_Size = 21;
            controlText.Font_Weight = "bold";
            controlText.Fill = textColor.ToWebColor();
            controlText.CenterText(MainGroup, rect);
            controlText.PopulateStrokesToStyles();
        }
    }
}