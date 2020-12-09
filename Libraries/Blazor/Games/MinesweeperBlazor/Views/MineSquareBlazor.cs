using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MinesweeperCP.Data;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace MinesweeperBlazor.Views
{
    internal record MineRecord(bool Flagged, bool IsFlipped, bool Pressed);
    public class MineSquareBlazor : GraphicsCommand
    {

        [CascadingParameter]
        private GameboardBlazor? MainBoard { get; set; }
        private MineSquareModel? Square { get; set; } //this influences what gets drawn.


        private MineRecord? _previous;

        private MineRecord GetRecord()
        {
            return new MineRecord(Square!.Flagged, Square.IsFlipped, Square.Pressed);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            _previous = GetRecord();
            base.OnAfterRender(firstRender);
        }

        //this time, looks like this will use standard command.
        //needs commandparameter obviously as well.
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (CommandParameter == null)
            {
                return;
            }
            Square = (MineSquareModel)CommandParameter;
            ISvg svg = new SVG();
            SvgRenderClass render = new SvgRenderClass();
            CreateClick(svg);
            svg.ViewBox = "0 0 100 100"; //hopefully this simple.
            svg.Height = MainBoard!.GetViewHeight();
            Defs defs;
            LinearGradient linear;
            Stop stop;
            Ellipse ellipse;
            Path path;
            if (Square.IsFlipped)
            {
                if (Square.IsMine || Square.Flagged)
                {

                    ellipse = new Ellipse();
                    ellipse.Fill = "rgb(255,0,0)";
                    ellipse.CX = "50";
                    ellipse.CY = "50";
                    ellipse.RX = "25";
                    ellipse.RY = "25";
                    svg.Children.Add(ellipse);
                    defs = new Defs();
                    svg.Children.Add(defs);
                    linear = new LinearGradient();
                    linear.ID = "gradient_0";
                    linear.GradientUnits = "userSpaceOnUse";
                    linear.X1 = "0";
                    linear.Y1 = "0";
                    linear.X2 = "100";
                    linear.Y2 = "100";
                    defs.Children.Add(linear);
                    stop = new Stop();
                    stop.Offset = "0";
                    stop.Stop_Color = "rgb(255,255,255)";
                    stop.Stop_Opacity = ".39215687";
                    linear.Children.Add(stop);
                    stop = new Stop();
                    stop.Offset = "1";
                    stop.Stop_Color = "rgb(0,0,0)";
                    stop.Stop_Opacity = ".39215687";
                    linear.Children.Add(stop);
                    ellipse = new Ellipse();
                    ellipse.Fill = "url(#gradient_0)";
                    ellipse.CX = "50";
                    ellipse.CY = "50";
                    ellipse.RX = "25";
                    ellipse.RY = "25";
                    svg.Children.Add(ellipse);
                }
                else if (Square.NeighborMines > 0)
                {
                    Text text = new Text();

                    text.CenterText();
                    text.Font_Size = 75;
                    text.Fill = cc.Aqua.ToWebColor();
                    text.PopulateStrokesToStyles(strokeWidth: 4, fontFamily: "Verdana");
                    text.Content = Square.NeighborMines.ToString();
                    svg.Children.Add(text);
                }
                if (Square.Flagged)
                {
                    //this is for the flag portion.

                    path = new Path();
                    path.PopulateStrokesToStyles("rgb(0,0,0)", 5);
                    path.Fill = "none";
                    path.D = "M0 0L100 100";
                    svg.Children.Add(path);
                    path = new Path();
                    path.PopulateStrokesToStyles("rgb(0,0,0)", 5);
                    path.Fill = "none";
                    path.D = "M100 0L0 100";
                    svg.Children.Add(path);
                }
            }
            else
            {
                string firstColor;
                string secondColor;
                string currentColor;
                if (Square.Pressed)
                {
                    firstColor = "rgb(0,0,0)";
                    secondColor = "rgb(255,255,255)";
                    currentColor = cc.DarkGray;
                }
                else
                {
                    firstColor = "rgb(255,255,255)";
                    secondColor = "rgb(0,0,0)";
                    currentColor = cc.SlateGray;
                }
                defs = new Defs();
                svg.Children.Add(defs);
                linear = new LinearGradient();
                linear.ID = "gradient_0";
                linear.GradientUnits = "userSpaceOnUse";
                linear.X1 = "0";
                linear.Y1 = "0";
                linear.X2 = "100";
                linear.Y2 = "100";
                defs.Children.Add(linear);
                stop = new Stop();
                stop.Offset = "0";
                stop.Stop_Color = firstColor;
                stop.Stop_Opacity = ".58823532";
                linear.Children.Add(stop);
                stop = new Stop();
                stop.Offset = "1";
                stop.Stop_Color = secondColor;
                stop.Stop_Opacity = ".58823532";
                linear.Children.Add(stop);
                Rect rect = new Rect();
                rect.Width = "100";
                rect.Height = "100";
                rect.Fill = "url(#gradient_0)";
                svg.Children.Add(rect);
                rect = new Rect();
                rect.Fill = "none";
                rect.PopulateStrokesToStyles(currentColor, 4);
                rect.X = "16.666666";
                rect.Y = "16.666666";
                rect.Width = "66.6666664";
                rect.Height = "66.666664";
                svg.Children.Add(rect);
                if (Square.Flagged)
                {
                    //the flag drawings.
                    path = new Path();
                    path.Fill = "rgb(255,0,0)";
                    path.D = "M50 25L75 37.5L50 50";
                    svg.Children.Add(path);
                    path = new Path();
                    path.Fill = "none";
                    path.PopulateStrokesToStyles("rgb(255,0,0)", 5);
                    path.D = "M50 75L50 25";
                    svg.Children.Add(path);
                }
            }
            Rect final = new Rect();
            final.Width = "100%";
            final.Height = "100%";
            final.Fill = "none";
            final.PopulateStrokesToStyles("rgb(112,128,144)", 4);
            svg.Children.Add(final);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
        protected override bool ShouldRender()
        {
            MineRecord current = GetRecord();
            return !current.Equals(_previous);
        }
    }
}