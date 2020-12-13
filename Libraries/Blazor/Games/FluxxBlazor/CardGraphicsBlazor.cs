using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using FluxxCP.Cards;
using Newtonsoft.Json;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using System.Reflection;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace FluxxBlazor
{
    public class CardGraphicsBlazor : BaseDeckGraphics<FluxxCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(73, 113);
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            return DeckObject!.IsUnknown == true || DeckObject.Deck > 0;
        }
        protected override void DrawBacks()
        {
            var bounds = new RectangleF(5, 5, 60, 100);
            Rect rect = new Rect();
            rect.Fill = cc.Black.ToWebColor();
            rect.PopulateRectangle(bounds);
            if (DeckObject!.IsSelected)
            {
                rect.PopulateStrokesToStyles(cc.Aqua.ToWebColor(), strokeWidth: 4);
            }
            else
            {
                rect.PopulateStrokesToStyles(cc.Red.ToWebColor(), strokeWidth: 4);
            }
            MainGroup!.Children.Add(rect);
        }
        private readonly CustomBasicList<string> _sideList;
        private readonly CustomBasicList<string> _textList;
        private readonly CustomBasicList<string> _keeperList;
        private readonly CustomBasicList<PointF> _locationList;
        private RectangleF _rightRect;
        public CardGraphicsBlazor()
        {
            _rightRect = new RectangleF(20, 0, 48, 113);
            _sideList = new CustomBasicList<string>() { "Draw 1, Play 1", "Play 2", "Play 3", "Play 4", "Play All", "Keeper Limit 2", "Keeper Limit 3", "Keeper Limit 4", "Draw 2", "Draw 3", "Draw 4", "Draw 5", "Hand Limit 0", "Hand Limit 1", "Hand Limit 2", "No-Hand Bonus", "Poor Bonus", "Rich Bonus", "Reverse Order", "First Play Random", "X=X+1", "Double Agenda", "Milk", "The Rocket", "The Moon", "War", "Television", "The Toaster", "Money", "Love", "Dreams", "Peace", "Bread", "The Sun", "Cookies", "Time", "The Brain", "Death", "Sleep", "Chocolate", "Toast", "5 Keepers", "Time Is Money", "Bed Time", "All You Need Is Love", "Peace (No War)", "Baked Goods", "Dreamland", "Hearts And Minds", "Milk And Cookies", "Rocket To The Moon", "Hippyism", "Night and Day", "Squishy Chocolate", "Rocket Science", "Winning the Lottery", "The Appliances", "The Brain (No TV)", "Death By Chocolate", "Chocolate Cookies", "Chocolate Milk", "10 Cards in Hand", "War=Death", "Trash a New Rule", "Take Another Turn", "Trade Hands", "Trash a Keeper", "Exchange Keepers", "Steal a Keeper", "Use What You Take", "Let's Do That Again", "Scramble Keepers", "Rules Reset", "Empty the Trash", "Draw 3, Play 2", "Draw 2 & Use 'Em", "Everybody Gets 1", "Discard & Draw", "Let's Simplify", "Rotate Hands", "No Limits", "Taxation!", "Jackpot" };
            _textList = new CustomBasicList<string>()
            {
                "Play|2 cards|per turn.",
                "Play|3 cards|per turn.",
                "Play|4 cards|per turn.",
                "Play|all|cards.",
                "The most|keepers|is 2.",
                "The most|keepers|is 3.",
                "The most|keepers|is 4.",
                "Draw|2 cards|per turn.",
                "Draw|3 cards|per turn.",
                "Draw|4 cards|per turn.",
                "Draw|5 cards|per turn.",
                "Hand|Limit Is|0",
                "Hand|Limit Is|1.",
                "Hand|Limit Is|2.",
                "Draw|3 Cards|Empty|Hand.",
                "Draw|1 Card|Least|Keepers.",
                "Can|Play 1|Most|Keepers.",
                "Opposite|Order|Turn.",
                "Next|Player|Random.",
                "Add One|To Any|Number.",
                "Second|Goal|Allowed.",
                "Discard|Rule.",
                "Get|Extra|Turn.",
                "Trade|hand|anybody.",
                "Discard|Any|Keeper.",
                "Trade|Keeper.",
                "Take A|Keeper|To Use.",
                "Take|R. Card|Player.",
                "Use Action|Or Rule|From|Discard.",
                "Reshuffle|Keepers.",
                "Remove|All|Rules.",
                "Reshuffle|Cards.",
                "Draw|3 cards|play 2.",
                "Draw|2 cards|play them.",
                "Draw|1 card|pass out.",
                "Draw|All New|Cards.",
                "Discard|up to|half rules.",
                "Players|Pass|Cards.",
                "Discard|Limit|Rules.",
                "All Players|Pick One|Card.",
                "Draw|3 extra|cards!"
            };
            _keeperList = new CustomBasicList<string>()
            {
                "Milk",
                "Rocket",
                "Moon",
                "Tank",
                "TV",
                "Toaster",
                "Money",
                "Love",
                "Dreams",
                "Peace",
                "Bread",
                "Sun",
                "Cookies",
                "Time",
                "Brain",
                "Death",
                "Sleep",
                "Chocolate"
            };
            Assembly thisA = Assembly.GetAssembly(GetType());
            string thisStr = thisA.ResourcesAllTextFromFile("sidepoints.json");
            _locationList = JsonConvert.DeserializeObject<CustomBasicList<PointF>>(thisStr);
        }
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            DrawSideText(DeckObject.Deck);
            string firstText;
            string secondText;
            switch (DeckObject.Deck)
            {
                case 1:
                    {
                        firstText = "Basic|Rules";
                        secondText = "Draw 1|Play 1";
                        break;
                    }
                case int _ when DeckObject.Deck <= 22:
                    {
                        firstText = "New|Rule";
                        secondText = _textList![DeckObject.Deck - 2];
                        break;
                    }
                case int _ when DeckObject.Deck <= 40:
                    {
                        firstText = "Keeper";
                        secondText = "";
                        break;
                    }
                case int _ when DeckObject.Deck <= 63:
                    {
                        firstText = "Goal";
                        if (DeckObject.Deck == 42)
                        {
                            secondText = "5|Keepers|To Win";
                        }
                        else if (DeckObject.Deck == 62)
                        {
                            secondText = "10 Card|In Hand|To Win";
                        }
                        else
                        {
                            secondText = "";
                        }
                        break;
                    }

                case int _ when DeckObject.Deck <= 83:
                    {
                        firstText = "Action";
                        secondText = _textList![(DeckObject.Deck - 63) + 20]; // i think
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
            DrawMainText(firstText, secondText);
            if ((firstText ?? "") == "Keeper")
            {
                DrawImages(_keeperList![DeckObject.Deck - 23], "");
            }
            else if ((firstText ?? "") == "Goal" && string.IsNullOrEmpty(secondText))
            {
                switch (DeckObject.Deck)
                {
                    case 41:
                        {
                            DrawImages("Bread", "Toaster");
                            break;
                        }
                    case 43:
                        {
                            DrawImages("Time", "Money");
                            break;
                        }
                    case 44:
                        {
                            DrawImages("Sleep", "Time");
                            break;
                        }
                    case 45:
                        {
                            DrawImages("Love", "");
                            break;
                        }
                    case 46:
                        {
                            DrawImages("Peace", "NoWar");
                            break;
                        }
                    case 47:
                        {
                            DrawImages("Bread", "Cookies");
                            break;
                        }
                    case 48:
                        {
                            DrawImages("Dreams", "Sleep");
                            break;
                        }
                    case 49:
                        {
                            DrawImages("Love", "Brain");
                            break;
                        }
                    case 50:
                        {
                            DrawImages("Milk", "Cookies");
                            break;
                        }
                    case 51:
                        {
                            DrawImages("Rocket", "Moon");
                            break;
                        }
                    case 52:
                        {
                            DrawImages("Peace", "Love");
                            break;
                        }
                    case 53:
                        {
                            DrawImages("Moon", "Sun");
                            break;
                        }
                    case 54:
                        {
                            DrawImages("Sun", "Chocolate");
                            break;
                        }
                    case 55:
                        {
                            DrawImages("Rocket", "Brain");
                            break;
                        }
                    case 56:
                        {
                            DrawImages("Dreams", "Money");
                            break;
                        }
                    case 57:
                        {
                            DrawImages("TV", "Toaster");
                            break;
                        }
                    case 58:
                        {
                            DrawImages("Brain", "NoTV");
                            break;
                        }
                    case 59:
                        {
                            DrawImages("Death", "Chocolate");
                            break;
                        }
                    case 60:
                        {
                            DrawImages("Chocolate", "Cookies");
                            break;
                        }
                    case 61:
                        {
                            DrawImages("Chocolate", "Milk");
                            break;
                        }
                    case 63:
                        {
                            DrawImages("War", "Death");
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        private string SidePaint()
        {
            if (DeckObject == null)
            {
                return cc.Aqua;
            }
            return DeckObject.Deck switch
            {
                int _ when DeckObject.Deck <= 22 => cc.Yellow,
                int _ when DeckObject.Deck <= 40 => cc.LimeGreen,
                int _ when DeckObject.Deck <= 63 => cc.Orchid,
                _ => cc.Aqua,
            };
        }
        private void DrawSideText(int deck)
        {
            string transform = "matrix(0 -1 1 0 0 107)";
            string sideColor = SidePaint();
            string textColor = cc.Black;
            float fontSize = 11.2f;
            if (deck == 45 || deck == 51 || deck == 59 || deck == 60)
            {
                fontSize = 10f;
            }
            Rect rect = new Rect();
            rect.Fill = sideColor.ToWebColor();
            rect.Width = "98";
            rect.Height = "14";
            rect.X = "3";
            rect.Y = "3";
            rect.Transform = transform;
            MainGroup!.Children.Add(rect);
            PointF location = _locationList[deck - 1];
            Text text = new Text();
            text.Content = _sideList[deck - 1];
            if (deck == 45 || deck == 51 || deck == 59)
            {
                var xx = location.X;
                xx += 2;
                text.X = xx.ToString();
            }
            else
            {
                text.X = location.X.ToString();
            }
            var fins = location.Y + 2;
            text.Y = fins.ToString();
            text.Font_Size = fontSize;
            text.Fill = textColor.ToWebColor();
            text.Transform = transform;
            MainGroup.Children.Add(text);
        }
        private void DrawMainText(string firstText, string secondText)
        {
            if (MainGroup == null)
            {
                return;
            }
            var fontSize = 15;
            string textColor = cc.Black;
            var firstList = firstText.Split("|").ToCustomBasicList();
            int tops;
            tops = 10;
            foreach (var thisText in firstList)
            {
                var textRect = new RectangleF(17, tops, 48, 20);
                Text text = new Text();
                text.Font_Weight = "bold";
                text.Content = thisText;
                text.Font_Size = fontSize;
                text.CenterText(MainGroup, textRect);
                text.Fill = textColor.ToWebColor();
                tops += 15;
            }
            var secondList = secondText.Split("|").ToCustomBasicList();
            fontSize = 11;
            tops = 50;
            foreach (var thisText in secondList)
            {
                var textRect = new RectangleF(17, tops, 48, 20);
                Text text = new Text();
                text.Content = thisText;
                text.Font_Size = fontSize;
                text.CenterText(MainGroup, textRect);
                text.Fill = textColor.ToWebColor();
                tops += 12;
            }
        }
        private void DrawImages(string firstText, string secondText)
        {
            if (MainGroup == null)
            {
                return;
            }
            if ((firstText ?? "") == "War")
            {
                firstText = "Tank";
            }
            if ((secondText ?? "") == "War")
            {
                secondText = "Tank";
            }
            var firstRect = new RectangleF(_rightRect.Left + (_rightRect.Width / 20), _rightRect.Top + (_rightRect.Height / 2) - (_rightRect.Width * 0.3f), _rightRect.Width - (_rightRect.Width / 10), (_rightRect.Width * 0.7f));
            RectangleF secondRect = default;
            if (!string.IsNullOrEmpty(secondText))
            {
                firstRect = new RectangleF(firstRect.Location.X, firstRect.Location.Y - (firstRect.Height * 0.55f), firstRect.Width, firstRect.Height);
                secondRect = new RectangleF(firstRect.Location.X, firstRect.Location.Y + (firstRect.Height * 1.1f), firstRect.Width, firstRect.Height);
            }
            string fileName = $"{firstText}.png";
            Image image = new Image();
            image.PopulateImagePositionings(firstRect);
            image.PopulateFullExternalImage(this, fileName);
            MainGroup.Children.Add(image);
            if (!string.IsNullOrEmpty(secondText))
            {
                fileName = $"{secondText}.png";
                image = new Image();
                image.PopulateImagePositionings(secondRect);
                image.PopulateFullExternalImage(this, fileName);
                MainGroup.Children.Add(image);
            }
        }
    }
}