using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CandylandCP.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.Exceptions;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace CandylandBlazor
{
    public class CandylandCardBlazor : BaseDeckGraphics<CandylandCardData>
    {
        protected override SizeF DefaultSize => new SizeF(150, 84);
        protected override bool NeedsToDrawBacks => false;
        protected override bool CanStartDrawing()
        {
            return DeckObject!.WhichCard != EnumCandyLandType.None;
        }
        protected override void DrawBacks()
        {
            throw new BasicBlankException("This does not even have backs");
        }
        protected override void DrawImage()
        {
            switch (DeckObject!.WhichCard)
            {
                case EnumCandyLandType.IsAngel:
                    DrawOther("angel.png");
                    break;
                case EnumCandyLandType.IsFairy:
                    DrawOther("fairy.png");
                    break;
                case EnumCandyLandType.IsGirl:
                    DrawOther("girl.png");
                    break;
                case EnumCandyLandType.IsGuard:
                    DrawOther("guard.png");
                    break;
                case EnumCandyLandType.IsMagic:
                    DrawOther("magic.png");
                    break;
                case EnumCandyLandType.IsTree:
                    DrawOther("tree.png");
                    break;
                default:
                    {
                        if (DeckObject.HowMany != 1 && DeckObject.HowMany != 2)
                        {
                            throw new BasicBlankException("Only one or two are supported");
                        }
                        float int_Size;
                        float int_Offset;
                        var bounds = new RectangleF(0, 0, 150, 84);
                        PointF pt_Center = new PointF(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
                        int_Offset = bounds.Width / 10;
                        int_Size = Math.Min(bounds.Width - (int_Offset * 2), bounds.Height - (int_Offset * 2));
                        RectangleF rect1;
                        RectangleF rect2;
                        string color = "";
                        switch (DeckObject.WhichCard)
                        {
                            case EnumCandyLandType.IsBlue:
                                color = cc.Blue;
                                break;
                            case EnumCandyLandType.IsOrange:
                                color = cc.Orange;
                                break;
                            case EnumCandyLandType.IsGreen:
                                color = cc.Green;
                                break;
                            case EnumCandyLandType.IsPurple:
                                color = cc.Purple;
                                break;
                            case EnumCandyLandType.IsYellow:
                                color = cc.Yellow;
                                break;
                            case EnumCandyLandType.IsRed:
                                color = cc.Red;
                                break;

                            default:
                                break;
                        }
                        if (DeckObject.HowMany == 2)
                        {
                            rect1 = new RectangleF(pt_Center.X - int_Size - (int_Offset / 2), bounds.Top + (int_Offset), int_Size, int_Size);
                            rect2 = new RectangleF(pt_Center.X + (int_Offset / 2), bounds.Top + (int_Offset), int_Size, int_Size);
                            DrawRectangle(rect1, color);
                            DrawRectangle(rect2, color);
                        }
                        else
                        {
                            rect1 = new RectangleF(pt_Center.X - (int_Size / 2), bounds.Top + int_Offset, int_Size, int_Size);
                            DrawRectangle(rect1, color);
                        }
                        break;
                    }
            }
        }
        private void DrawRectangle(RectangleF rect, string color)
        {
            Rect draw = new Rect();
            draw.X = rect.X.ToString();
            draw.Y = rect.Y.ToString();
            draw.Width = rect.Width.ToString();
            draw.Height = rect.Height.ToString();
            draw.Fill = color.ToWebColor();
            draw.PopulateStrokesToStyles(strokeWidth: 2); //hopefully this simple.
            MainGroup!.Children.Add(draw);
        }
        private void DrawOther(string fileName)
        {
            Image image = new Image();
            image.PopulateFullExternalImage(this, fileName);
            image.Width = "100%";
            image.Height = "100%";
            MainGroup!.Children.Add(image);
        }
    }
}