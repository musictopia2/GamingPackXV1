using BasicGamingUIBlazorLibrary.GameGraphics.Cards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using LifeCardGameCP.Cards;
using LifeCardGameCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Collections.Specialized;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace LifeCardGameBlazor
{
    public class CardGraphicsBlazor : BaseDarkCardsBlazor<LifeCardGameCardInformation>
    {
        protected override SizeF DefaultSize => new SizeF(100, 115); //this is default but can change to anything you want.
        protected override bool NeedsToDrawBacks => true;
        protected override bool CanStartDrawing()
        {
            return DeckObject!.IsUnknown || DeckObject!.Category != EnumFirstCardCategory.None;
        }
        protected override bool IsLightColored => false;
        private void DrawRectangle(RectangleF bounds, string color, int borderSize)
        {
            Rect rect = new Rect();
            rect.PopulateRectangle(bounds);
            rect.Fill = color.ToWebColor();
            if (borderSize > 0)
            {
                rect.PopulateStrokesToStyles();
            }
            MainGroup!.Children.Add(rect);
        }
        private void DrawText(string content, RectangleF bounds, string color, float fontSize, int borderSize, bool bold = false)
        {
            Text text = new Text();
            text.Content = content;
            text.CenterText(MainGroup!, bounds);
            text.Fill = color.ToWebColor();
            text.Font_Size = fontSize;
            if (borderSize > 0)
            {
                text.PopulateStrokesToStyles(strokeWidth: borderSize);
            }
            if (bold)
            {
                text.Font_Weight = "bold";
            }
        }
        private void DrawBorders()
        {
            Rect rect = StartRectangle();
            rect.PopulateStrokesToStyles(strokeWidth: BorderWidth);
            MainGroup!.Children.Add(rect);
            DrawHighlighters();
        }
        protected override void DrawBacks()
        {
            RectangleF firstRect;
            RectangleF secondRect;
            RectangleF thirdRect;
            RectangleF fourthRect;
            firstRect = new RectangleF(8, 8, 21, 50);
            secondRect = new RectangleF(29, 8, 17, 50);
            thirdRect = new RectangleF(46, 8, 23, 50);
            fourthRect = new RectangleF(69, 8, 23, 50);
            RectangleF bottomRect;
            bottomRect = new RectangleF(8, 65, 84, 42);
            var fontSize = firstRect.Height * 0.6f;
            string color = cc.White;
            DrawRectangle(firstRect, cc.Purple, 1);
            DrawText("L", firstRect, color, fontSize, 1);
            DrawRectangle(secondRect, cc.Green, 1);
            DrawText("I", secondRect, color, fontSize, 1);
            DrawRectangle(thirdRect, cc.Blue, 1);
            DrawText("F", thirdRect, color, fontSize, 1);
            DrawRectangle(fourthRect, cc.DarkOrange, 1);
            DrawText("E", fourthRect, color, fontSize, 1);
            DrawRectangle(bottomRect, cc.LimeGreen, 0);
        }
        protected override void DrawImage()
        {
            if (DeckObject == null)
            {
                return;
            }
            string mainColor = GetMainColor();
            if (DeckObject.Points == 0)
            {
                DrawText("+10", new RectangleF(0, 0, DefaultSize.Width, DefaultSize.Height), mainColor, 12, 2); //experiment with size.
                return;
            }
            var tempRect = new RectangleF(3, 3, 94, 109);
            DrawRectangle(tempRect, mainColor, 2);
            DrawTopPortion();
            if (DeckObject.Points == 20 && DeckObject.Category == EnumFirstCardCategory.Career)
            {
                DrawPayday();
                DrawBorders();
                return;
            }
            if (DeckObject.Action != EnumAction.None)
            {
                DrawAction();
                DrawBorders();
                return;
            }
            string bottomText = "";
            string picture;
            switch (DeckObject.SpecialCategory)
            {
                case EnumSpecialCardCategory.Boat:
                    {
                        bottomText = "(Boat)";
                        break;
                    }
                case EnumSpecialCardCategory.Airplane:
                    {
                        bottomText = "(Plane)";
                        break;
                    }
                case EnumSpecialCardCategory.Car:
                    {
                        bottomText = "(Car)";
                        break;
                    }
                case EnumSpecialCardCategory.House:
                    {
                        bottomText = "(House)";
                        break;
                    }
                case EnumSpecialCardCategory.Marriage:
                    {
                        bottomText = "(1 wedding|per life story)";
                        break;
                    }
                case EnumSpecialCardCategory.Passport:
                    {
                        bottomText = "(1 passport|per life story)";
                        break;
                    }
            }
            switch (DeckObject.SwitchCategory)
            {
                case EnumSwitchCategory.Career:
                    {
                        if (!string.IsNullOrEmpty(bottomText))
                        {
                            return;
                        }
                        bottomText = "(Career)";
                        break;
                    }
                case EnumSwitchCategory.Baby:
                    {
                        if (!string.IsNullOrEmpty(bottomText))
                        {
                            return;
                        }
                        bottomText = "(Baby)";
                        break;
                    }
                case EnumSwitchCategory.Pet:
                    {
                        if (!string.IsNullOrEmpty(bottomText))
                        {
                            return;
                        }
                        bottomText = "(Pet)";
                        break;
                    }
            }
            if (DeckObject.Requirement == EnumSpecialCardCategory.House)
            {
                bottomText = "Home improvement";
            }
            switch (DeckObject.Description)
            {
                case "Degree":
                    {
                        picture = "My.Resources._7_University";
                        break;
                    }
                case "Stunt Double":
                    {
                        picture = "My.Resources._40_StuntDouble";
                        break;
                    }
                case "Pop Star":
                    {
                        picture = "My.Resources._11_PopStar";
                        break;
                    }
                case "Jet Pilot":
                    {
                        picture = "My.Resources._27_JetPilot";
                        break;
                    }
                case "Teacher":
                    {
                        picture = "My.Resources._51_Teacher";
                        break;
                    }
                case "Exotic Pet Vet":
                    {
                        picture = "My.Resources._52_ExoticPetVet";
                        break;
                    }
                case "Rocket Scientist":
                    {
                        picture = "My.Resources._49_RocketScientist";
                        break;
                    }
                case "Politician":
                    {
                        picture = "My.Resources._59_Politician";
                        break;
                    }
                case "Monkey":
                    {
                        picture = "My.Resources._43_Monkey";
                        break;
                    }
                case "Shark":
                    {
                        picture = "My.Resources._50_Shark";
                        break;
                    }
                case "Giraffe":
                    {
                        picture = "My.Resources._20_Giraffe";
                        break;
                    }
                case "Baby polar bear":
                    {
                        picture = "My.Resources._16_BabyPolarBear";
                        break;
                    }

                case "Lion":
                    {
                        picture = "My.Resources._15_Lion";
                        break;
                    }
                case "Vegas wedding":
                    {
                        picture = "My.Resources._1_VegasWedding";
                        break;
                    }
                case "Celebrity wedding":
                    {
                        picture = "My.Resources._2_CelebrityWedding";
                        break;
                    }
                case "Underwater wedding":
                    {
                        picture = "My.Resources._3_UnderwaterWedding";
                        break;
                    }
                case "Parachute wedding":
                    {
                        picture = "My.Resources._4_ParachuteWedding";
                        break;
                    }
                case "Beach wedding":
                    {
                        picture = "My.Resources._5_BeachWedding";
                        break;
                    }
                case "Fairytale wedding":
                    {
                        picture = "My.Resources._6_FairytaleWedding";
                        break;
                    }
                case "Golden|anniversary":
                    {
                        picture = "My.Resources._35_DiamondAnniversary";
                        break;
                    }
                case "Diamond|anniversary":
                    {
                        picture = "My.Resources._35_DiamondAnniversary";
                        break;
                    }
                case "Baby girl":
                    {
                        picture = "My.Resources._18_BabyGirl";
                        break;
                    }
                case "Baby triplets":
                    {
                        picture = "My.Resources._41_BabyTriplets";
                        break;
                    }
                case "Baby boy":
                    {
                        picture = "My.Resources._60_BabyBoy";
                        break;
                    }
                case "Baby girl twins":
                    {
                        picture = "My.Resources._54_BabyGirlTwins";
                        break;
                    }
                case "Baby boy twins":
                    {
                        picture = "My.Resources._19_TwinBoys";
                        break;
                    }
                case "Learn to|play the bonjos":
                    {
                        picture = "My.Resources._9_LearnToPlayTheBongos";
                        break;
                    }
                case "See a solar eclipse":
                    {
                        picture = "My.Resources._32_ViewSolarEclipse";
                        break;
                    }
                case "Go diving in|Niagara Falls":
                    {
                        picture = "My.Resources._39_GoDivingInNiagraFalls";
                        break;
                    }
                case "Headline at|a rock concert":
                    {
                        picture = "My.Resources._38_HeadlineAtRockConcert";
                        break;
                    }
                case "Find a message|in a bottle":
                    {
                        picture = "My.Resources._23_FindMessageInABottle";
                        break;
                    }
                case "Go Skydiving":
                    {
                        picture = "My.Resources._37_GoSkydiving";
                        break;
                    }
                case "Ride the tallest|rollercoaster in|the world":
                    {
                        picture = "My.Resources._10_RideTheTallestRollerCoaster";
                        break;
                    }
                case "Swim with dolphins":
                    {
                        picture = "My.Resources._44_SwimWithDolphins";
                        break;
                    }
                case "Win a charity|skateboard contest":
                    {
                        picture = "My.Resources._25_WinACharitySkateboardContest";
                        break;
                    }
                case "Win the jackpot":
                    {
                        picture = "My.Resources._46_WinTheJackpot";
                        break;
                    }
                case "Fly high in a|hot-air balloon":
                    {
                        picture = "My.Resources._8_FlyHighInHotAirBalloon";
                        break;
                    }
                case "Dance at the Rio|Carnival":
                    {
                        picture = "My.Resources._58_DanceAtRioCarnival";
                        break;
                    }
                case "Trek to the North|Pole":
                    {
                        picture = "My.Resources._16_BabyPolarBear";
                        break;
                    }
                case "Explore a|live volcano":
                    {
                        picture = "My.Resources._22_ExploreLiveVolcano";
                        break;
                    }
                case "Dig up dinosaur|fossils":
                    {
                        picture = "My.Resources._48_DigUpDinosaurFossil";
                        break;
                    }
                case "Travel to|the Moon|in a rocket":
                    {
                        picture = "My.Resources._26_TravelToMoonInRocket";
                        break;
                    }
                case "Find Big Foot":
                    {
                        picture = "My.Resources._21_FindBigFoot";
                        break;
                    }
                case "Win the Jungle|Safari Rally":
                    {
                        picture = "My.Resources._56_WinJungleSafariRaffle";
                        break;
                    }
                case "Sail solo around|the world":
                    {
                        picture = "My.Resources._57_SailAroundWorld";
                        break;
                    }
                case "Learn to|loop-the-loop":
                    {
                        picture = "My.Resources._55_LearnTheLoopTheLoop";
                        break;
                    }
                case "Passport":
                    {
                        picture = "My.Resources._12_PictureOfPeopleCarryingLuggage";
                        break;
                    }
                case "Pink Cadillac":
                    {
                        picture = "My.Resources._14_PinkCadillac";
                        break;
                    }
                case "Eco-bubble car":
                    {
                        picture = "My.Resources._17_EcoBubbleCar";
                        break;
                    }
                case "Racing yacht":
                    {
                        picture = "My.Resources._36_RacingYacht";
                        break;
                    }
                case "Bathtub boat":
                    {
                        picture = "My.Resources._34_BathtubBoat";
                        break;
                    }
                case "Private jet":
                    {
                        picture = "My.Resources._47_PrivateJet";
                        break;
                    }
                case "Treehouse":
                    {
                        picture = "My.Resources._13_Treehouse";
                        break;
                    }
                case "Igloo":
                    {
                        picture = "My.Resources._45_Igloo";
                        break;
                    }
                case "Lighthouse":
                    {
                        picture = "My.Resources._30_Lighthouse";
                        break;
                    }
                case "Beach house":
                    {
                        picture = "My.Resources._31_BeachHouse";
                        break;
                    }
                case "Ranch":
                    {
                        picture = "My.Resources._29_RanchHouse";
                        break;
                    }
                case "Eco house":
                    {
                        picture = "My.Resources._28_EcoHouse";
                        break;
                    }
                case "Castle":
                    {
                        picture = "My.Resources._24_Castle";
                        break;
                    }
                case "Build a|swimming pool":
                    {
                        picture = "My.Resources._33_BuildASwimmingPool";
                        break;
                    }
                case "Switch to|natural power":
                    {
                        picture = "My.Resources._53_SwitchToNaturalPower";
                        break;
                    }
                case "Build a multi-screen|cinema":
                    {
                        picture = "My.Resources._41_BuildMultiScreenCinema";
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
            DrawImagePngCards(picture, bottomText);
            DrawBorders();
        }
        private void DrawTopPortion()
        {
            if (DeckObject == null)
            {
                return;
            }
            var bounds = new RectangleF(5, 5, 90, 20);
            DrawRectangle(bounds, cc.DarkOrange, 1);
            RectangleF firstRect;
            firstRect = new RectangleF(5, 5, 20, 20);
            var fontSize = 15;
            string textColor = cc.Black;
            DrawRectangle(firstRect, cc.LimeGreen, 2);
            DrawText(DeckObject.Points.ToString(), firstRect, textColor, fontSize, 0);
            if (DeckObject.Requirement != EnumSpecialCardCategory.None && DeckObject.Action != EnumAction.MovingHouse)
            {
                DrawSvgPiece();
            }
            else if (DeckObject.SpecialCategory == EnumSpecialCardCategory.Switch)
            {
                DrawSvgPiece();
            }
        }

        private void DrawSvgPiece()
        {
            if (DeckObject == null || MainGroup == null)
            {
                return;
            }
            string name;
            if (DeckObject.Requirement !=  EnumSpecialCardCategory.None && DeckObject.Action != EnumAction.MovingHouse)
            {
                name = $"{DeckObject.Requirement}.svg";
            }
            else if (DeckObject.SpecialCategory != EnumSpecialCardCategory.None)
            {
                name = $"{DeckObject.SpecialCategory}.svg";
            }
            else
            {
                return;
            }
            var lastRect = new RectangleF(71, 5, 20, 20);
            Image image = new Image();
            image.PopulateFullExternalImage(this, name);
            image.PopulateImagePositionings(lastRect);
            MainGroup.Children.Add(image);
        }
        private void DrawPayday()
        {
            if (DeckObject == null)
            {
                return;
            }
            var firstRect = new RectangleF(3, 15, 94, 60);
            var fontSize = 40;
            string textColor = cc.White;
            DrawText("$", firstRect, textColor, fontSize, 2);
            fontSize = 12;
            textColor = cc.Black;
            var firstList = DeckObject.Description.Split("|").ToCustomBasicList();
            int tops;
            tops = 65;
            foreach (var thisText in firstList)
            {
                var lastRect = new RectangleF(3, tops, 94, 15);
                DrawText(thisText, lastRect, textColor, fontSize, 0);
                tops += 15; 
            }
        }
        private void DrawAction()
        {
            if (DeckObject == null)
            {
                return;
            }
            var topRect = new RectangleF(3, 25, 94, 13);
            bool isComplex = false;
            string lastBold;
            if (DeckObject.OpponentKeepsCard == true)
            {
                if (DeckObject.Action == EnumAction.MidlifeCrisis)
                {
                    isComplex = true;
                    lastBold = "They put this card in|their life story and|take a new card.";

                }
                else
                {
                    lastBold = "They keep this card.";
                }
            }
            else if (DeckObject.Action == EnumAction.LifeSwap || DeckObject.Action == EnumAction.SecondChance)
            {
                lastBold = "You keep this card.";
            }
            else
            {
                lastBold = "";
            }
            string firstBold;
            switch (DeckObject.Action)
            {
                case EnumAction.Lawsuit:
                    {
                        firstBold = "Lawsuit";
                        break;
                    }

                case EnumAction.IMTheBoss:
                    {
                        firstBold = "I'm The Boss";
                        break;
                    }

                case EnumAction.YoureFired:
                    {
                        firstBold = "You're Fired";
                        break;
                    }

                case EnumAction.TurnBackTime:
                    {
                        firstBold = "Turn Back Time";
                        break;
                    }

                case EnumAction.CareerSwap:
                    {
                        firstBold = "Career Swap";
                        break;
                    }

                case EnumAction.LostPassport:
                    {
                        firstBold = "Lost Passport";
                        break;
                    }

                case EnumAction.YourStory:
                    {
                        firstBold = "Your Story";
                        break;
                    }

                case EnumAction.LifeSwap:
                    {
                        firstBold = "Life Swap";
                        break;
                    }

                case EnumAction.SecondChance:
                    {
                        firstBold = "Second Chance";
                        break;
                    }

                case EnumAction.AdoptBaby:
                    {
                        firstBold = "Adopt a Baby";
                        break;
                    }

                case EnumAction.LongLostRelative:
                    {
                        firstBold = "Long-Lost Relative";
                        break;
                    }

                case EnumAction.MidlifeCrisis:
                    {
                        firstBold = "Mid-Life Crisis";
                        break;
                    }

                case EnumAction.MixUpAtVets:
                    {
                        firstBold = "Mix-up At Vet's";
                        break;
                    }

                case EnumAction.DonateToCharity:
                    {
                        firstBold = "Donate to Charity";
                        break;
                    }

                case EnumAction.MovingHouse:
                    {
                        firstBold = "Moving House";
                        break;
                    }

                default:
                    {
                        return;
                    }
            }
            var fontSize = 9;
            string textColor = cc.Black;
            var simpleBottomRect = new RectangleF(3, 100, 94, 10);
            DrawText(firstBold, topRect, textColor, fontSize, 0, true);
            int tops;
            var middleText = DeckObject.Description.Split("|").ToCustomBasicList();
            tops = 45;
            fontSize = 10;
            foreach (var thisMiddle in middleText)
            {
                var middleRect = new RectangleF(3, tops, 94, 10);
                DrawText(thisMiddle, middleRect, textColor, fontSize, 0);
                if (isComplex == false)
                {
                    tops += 12;
                }
                else
                {
                    tops += 10;
                }
            }
            if (string.IsNullOrEmpty(lastBold))
            {
                return;
            }
            fontSize = 8;
            if (isComplex == false)
            {
                DrawText(lastBold, simpleBottomRect, textColor, fontSize, 0, true);
            }
            else
            {
                var thisList = lastBold.Split("|").ToCustomBasicList();
                fontSize = 9;
                tops = 75;
                foreach (var item in thisList)
                {
                    var bounds = new RectangleF(3, tops, 94, 10);
                    DrawText(item, bounds, textColor, fontSize, 0, true);
                    tops += 11;
                }
            }
        }
        private void DrawImagePngCards(string fileName, string bottomText)
        {
            if (DeckObject == null || MainGroup == null)
            {
                return;
            }
            fileName = fileName.Replace("My.Resources._", "");
            fileName = fileName.Replace("_", "-");
            fileName += ".png";
            var topRect = new RectangleF(30, 30, 40, 40);
            if (DeckObject.SpecialCategory == EnumSpecialCardCategory.Passport || DeckObject.SpecialCategory ==  EnumSpecialCardCategory.Degree)
            {
                DrawRectangle(topRect, cc.Black, 0);
            }
            Image image = new Image();
            image.PopulateImagePositionings(topRect);
            image.PopulateFullExternalImage(this, fileName);
            MainGroup.Children.Add(image);
            float fontSize;
            string entireText;
            int heights;
            if (!string.IsNullOrEmpty(bottomText))
            {
                fontSize = 9;
                heights = 12;
                entireText = DeckObject.Description + "|" + bottomText;
            }
            else
            {
                fontSize = 10;
                heights = 14;
                entireText = DeckObject.Description;
            }
            string textColor = cc.Black;
            int tops;
            tops = 70;
            var thisList = entireText.Split("|").ToCustomBasicList();
            foreach (var thisText in thisList)
            {
                var thisRect =new RectangleF(3, tops, 94, heights);
                DrawText(thisText, thisRect, textColor, fontSize, 0);
                tops += heights + 1;
            }
        }
        private string GetMainColor()
        {
            if (DeckObject!.Action != EnumAction.None)
            {
                return cc.DarkOrange;
            }
            return DeckObject.Category switch
            {
                EnumFirstCardCategory.Career => cc.LightBlue,
                EnumFirstCardCategory.Family => cc.DeepPink,
                EnumFirstCardCategory.Adventure => cc.Yellow,
                EnumFirstCardCategory.Wealth => cc.LimeGreen,
                _ => "",
            };
        }
    }
}