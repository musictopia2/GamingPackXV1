using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Base
{
    //public record DeckRecord(int Deck, bool IsSelected, bool Drew, bool IsUnknown, bool IsEnabled, string Tag = ""); //so others can use as welol.
    public abstract class BaseDeckGraphics<D> : GraphicsCommand
        where D : class, IDeckObject, new()
    {
        [Parameter]
        public D? DeckObject { get; set; } //they have to provide not only the command parameter but also the deckobject.

        //something else has to handle it now.

        [Parameter]
        public bool ClickBlank { get; set; } = false; //not sure if we need the ability to click a blank one in all cases or not (?)

        [Parameter]
        public PointF Location { get; set; } //this needs to be an option.
        /// <summary>
        /// if this is part of a gameboard, then won't do the extra stuff to make borders fit.
        /// </summary>
        [Parameter]
        public bool PartOfBoard { get; set; } = false;

        [Parameter]
        public string TargetWidth { get; set; } = ""; //this focus on width alone.

        [Parameter]
        public string TargetHeight { get; set; } = ""; //this focus on height alone.

        [Parameter]
        public string TargetSize { get; set; } = ""; //this focus on the largest of them.
        protected abstract SizeF DefaultSize { get; } //i think this makes the most sense now.


        [Parameter]
        public float LongestSize { get; set; } //needs to have many options on setting the sizes needed.


        [Parameter]
        public bool EmptyBorders { get; set; } //used for games like captive queens where we can't quite use the built in frame but still needs to draw the frame.

        protected float BorderWidth { get; set; } = 4; //defaults at 4 but anybody can override that part.

        protected abstract bool NeedsToDrawBacks { get; }

        protected abstract bool CanStartDrawing();

        protected abstract void DrawBacks(); //if it needs to draw backs, decided this time, will not even get the rectangle.  they won't necessarily draw a rectangle anyways.
        protected G? MainGroup { get; set; }
        
        private void PopulateCustomViewBox(ISvg svg)
        {
            if (PartOfBoard == false)
            {
                var value = (BorderWidth / 2) * -1;
                if (DeckObject!.Rotated == false)
                {
                    svg.ViewBox = $"{value} {value} {DefaultSize.Width + BorderWidth } {DefaultSize.Height + BorderWidth }";
                }
                else
                {
                    svg.ViewBox = $"{value} {value} {DefaultSize.Height + BorderWidth } {DefaultSize.Width + BorderWidth }";
                }
                return;
            }
            if (DeckObject!.Rotated == false)
            {
                svg.ViewBox = $"0 0 {DefaultSize.Width} {DefaultSize.Height}";
            }
            else
            {
                svg.ViewBox = $"0 0 {DefaultSize.Height} {DefaultSize.Width}";
            }
        }

        protected float Scale()
        {
            if (LongestSize == 0)
            {
                return 1;
            }
            if (DefaultSize.Width <= DefaultSize.Height)
            {
                return LongestSize / DefaultSize.Height;
            }
            return LongestSize / DefaultSize.Width;
        }

        //for colors, keep for now.  could eventually do something for dark colors

        protected double GetDarkHighlighter() => .25; //can experiment to see what makes the most sense.

        protected double GetLightHighlighter() => .1; //may have to experiment more later.
        //this could be iffy when we get to color cards.
        protected string FillColor { set; get; } = cc.White; //defaults to white but can be any color.

        protected float RoundedRadius = 6;

        protected virtual bool ShowDisabledColors { get; } = false;

        //this will show isselected and drew  however, can override to do what is needed
        protected virtual void DrawHighlighters()
        {
            if (DeckObject!.Deck == 0)
            {
                return;
            }
            if (ShowDisabledColors == true && DeckObject!.IsEnabled == false)
            {
                Rect temp = StartRectangle();
                temp.Fill = cc.Gray.ToWebColor();
                MainGroup!.Children.Add(temp);
                return; //for now, if shown, then has to be this way.
            }
            if (DeckObject!.IsSelected == false && DeckObject.Drew == false)
            {
                return;
            }
            Rect rect = StartRectangle();
            rect.Fill_Opacity = GetOpacity;
            if (DeckObject.IsSelected)
            {
                rect.Fill = SelectFillColor;
            }
            else
            {
                rect.Fill = DrawFillColor;
            }
            MainGroup!.Children.Add(rect);
        }

        protected virtual string GetOpacity => GetLightHighlighter().ToString();
        protected virtual string SelectFillColor => cc.Red.ToWebColor();
        protected virtual string DrawFillColor => cc.Lime.ToWebColor();

        protected Rect StartRectangle()
        {
            Rect output = new Rect();
            if (PartOfBoard == false)
            {
                output.Width = (DefaultSize.Width - BorderWidth).ToString();
                output.Height = (DefaultSize.Height - BorderWidth).ToString();
            }
            else
            {
                output.Width = DefaultSize.Width.ToString();
                output.Height = DefaultSize.Height.ToString();
            }
            output.RX = RoundedRadius.ToString();
            output.RY = RoundedRadius.ToString();
            return output;
        }

        protected void PopulateImage(Image image)
        {
            if (PartOfBoard == false)
            {
                image.Width = (DefaultSize.Width - BorderWidth).ToString();
                image.Height = (DefaultSize.Height - BorderWidth).ToString();
            }
            else
            {
                image.Width = DefaultSize.Width.ToString();
                image.Height = DefaultSize.Height.ToString();
            }
        }

        public ISvg GetGraphicsToEmbed(RectangleF bounds)
        {
            ISvg svg = new SVG();

            if (DeckObject == null)
            {
                return svg;
            }

            //this will not have target size for this.
            if (bounds.Width > bounds.Height)
            {
                LongestSize = bounds.Width;
            }
            else
            {
                LongestSize = bounds.Height;
            }
            svg.X = bounds.X.ToString();
            svg.Y = bounds.Y.ToString();
            var value = DefaultSize.Width;
            if (DeckObject!.Rotated == false)
            {
                svg.Width = value.ToString();
            }
            else
            {
                svg.Height = value.ToString();
            }
            value = DefaultSize.Width;
            if (DeckObject.Rotated == false)
            {
                svg.Height = value.ToString();
            }
            else
            {
                svg.Width = value.ToString();
            }
            if (DeckObject!.Rotated == false)
            {
                svg.ViewBox = $"0 0 {DefaultSize.Width} {DefaultSize.Height}";
            }
            else
            {
                svg.ViewBox = $"0 0 {DefaultSize.Height} {DefaultSize.Width}";
            }
            MainGroup = new G();
            svg.Children.Add(MainGroup); //everything is happening on the group because of the rotations.
            PopulateRotation();
            //has to draw the default borders and fill colors.
            Rect rect = StartRectangle();
            rect.PopulateStrokesToStyles(strokeWidth: (int)BorderWidth);
            BeforeFilling();
            rect.Fill = FillColor.ToWebColor();
            MainGroup.Children.Add(rect);
            DrawImage();
            return svg;

        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (ClickBlank == false)
            {
                if (DeckObject == null || DeckObject.Visible == false || CanStartDrawing() == false)
                {
                    return;
                }
            }
            ISvg svg = new SVG();
            SvgRenderClass render = new SvgRenderClass();
            render.Allow0 = true; //try this way.  because some cards can display 0.
            if (TargetSize != "")
            {
                if (DefaultSize.Width >= DefaultSize.Height)
                {
                    if (DeckObject!.Rotated == false)
                    {
                        svg.Width = TargetSize;
                    }
                    else
                    {
                        svg.Height = TargetSize;
                    }
                }
                else
                {
                    if (DeckObject!.Rotated == false)
                    {
                        svg.Height = TargetSize;
                    }
                    else
                    {
                        svg.Width = TargetSize;
                    }
                }
                svg.X = Location.X.ToString();
                svg.Y = Location.Y.ToString();
                PopulateCustomViewBox(svg);
            }
            else if (TargetHeight != "")
            {
                if (DeckObject!.Rotated == false)
                {
                    svg.Height = TargetHeight; //trying something else.
                }
                else
                {
                    svg.Width = TargetHeight;
                }
                PopulateCustomViewBox(svg);
            }
            else if (TargetWidth != "")
            {
                if (DeckObject!.Rotated == false)
                {
                    svg.Width = TargetWidth;
                }
                else
                {
                    svg.Height = TargetWidth;
                }
                PopulateCustomViewBox(svg);
            }
            else
            {
                svg.X = Location.X.ToString();
                svg.Y = Location.Y.ToString();
                var value = Scale() * DefaultSize.Width;
                if (DeckObject!.Rotated == false)
                {
                    svg.Width = value.ToString();
                }
                else
                {
                    svg.Height = value.ToString();
                }
                value = Scale() * DefaultSize.Height;
                if (DeckObject.Rotated == false)
                {
                    svg.Height = value.ToString();
                }
                else
                {
                    svg.Width = value.ToString();
                }
                if (PartOfBoard == false)
                {
                    value = (BorderWidth / 2) * -1;
                    if (DeckObject!.Rotated == false)
                    {
                        svg.ViewBox = $"{value} {value} {DefaultSize.Width + BorderWidth} {DefaultSize.Height + BorderWidth}";
                    }
                    else
                    {
                        svg.ViewBox = $"{value} {value} {DefaultSize.Height + BorderWidth} {DefaultSize.Width + BorderWidth}";
                    }
                }
                else
                {
                    if (DeckObject!.Rotated == false)
                    {
                        svg.ViewBox = $"0 0 {DefaultSize.Width} {DefaultSize.Height}";
                    }
                    else
                    {
                        svg.ViewBox = $"0 0 {DefaultSize.Height} {DefaultSize.Width}";
                    }
                }
            }

            MainGroup = new G();
            svg.Children.Add(MainGroup); //everything is happening on the group because of the rotations.
            PopulateRotation();
            //has to draw the default borders and fill colors.
            Rect rect = StartRectangle();
            if (DeckObject!.Deck == 0)
            {
                rect.Fill = cc.Transparent.ToWebColor();
            }
            else if (DeckObject!.Deck < 0)
            {
                rect.Fill = cc.Blue.ToWebColor();
                rect.Fill_Opacity = "0.0";
            }
            else
            {
                rect.PopulateStrokesToStyles(strokeWidth: (int)BorderWidth);
                BeforeFilling();
                rect.Fill = FillColor.ToWebColor();

            }
            MainGroup.Children.Add(rect);
            DrawHighlighters();
            if (DeckObject!.Deck >= 0)
            {
                if (DeckObject!.IsUnknown && NeedsToDrawBacks)
                {
                    DrawBacks();
                    if (DeckObject!.IsSelected)
                    {
                        DrawHighlighters(); //has to do again since it overrided the highlighters.
                    }
                }
                else if (EmptyBorders && DeckObject!.Deck == 0)
                {
                    rect.PopulateStrokesToStyles(color: cc.White.ToWebColor(), strokeWidth: (int)BorderWidth);
                    rect.Fill = cc.Navy.ToWebColor();
                }
                else if (DeckObject!.Deck != 0)
                {
                    DrawImage();
                }
            }
            CreateClick(svg); //now it can do automatically.  has to do before drawing the image.
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
        protected virtual void BeforeFilling() { }
        protected abstract void DrawImage();
        private void PopulateRotation() //the location is already handled.
        {
            if (DeckObject!.Rotated == false)
            {
                return;
            }
            MainGroup!.Transform = $"matrix(0 1 -1 0 {DefaultSize.Height}, 0)";
        }
    }
}