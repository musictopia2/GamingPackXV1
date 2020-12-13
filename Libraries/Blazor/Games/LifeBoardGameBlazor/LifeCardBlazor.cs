using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using LifeBoardGameCP.Cards;
using LifeBoardGameCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace LifeBoardGameBlazor
{
    public class LifeCardBlazor : BaseDeckGraphics<LifeBaseCard>
    {
        protected override SizeF DefaultSize => new SizeF(80, 100);
        protected override bool NeedsToDrawBacks => true;
        protected override void BeforeFilling()
        {
            if (DeckObject!.IsUnknown)
            {
                FillColor = cc.Green;
                return;
            }
            FillColor = cc.White;
        }
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.IsUnknown == true)
            {
                return true;
            }
            return DeckObject.CardCategory != EnumCardCategory.None;
        }
        protected override void DrawBacks()
        {
            Rect firstRect = new Rect();
            Rect secondRect = new Rect();
            Rect thirdRect = new Rect();
            Rect fourthRect = new Rect();
            firstRect.PopulateRectangle(8, 8, 15, 34);
            secondRect.PopulateRectangle(23, 8, 11, 34);
            thirdRect.PopulateRectangle(34, 8, 19, 34);
            fourthRect.PopulateRectangle(53, 8, 19, 34);
            Rect bottomRect = new Rect();
            bottomRect.PopulateRectangle(8, 50, 64, 42);
            var fontSize = 20;
            firstRect.Fill = cc.Purple.ToWebColor();
            firstRect.PopulateStrokesToStyles();
            secondRect.Fill = cc.Blue.ToWebColor();
            secondRect.PopulateStrokesToStyles();
            thirdRect.Fill = cc.Green.ToWebColor();
            thirdRect.PopulateStrokesToStyles();
            fourthRect.Fill = cc.DarkOrange.ToWebColor();
            fourthRect.PopulateStrokesToStyles();
            Text text = new Text();
            text.CenterText(MainGroup!, firstRect);
            MainGroup!.Children.Add(firstRect);
            text.CenterText(MainGroup!, firstRect);
            text.Font_Size = fontSize;
            text.Content = "L";
            text.PopulateStrokesToStyles();
            text.Fill = cc.White.ToWebColor();
            text = new Text();
            text.CenterText(MainGroup!, secondRect);
            MainGroup.Children.Add(secondRect);
            text.CenterText(MainGroup!, secondRect);
            text.Font_Size = fontSize;
            text.Content = "I";
            text.PopulateStrokesToStyles();
            text.Fill = cc.White.ToWebColor();
            text = new Text();
            text.CenterText(MainGroup!, thirdRect);
            MainGroup.Children.Add(thirdRect);
            text.CenterText(MainGroup!, thirdRect);
            text.Font_Size = fontSize;
            text.Content = "F";
            text.PopulateStrokesToStyles();
            text.Fill = cc.White.ToWebColor();
            text = new Text();
            text.CenterText(MainGroup!, fourthRect);
            MainGroup.Children.Add(fourthRect);
            text.CenterText(MainGroup!, fourthRect);
            text.Font_Size = fontSize;
            text.Content = "E";
            text.PopulateStrokesToStyles();
            text.Fill = cc.White.ToWebColor();
            string currentColor;
            string content = DeckObject!.CardCategory.ToString();
            switch (DeckObject!.CardCategory)
            {
                case EnumCardCategory.Career:
                    currentColor = cc.Blue.ToWebColor();
                    break;
                case EnumCardCategory.House:
                    currentColor = cc.DarkOrange.ToWebColor();
                    break;
                case EnumCardCategory.Salary:
                    currentColor = cc.Green.ToWebColor();
                    break;
                case EnumCardCategory.Stock:
                    currentColor = cc.Purple.ToWebColor();
                    break;
                default:
                    currentColor = cc.White.ToWebColor();
                    content = "Unknown"; //to help with debugging since blazor does not have debugging support (does not show any errors).
                    break;
            }
            bottomRect.Fill = currentColor;
            MainGroup.Children.Add(bottomRect);
            text = new Text();
            text.CenterText(MainGroup!, bottomRect);
            text.Font_Size = fontSize;
            text.Fill = cc.White.ToWebColor();
            text.Content = content;
            text.PopulateStrokesToStyles();
        }
        protected override void DrawImage()
        {
            switch (DeckObject!.CardCategory)
            {
                case EnumCardCategory.Career:
                    DrawCareerCard();
                    break;
                case EnumCardCategory.House:
                    DrawHouseCard();
                    break;
                case EnumCardCategory.Salary:
                    DrawSalaryCard();
                    break;
                case EnumCardCategory.Stock:
                    DrawStockCard();
                    break;
                default:
                    break;
            }
        }

        private void DrawCareerCard()
        {
            Rect pictureRect = new Rect();
            pictureRect.PopulateRectangle(15, 7, 50, 50);
            Rect firstRect = new Rect();
            firstRect.PopulateRectangle(8, 48, 32, 10);
            Rect secondRect = new Rect();
            secondRect.PopulateRectangle(40, 48, 32, 10);
            string content;
            string firstColor;
            string secondColor;
            CareerInfo career = (CareerInfo)DeckObject!;
            switch (career.Career)
            {
                case EnumCareerType.Doctor:
                    firstColor = cc.DarkOrange.ToWebColor();
                    secondColor = cc.Green.ToWebColor();
                    content = "Doctor";
                    break;
                case EnumCareerType.SalesPerson:
                    firstColor = cc.Red.ToWebColor();
                    secondColor = cc.Green.ToWebColor();
                    content = "Sales Person";
                    break;
                case EnumCareerType.ComputerConsultant:
                    firstColor = cc.DarkBlue.ToWebColor();
                    secondColor = cc.Green.ToWebColor();
                    content = "Computer Consultant";
                    break;
                case EnumCareerType.Teacher:
                    firstColor = cc.Red.ToWebColor();
                    secondColor = cc.Green.ToWebColor();
                    content = "Teacher";
                    break;
                case EnumCareerType.Accountant:
                    firstColor = cc.DarkBlue.ToWebColor();
                    secondColor = cc.Red.ToWebColor();
                    content = "Accountant";
                    break;
                case EnumCareerType.Athlete:
                    firstColor = cc.DarkBlue.ToWebColor();
                    secondColor = cc.Red.ToWebColor();
                    content = "Athlete";
                    break;
                case EnumCareerType.Artist:
                    firstColor = cc.DarkBlue.ToWebColor();
                    secondColor = cc.Red.ToWebColor();
                    content = "Artist";
                    break;
                case EnumCareerType.Entertainer:
                    firstColor = cc.DarkBlue.ToWebColor();
                    secondColor = cc.Red.ToWebColor();
                    content = "Entertainer";
                    break;
                case EnumCareerType.PoliceOfficer:
                    firstColor = cc.Red.ToWebColor();
                    secondColor = cc.Green.ToWebColor();
                    content = "Police Officer";
                    break;
                default:
                    content = "Unknown";
                    firstColor = cc.White.ToWebColor();
                    secondColor = cc.White.ToWebColor();
                    break;
            }
            Image image = new Image();
            image.PopulateFullExternalImage(this, $"{career.Career}.png");
            image.PopulateImagePositionings(pictureRect);
            MainGroup!.Children.Add(image);
            firstRect.Fill = firstColor;
            secondRect.Fill = secondColor;
            Rect lastRect = new Rect();
            lastRect.PopulateRectangle(8, 65, 64, 27);
            MainGroup.Children.Add(lastRect);
            lastRect.Fill = cc.Blue.ToWebColor();
            MainGroup!.Children.Add(firstRect);
            MainGroup.Children.Add(secondRect);
            DrawCareerSymbol(career.Career);
            var fontSize = 11;
            if (career.Career != EnumCareerType.ComputerConsultant)
            {
                Text firstText = new Text();
                firstText.CenterText(MainGroup, lastRect);
                firstText.Content = content;
                firstText.Font_Size = fontSize;
                firstText.Fill = cc.White.ToWebColor();
            }
            else
            {
                Text line = new Text();
                line.CenterText(MainGroup, 8, 65, 64, 14);
                line.Content = "Computer";
                line.Fill = cc.White.ToWebColor();
                line.Font_Size = fontSize;
                line = new Text();
                line.CenterText(MainGroup, 8, 75, 64, 14);
                line.Content = "Consultant";
                line.Fill = cc.White.ToWebColor();
                line.Font_Size = fontSize;
            }
        }
        private void DrawHouseCard()
        {
            Image image = new Image();
            image.PopulateImagePositionings(20, 8, 40, 40);
            HouseInfo house = (HouseInfo)DeckObject!;
            image.PopulateFullExternalImage(this, $"{house.HouseCategory}.png");
            MainGroup!.Children.Add(image);
            Rect firstRect = new Rect();
            firstRect.PopulateRectangle(8, 65, 64, 27);
            MainGroup.Children.Add(firstRect);
            firstRect.Fill = cc.DarkOrange.ToWebColor();
            string text = house.HouseCategory.ToString().GetWords();
            var firstFont = 11;
            var secondFont = 14;
            string firstColor = cc.White.ToWebColor();
            string secondColor = cc.Black.ToWebColor();
            Text firsts = new Text();
            firsts.Fill = firstColor;
            firsts.Font_Size = firstFont;
            if (house.HouseCategory != EnumHouseType.DutchColonial)
            {
                firsts.CenterText(MainGroup, firstRect);
                firsts.Content = text;
            }
            else
            {
                firsts.CenterText(MainGroup, 8, 63, 64, 17);
                firsts.Content = "Dutch";

                firsts = new Text();
                firsts.Fill = firstColor;
                firsts.Font_Size = firstFont;
                firsts.CenterText(MainGroup, 8, 75, 64, 17);
                firsts.Content = "Colonial";
            }
            Text seconds = new Text();
            seconds.Content = house.HousePrice.ToCurrency(0);
            seconds.Fill = secondColor;
            seconds.Font_Size = secondFont;
            seconds.CenterText(MainGroup, 8, 48, 64, 17);
            seconds.Font_Weight = "bold";
        }
        private void DrawSalaryCard()
        {
            Text firstText = new Text();
            Text secondText = new Text();
            Text thirdText = new Text();
            Text fourthText = new Text();
            SalaryInfo salary = (SalaryInfo)DeckObject!;
            var fontSize = 15;
            firstText.CenterText(MainGroup!, 3, 3, 70, 20);
            secondText.CenterText(MainGroup!, 3, 24, 70, 20);
            thirdText.CenterText(MainGroup!, 3, 55, 70, 20);
            fourthText.CenterText(MainGroup!, 3, 76, 70, 20);
            firstText.Font_Weight = "bold";
            secondText.Font_Weight = "bold";
            thirdText.Font_Weight = "bold";
            fourthText.Font_Weight = "bold";
            firstText.Font_Size = fontSize;
            secondText.Font_Size = fontSize;
            thirdText.Font_Size = fontSize;
            fourthText.Font_Size = fontSize;
            firstText.Content = "Collect";
            secondText.Content = salary.PayCheck.ToCurrency(0);
            thirdText.Content = "Taxes Due";
            thirdText.Content = salary.TaxesDue.ToCurrency(0);
            secondText.Fill = GetSalaryColor(salary.PayCheck).ToWebColor();
        }

        private string GetSalaryColor(decimal collectAmount)
        {
            string output;
            switch (collectAmount)
            {
                case var @case when @case == 100000:
                    {
                        output = cc.DarkOrange;
                        break;
                    }
                case var case1 when case1 == 90000:
                case var case2 when case2 == 70000:
                case var case3 when case3 == 60000:
                    {
                        output = cc.Green;
                        break;
                    }
                case var case4 when case4 == 80000:
                case var case5 when case5 == 30000:
                case var case6 when case6 == 40000:
                    {
                        output = cc.Red;
                        break;
                    }
                case var case7 when case7 == 50000:
                case var case8 when case8 == 20000:
                    {
                        output = cc.DarkBlue;
                        break;
                    }

                default:
                    {
                        return cc.White;
                    }
            }
            return output;
        }
        private void DrawStockCard()
        {
            Text firstText = new Text();
            Text secondText = new Text();
            Text thirdText = new Text();
            Text fourthText = new Text();
            StockInfo stock = (StockInfo)DeckObject!;
            var firstFont = 15;
            var secondFont = 24;
            firstText.CenterText(MainGroup!, 3, 10, 70, 20);
            secondText.CenterText(MainGroup!, 3, 31, 70, 20);
            thirdText.CenterText(MainGroup!, 3, 51, 70, 20);
            fourthText.CenterText(MainGroup!, 3, 76, 70, 20);
            string purpleText = cc.Purple.ToWebColor();
            firstText.Font_Weight = "bold";
            secondText.Font_Weight = "bold";
            thirdText.Font_Weight = "bold";
            fourthText.Font_Weight = "bold";
            firstText.Font_Size = secondFont;
            secondText.Font_Size = firstFont;
            thirdText.Font_Size = firstFont;
            fourthText.Font_Size = firstFont;
            firstText.Fill = purpleText;
            firstText.Content = stock.Value.ToString();
            secondText.Content = "Stock";
            thirdText.Content = "Certificate";
            fourthText.Content = "$50,000";
        }
        private void PopulateStrokePath(float strokeWidth, string d)
        {
            Path path = new Path();
            path.PopulateStrokesToStyles(cc.White.ToWebColor(), strokeWidth);
            path.D = d;
            MainGroup!.Children.Add(path);
        }
        private void FillPath(string d)
        {
            Path path = new Path();
            path.Fill = cc.White.ToWebColor();
            path.D = d;
            MainGroup!.Children.Add(path);
        }
        private void PopulateFullSymbolRectangle()
        {
            Rect rect = new Rect();
            rect.PopulateRectangle(30, 43, 20, 20);
            rect.PopulateStrokesToStyles(cc.White.ToWebColor(), 1);
            MainGroup!.Children.Add(rect);
        }
        private void FillRectangle(float x, float y, float width, float height)
        {
            Rect rect = new Rect();
            rect.PopulateRectangle(x, y, width, height);
            rect.Fill = cc.White.ToWebColor();
            MainGroup!.Children.Add(rect);
        }
        private void DrawCareerSymbol(EnumCareerType career)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.CX = "40";
            ellipse.CY = "53";
            ellipse.RX = "10";
            ellipse.RY = "10";
            ellipse.Fill = "rgb(70,130,180)";
            MainGroup!.Children.Add(ellipse);
            switch (career)
            {
                case EnumCareerType.Doctor:
                    PopulateStrokePath(6, "M40 45L40 61");
                    PopulateStrokePath(6, "M32 53L48 53");
                    break;
                case EnumCareerType.SalesPerson:
                    FillPath("M0 0L32 53L40 61L48 53L40 45L36 45L32 49L0 0ZM36 48Q36 48.0985 35.9808 48.1951Q35.9616 48.2917 35.9239 48.3827Q35.8862 48.4737 35.8315 48.5556Q35.7767 48.6375 35.7071 48.7071Q35.6375 48.7767 35.5556 48.8315Q35.4737 48.8862 35.3827 48.9239Q35.2917 48.9616 35.1951 48.9808Q35.0985 49 35 49Q34.9015 49 34.8049 48.9808Q34.7083 48.9616 34.6173 48.9239Q34.5263 48.8862 34.4444 48.8315Q34.3625 48.7767 34.2929 48.7071Q34.2232 48.6375 34.1685 48.5556Q34.1138 48.4737 34.0761 48.3827Q34.0384 48.2917 34.0192 48.1951Q34 48.0985 34 48Q34 47.9015 34.0192 47.8049Q34.0384 47.7083 34.0761 47.6173Q34.1138 47.5263 34.1685 47.4444Q34.2232 47.3625 34.2929 47.2929Q34.3625 47.2232 34.4444 47.1685Q34.5263 47.1138 34.6173 47.0761Q34.7083 47.0384 34.8049 47.0192Q34.9015 47 35 47Q35.0985 47 35.1951 47.0192Q35.2917 47.0384 35.3827 47.0761Q35.4737 47.1138 35.5556 47.1685Q35.6375 47.2232 35.7071 47.2929Q35.7767 47.3625 35.8315 47.4444Q35.8862 47.5263 35.9239 47.6173Q35.9616 47.7083 35.9808 47.8049Q36 47.9015 36 48Z");
                    break;
                case EnumCareerType.ComputerConsultant:
                    FillRectangle(35, 45, 10, 8);
                    FillRectangle(33, 55, 14, 4);
                    break;
                case EnumCareerType.Teacher:
                    Ellipse others = new Ellipse();
                    others.Fill = cc.White.ToWebColor();
                    others.CX = "40";
                    others.CY = "55";
                    others.RX = "7";
                    others.RY = "6";
                    MainGroup!.Children.Add(others);
                    FillPath("M40 49L38 47L34 45L34 45L35 48L40 49L40 49L42 47L43 45L40 49Z");
                    break;
                case EnumCareerType.Accountant:
                    Text text = new Text();
                    text.CenterText(MainGroup!, 30, 43, 20, 20); //i think (?)
                    text.Fill = cc.White.ToWebColor();
                    text.Font_Size = 16;
                    text.Content = "$";
                    break;
                case EnumCareerType.Athlete:
                    FillPath("M44 46L36 46L36 50Q36 50.1965 36.012 50.3921Q36.0241 50.5876 36.048 50.7804Q36.072 50.9731 36.1076 51.1611Q36.1433 51.3492 36.1903 51.5307Q36.2373 51.7123 36.2952 51.8856Q36.3531 52.0589 36.4213 52.2223Q36.4896 52.3857 36.5675 52.5376Q36.6454 52.6895 36.7322 52.8284Q36.8191 52.9674 36.914 53.092Q37.009 53.2167 37.1111 53.3259Q37.2132 53.435 37.3215 53.5277Q37.4298 53.6203 37.5433 53.6955Q37.6568 53.7707 37.7743 53.8278Q37.8918 53.8848 38.0123 53.9231Q38.1327 53.9615 38.255 53.9807Q38.3772 54 38.5 54L39 58L39 59L39 59L37 59L37 59L37 61L37 61L43 61L43 61L43 59L43 59L41 59L41 59L41 58L41.5 54Q41.6228 54 41.745 53.9807Q41.8673 53.9615 41.9877 53.9231Q42.1082 53.8848 42.2257 53.8278Q42.3432 53.7707 42.4567 53.6955Q42.5702 53.6203 42.6785 53.5277Q42.7868 53.435 42.8889 53.3259Q42.991 53.2167 43.086 53.092Q43.1809 52.9674 43.2678 52.8284Q43.3546 52.6895 43.4325 52.5376Q43.5104 52.3857 43.5787 52.2223Q43.6469 52.0589 43.7048 51.8856Q43.7627 51.7123 43.8097 51.5307Q43.8567 51.3492 43.8923 51.1611Q43.928 50.9731 43.952 50.7804Q43.9759 50.5876 43.988 50.3921Q44 50.1965 44 50L44 46Z");
                    PopulateStrokePath(1, "M40 46L34 50L40 55");
                    PopulateStrokePath(1, "M40 46L46 50L40 55");
                    break;
                case EnumCareerType.Artist:
                    FillPath("M41.4142 50.1716L42.8284 51.5858L35.0502 59.364L33.636 57.9497L41.4142 50.1716ZM42.1213 50.8787L43.5355 48.0503L46.364 46.636L46.364 46.636L44.9497 49.4645L42.1213 50.8787Z");
                    break;
                case EnumCareerType.Entertainer:
                    FillPath("M40 43L38 50L32 50L37 55L35 61L40 57L45 61L44 55L48 50L42 50L40 43Z");
                    break;
                case EnumCareerType.PoliceOfficer:
                    FillPath("M34 47L37 49L40 47L40 47L43 49L46 47L47 48L46 49L46 50L46 50L45 57L40 61L40 61L35 57L34 50L34 50L34 49L33 48L34 47Z");
                    break;
                default:
                    break;
            }
            PopulateFullSymbolRectangle();
        }
    }
}