using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using PaydayCP.Cards;
using PaydayCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using System.Linq;
using SKColors = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace PaydayBlazor
{
    public class PaydayCardBlazor : BaseDeckGraphics<CardInformation>
    {
        private struct TempInfo
        {
            public RectangleF Bounds { get; set; }
            public string Name { get; set; }
            public TempInfo(string name, float x, float y, float width, float height)
            {
                Name = name;
                Bounds = new RectangleF(x, y, width, height);
            }
        }
        protected override SizeF DefaultSize => DeckObject!.DefaultSize;
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            if (DeckObject!.CardCategory == EnumCardCategory.None)
            {
                return false;
            }
            return DeckObject.Index > 0;
        }
        protected override void DrawBacks() { }
        protected override void DrawImage()
        {
            if (DeckObject!.CardCategory == EnumCardCategory.Mail)
            {
                DrawMainMail();
                return;
            }
            DrawMainDeal();
        }
        private void DrawMailImage(string name)
        {
            Image image = new Image();
            image.Width = "92%";
            image.Height = "92%";
            image.X = "0";
            image.Y = "0";
            image.PopulateFullExternalImage(this, name);
            MainGroup!.Children.Add(image);
        }
        private void DrawMainMail()
        {
            if (DeckObject == null)
            {
                return;
            }
            switch (DeckObject.Index)
            {
                case 1:
                    DrawBill("Know-it-All", SKColors.Blue, "University", SKColors.Blue, "You will when you leave", SKColors.Black, "Tuition", SKColors.Red, "Please Pay $5,000", SKColors.Red);
                    break;
                case 2:
                    DrawBill("Dr. Feemer", SKColors.Blue, "Super Ski Sunday", SKColors.Black, "Expense:", SKColors.Black, "Set One Broken Leg", SKColors.Black, "Please Pay $300", SKColors.Red);
                    break;
                case 3:
                    DrawMailImage("MailPolution.svg");
                    break;
                case 4:
                    DrawMonsterCharge("$1,000", "$100");
                    break;
                case 5:
                    DrawMadMoney("Your band played for", SKColors.Black, "the highschool dance.", SKColors.Black, "Collect $300 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 6:
                    DrawPayANeighbor("Made new living", SKColors.Black, "and dining room", SKColors.Black, "drapes.", SKColors.Black, "$2,000", SKColors.Red);
                    break;
                case 7:
                    DrawMoveAhead();
                    break;
                case 8:
                    DrawBill("Dr. Patella", SKColors.Blue, "New Knee", SKColors.Black, "(You Can Dance Again)", SKColors.Black, "Please Pay $3,000", SKColors.Red, "", SKColors.Black);
                    break;
                case 9:
                    DrawBill("Zapp Electric Co.", SKColors.Blue, "Shocking!", SKColors.Black, "", SKColors.Black, "Please Pay $300", SKColors.Red, "", SKColors.Blue);
                    break;
                case 10:
                    DrawPayANeighbor("Made a ", SKColors.Black, "bridesmaid's gown", SKColors.Black, "$2,000.", SKColors.Red, "", SKColors.Red);
                    break;
                case 11:
                    DrawMadMoney("Wash & Vacuum", SKColors.Black, "neighbor's cars", SKColors.Black, "Collect $100 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 12:
                    DrawMonsterCharge("$3,000", "$300");
                    break;
                case 13:
                    DrawBill("M. Broider, M.D.", SKColors.Blue, "Ten Stitches", SKColors.Black, "($200 a Stitch)", SKColors.Black, "Please Pay $2,000", SKColors.Red, "", SKColors.Black);
                    break;
                case 14:
                    DrawMoveAhead();
                    break;
                case 15:
                    DrawPayANeighbor("Piano lessons for one", SKColors.Black, "month for all nine kids", SKColors.Black, "$300", SKColors.Red, "", SKColors.Red);
                    break;
                case 16:
                    DrawMadMoney("Cat sitting and dog", SKColors.Black, "walking one month", SKColors.Black, "Collect $400 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 17:
                    DrawMonsterCharge("$4,000", "$400");
                    break;
                case 18:
                    DrawMailImage("MailScholarship.svg");
                    break;
                case 19:
                    DrawBill("Big Noise Inc.", SKColors.Blue, "Installed car", SKColors.Black, "stereo system", SKColors.Black, "Please Pay $700", SKColors.Red, "", SKColors.Black);
                    break;
                case 20:
                    DrawMoveAhead();
                    break;
                case 21:
                    DrawBill("The Six Tubas", SKColors.Blue, "We played at your", SKColors.Black, "kid's birthday party", SKColors.Black, "Please Pay $300", SKColors.Red, "", SKColors.Black);
                    break;
                case 22:
                    DrawMadMoney("After-school job at", SKColors.Black, "Burgers 'N' Buns", SKColors.Black, "Collect $200 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 23:
                    DrawPayANeighbor("Snow shoveling for", SKColors.Black, "six blizzards", SKColors.Black, "$200", SKColors.Red, "", SKColors.Red);
                    break;
                case 24:
                    DrawMailImage("MailEnvironment.svg");
                    break;
                case 25:
                    DrawBill("Boom Box Music", SKColors.Blue, "Club", SKColors.Blue, "(Went a little", SKColors.Black, "overboard didn't we?)", SKColors.Black, "Please Pay $800", SKColors.Red);
                    break;
                case 26:
                    DrawMoveAhead();
                    break;
                case 27:
                    DrawBill("Dr. I.M. Blurd", SKColors.Blue, "One pair of", SKColors.Black, "designer eyeglasses", SKColors.Black, "Please Pay $200", SKColors.Red, "", SKColors.Black);
                    break;
                case 28:
                    DrawMadMoney("Sang at", SKColors.Black, "friend's wedding", SKColors.Black, "Collect $2,000 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 29:
                    DrawPayANeighbor("Babysitting the twins", SKColors.Black, "for one long month", SKColors.Black, "$300", SKColors.Red, "", SKColors.Red);
                    break;
                case 30:
                    DrawMailImage("MailWhale.svg");
                    break;
                case 31:
                    DrawBill("Mo Larr, D.D.S.", SKColors.Blue, "Fashion braces", SKColors.Black, "Please Pay $1,500", SKColors.Red, "", SKColors.Red, "", SKColors.Black);
                    break;
                case 32:
                    DrawMoveAhead();
                    break;
                case 33:
                    DrawBill("Pearl E. White,", SKColors.Blue, "D.D.S", SKColors.Blue, "Drilling, filling & billing", SKColors.Black, "Please Pay $100", SKColors.Red, "", SKColors.Black);
                    break;
                case 34:
                    DrawMadMoney("Catered", SKColors.Black, "friend's party", SKColors.Black, "Collect $1,000 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 35:
                    DrawPayANeighbor("Painted pet portrait", SKColors.Black, "for your Christmas", SKColors.Black, "cards", SKColors.Black, "$300", SKColors.Red);
                    break;
                case 36:
                    DrawMailImage("MailEndangeredSpecies.svg");
                    break;
                case 37:
                    DrawMadMoney("Neighbor drove car", SKColors.Black, "onto your front porch", SKColors.Black, "Collect $2,000 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 38:
                    DrawPayANeighbor("Cut the grass and", SKColors.Black, "raked it, too", SKColors.Black, "$200", SKColors.Red, "", SKColors.Red);
                    break;
                case 39:
                    DrawBill("Tick Tock Inc.", SKColors.Blue, "We cleaned your clock", SKColors.Black, "Please Pay $200", SKColors.Red, "", SKColors.Black, "", SKColors.Black);
                    break;
                case 40:
                    DrawMoveAhead();
                    break;
                case 41:
                    DrawBill("Yakity-Yak", SKColors.Blue, "Telephone Co.", SKColors.Blue, "Talked To Cousins", SKColors.Black, " On The Phone", SKColors.Black, "Please Pay $600", SKColors.Red);
                    break;
                case 42:
                    DrawBill("Health Club", SKColors.Blue, "Family Membership", SKColors.Blue, "(Includes pets)", SKColors.Black, "Please Pay $1,500", SKColors.Red, "", SKColors.Black);
                    break;
                case 43:
                    DrawMailImage("MailRecyling.svg");
                    break;
                case 44:
                    DrawBill("Away We Go", SKColors.Blue, "Trabel Agency", SKColors.Blue, "Two-week vacation", SKColors.Black, "in the sun", SKColors.Black, "Please Pay $2,500", SKColors.Red);
                    break;
                case 45:
                    DrawMadMoney("Built doghouse", SKColors.Black, "for neighbor", SKColors.Black, "Collect $1,500 from the", SKColors.Red, "player of your choice", SKColors.Red, "NOW!", SKColors.Red);
                    break;
                case 46:
                    DrawMoveAhead();
                    break;
                case 47:
                    DrawPayANeighbor("", SKColors.Red, "Painted your garage", SKColors.Black, "$1,000", SKColors.Red, "", SKColors.Red);
                    break;
                default:
                    break;
            }
        }
        private void DrawBill(string str_Line1, string clr_1, string str_Line2, string clr_2, string str_Line3, string clr_3, string str_Line4, string clr_4, string str_Line5, string clr_5)
        {
            DrawCardFromTemplate("Bill", str_Line1, clr_1, str_Line2, clr_2, str_Line3, clr_3, str_Line4, clr_4, str_Line5, clr_5);
        }
        private void DrawPayANeighbor(string str_Line1, string clr_1, string str_Line2, string clr_2, string str_Line3, string clr_3, string str_Line4, string clr_4)
        {
            DrawCardFromTemplate("Neighbor", str_Line1, clr_1, str_Line2, clr_2, str_Line3, clr_3, str_Line4, clr_4, "", SKColors.Black);
        }
        private void DrawMadMoney(string str_Line1, string clr_1, string str_Line2, string clr_2, string str_Line3, string clr_3, string str_Line4, string clr_4, string str_Line5, string clr_5)
        {
            DrawCardFromTemplate("MadMoney", str_Line1, clr_1, str_Line2, clr_2, str_Line3, clr_3, str_Line4, clr_4, str_Line5, clr_5);
        }
        private void DrawCardFromTemplate(string str_Type, string str_Line1, string clr_1, string str_Line2, string clr_2, string str_Line3, string clr_3, string str_Line4, string clr_4, string str_Line5, string clr_5)
        {
            if (MainGroup == null)
            {
                return;
            }
            var bounds = GetBounds;
            var fontSize = bounds.Height * .13;
            Text text = new Text();
            text.Font_Size = fontSize;
            text.Font_Weight = "bold";
            var rect_Top = new RectangleF(bounds.Left, bounds.Top + 1, bounds.Width, bounds.Height * 0.2f);
            RectangleF rect;
            int int_Count;
            string str_Current;
            string clr_Current;
            MainGroup.Children.Add(text);
            text.Y = "12.125";
            if (str_Type == "Bill")
            {
                text.X = "53.218712";
                for (int_Count = 0; int_Count <= 2; int_Count++)
                {
                    Line line = new Line();
                    PointF firstPoint = new PointF(bounds.Left + (bounds.Width * 0.05f), bounds.Top + (rect_Top.Height / 6) + (rect_Top.Height / 6) * int_Count);
                    PointF secondPoint = new PointF(bounds.Left + (bounds.Width * 0.3f), bounds.Top + (rect_Top.Height / 6) + (rect_Top.Height / 6) * int_Count);
                    line.PopulateLine(firstPoint, secondPoint);
                    line.PopulateStrokesToStyles(SKColors.Red);
                    MainGroup.Children.Add(line);
                    firstPoint = new PointF(bounds.Left + (bounds.Width * 0.95f), bounds.Top + (rect_Top.Height / 6) + (rect_Top.Height / 6) * int_Count);
                    secondPoint = new PointF(bounds.Left + (bounds.Width * 0.7f), bounds.Top + (rect_Top.Height / 6) + (rect_Top.Height / 6) * int_Count);
                    line.PopulateLine(firstPoint, secondPoint);
                    line.PopulateStrokesToStyles(SKColors.Red);
                    MainGroup.Children.Add(line);
                }
                text.Fill = SKColors.Red.ToWebColor();
                text.Content = "Bill";
            }
            else if (str_Type == "Neighbor")
            {
                text.X = "21.989468";
                text.Fill = SKColors.Blue.ToWebColor();
                text.Content = "Pay A Neighbor";
            }
            else if (str_Type == "MadMoney")
            {
                text.X = "31.816467";
                text.Fill = SKColors.Green.ToWebColor();
                text.Content = "Mad Money";
            }
            if ((str_Type == "MoveAhead") | (str_Type == "Neighbor"))
            {
                fontSize = bounds.Height * 0.13;
            }
            else
            {
                fontSize = bounds.Height * 0.11f;
            }
            for (int_Count = 0; int_Count <= 4; int_Count++)
            {
                if (!string.IsNullOrEmpty(str_Line5))
                {
                    rect = new RectangleF(bounds.Left, (bounds.Top + (rect_Top.Height)) - (bounds.Height / 30) + (((bounds.Height - rect_Top.Height) / 5) * int_Count), bounds.Width, ((bounds.Height - rect_Top.Height) / 5));
                }
                else if (!string.IsNullOrEmpty(str_Line4))
                {
                    rect = new RectangleF(bounds.Left, (bounds.Top + (rect_Top.Height)) - (bounds.Height / 30) + (((bounds.Height - rect_Top.Height) / 4) * int_Count), bounds.Width, ((bounds.Height - rect_Top.Height) / 4));
                }
                else
                {
                    rect = new RectangleF(bounds.Left, (bounds.Top + (rect_Top.Height)) - (bounds.Height / 30) + (((bounds.Height - rect_Top.Height) / 3) * int_Count), bounds.Width, ((bounds.Height - rect_Top.Height) / 3));
                }
                switch (int_Count)
                {
                    case 0:
                        {
                            str_Current = str_Line1;
                            clr_Current = clr_1;
                            break;
                        }

                    case 1:
                        {
                            str_Current = str_Line2;
                            clr_Current = clr_2;
                            break;
                        }

                    case 2:
                        {
                            str_Current = str_Line3;
                            clr_Current = clr_3;
                            break;
                        }

                    case 3:
                        {
                            str_Current = str_Line4;
                            clr_Current = clr_4;
                            break;
                        }
                    default:
                        {
                            str_Current = str_Line5;
                            clr_Current = clr_5;
                            break;
                        }
                }
                text = new Text();
                text.Font_Size = fontSize;
                text.Font_Weight = "bold";
                text.Fill = clr_Current.ToWebColor();
                text.Content = str_Current;
                text.CenterText(MainGroup, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }
        private RectangleF GetBounds => new RectangleF(new PointF(0, 0), DeckObject!.DefaultSize);
        private void DrawMonsterCharge(string str_Total, string str_Due)
        {
            if (MainGroup == null)
            {
                return;
            }
            RectangleF bounds = GetBounds;
            //lets start out with drawing monster charges.
            var rect_Top = new RectangleF(bounds.Left, bounds.Top + (bounds.Height / 50), bounds.Width, bounds.Height * 0.3f);
            var rect_Left = new RectangleF(bounds.Left, bounds.Top + (bounds.Height * 0.3f), bounds.Width / 2, bounds.Height * 0.7f);
            var rect_Right = new RectangleF(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height * 0.3f), bounds.Width / 2, bounds.Height * 0.7f);
            var fontSize = bounds.Height * .17;
            Text text = new Text();
            text.Content = "Monster Charge";
            text.Fill = SKColors.Red.ToWebColor();
            text.Font_Weight = "bold";
            text.Font_Size = fontSize;
            text.CenterText(MainGroup, rect_Top.X, rect_Top.Y, rect_Top.Width, rect_Top.Height);
            fontSize = bounds.Height * .10;
            text = new Text();
            text.Font_Weight = "bold";
            text.Font_Size = fontSize;
            text.Content = "Total Charges";
            text.CenterText(MainGroup, rect_Left.X, rect_Left.Y, rect_Left.Width, rect_Left.Height);
            text = new Text();
            text.Font_Weight = "bold";
            text.Font_Size = fontSize;
            text.Content = "Interest Due";
            text.CenterText(MainGroup, rect_Right.X, rect_Right.Y, rect_Right.Width, rect_Right.Height);
            rect_Left = new RectangleF(rect_Left.Left, bounds.Top + (bounds.Height * 0.65f), bounds.Width / 2, bounds.Height / 4);
            rect_Right = new RectangleF(rect_Right.Left, bounds.Top + (bounds.Height * 0.65f), bounds.Width / 2, bounds.Height / 4);
            rect_Left = new RectangleF(rect_Left.Left, bounds.Top + (bounds.Height * 0.7f), bounds.Width / 2, bounds.Height / 4);
            rect_Right = new RectangleF(rect_Right.Left, bounds.Top + (bounds.Height * 0.7f), bounds.Width / 2, bounds.Height / 4);
            fontSize = bounds.Height * .17;
            text = new Text();
            text.Content = str_Total;
            text.Font_Size = fontSize;
            text.CenterText(MainGroup, rect_Left.X, rect_Left.Y, rect_Left.Width, rect_Left.Height);
            text = new Text();
            text.Content = str_Due;
            text.Font_Size = fontSize;
            text.CenterText(MainGroup, rect_Right.X, rect_Right.Y, rect_Right.Width, rect_Right.Height);
        }
        private void DrawMoveAhead()
        {
            DrawMailImage("MailMoveAhead.svg");
        }
        private string GetDealSvgName => $"Deal{DeckObject!.Index}.svg";
        private double DealMainFontSize => 8;
        private double DealMoneyFontSize => 10;
        private void DrawMainDeal()
        {
            Image image = new Image();
            image.PopulateImagePositionings(10, 7, 24, 33);
            image.PopulateFullExternalImage(this, GetDealSvgName);
            MainGroup!.Children.Add(image);
            DrawDealLine();
            DrawMoneyDeal("Cost", false, 0, 40, 49, 14);
            DrawMoneyDeal("Value", false, 49, 40, 49, 14);
            DealCard deal = (DealCard)DeckObject!;
            DrawMoneyDeal(deal.Cost.ToCurrency(0), true, 0, 50, 49, 14);
            DrawMoneyDeal(deal.Value.ToCurrency(0), true, 49, 50, 49, 14);
            DrawDealNames(deal);
        }
        private void DrawDealNames(DealCard deal)
        {
            CustomBasicList<TempInfo> list = GetDealSplitInfo(deal);
            list.ForEach(x => DrawDealPartial(x.Name, x.Bounds));
        }
        private CustomBasicList<TempInfo> GetDealSplitInfo(DealCard deal)
        {
            CustomBasicList<string> list = deal.Business.Split("|").ToCustomBasicList();
            CustomBasicList<TempInfo> output = new CustomBasicList<TempInfo>();
            TempInfo temp;
            if (list.Count == 2)
            {
                //split the rectangles.
                temp = new TempInfo(list.First(), 35, 0, 55, 24);
                output.Add(temp);
                temp = new TempInfo(list.Last(), 35, 16, 55, 24);
                output.Add(temp);
            }
            else if (list.Count == 3)
            {
                //another split.
                temp = new TempInfo(list.First(), 35, 0, 55, 16);
                output.Add(temp);
                temp = new TempInfo(list[1], 35, 14, 55, 16);
                output.Add(temp);
                temp = new TempInfo(list.Last(), 35, 28, 55, 16);
                output.Add(temp);
            }
            return output;
        }
        private void DrawDealPartial(string name, RectangleF rectangle)
        {
            Text text = new Text();
            text.Content = name;
            text.CenterText(MainGroup!, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            text.Font_Size = DealMainFontSize;
        }
        private void DrawMoneyDeal(string content, bool isBold, float x, float y, float width, float height)
        {
            Text text = new Text();
            text.Content = content;
            text.CenterText(MainGroup!, x, y, width, height);
            text.Font_Size = DealMoneyFontSize;
            if (isBold)
            {
                text.Font_Weight = "bold";
            }
        }
        private void DrawDealLine()
        {
            Line line = new Line();
            line.X1 = "49";
            line.Y1 = "40";
            line.X2 = "49";
            line.Y2 = "98";
            line.PopulateStrokesToStyles(strokeWidth: 2);
            MainGroup!.Children.Add(line);
        }
    }
}