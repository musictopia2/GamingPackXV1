using BasicGamingUIBlazorLibrary.GameGraphics.Base;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.GameGraphics.GamePieces
{
    public enum EnumCheckerPieceCategory
    {
        SinglePiece = 1,
        CrownedPiece = 2,
        OnlyPiece = 3,
        FlatPiece = 4
    }
    internal record CheckerRecord(string Color, EnumCheckerPieceCategory PieceCategory, bool HasImage, bool IsSelected, bool Enabled);
    public class CheckerPiece : ComponentBase
    {

        CheckerRecord? _previousRecord;

        protected override bool ShouldRender()
        {
            if (MainGraphics!.Animating)
            {
                return true; //because you are doing animations.
            }
            var current = GetRecord();
            return current.Equals(_previousRecord) == false;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            _previousRecord = GetRecord();
            base.OnAfterRender(firstRender);
        }
        private CheckerRecord GetRecord()
        {
            return new CheckerRecord(MainColor, PieceCategory, HasImage, MainGraphics!.IsSelected, MainGraphics.CustomCanDo.Invoke());
        }
        [CascadingParameter]
        public BasePieceGraphics? MainGraphics { get; set; }

        [Parameter]
        public string MainColor { get; set; } = cc.Transparent; //if not set, then nothing will show obviously.

        [Parameter]
        public EnumCheckerPieceCategory PieceCategory { get; set; } = EnumCheckerPieceCategory.OnlyPiece; //only game of checkers has to do with single/crowned.

        [Parameter]
        public bool HasImage { get; set; } = true;

        [Parameter]
        public string BlankColor { get; set; } = cc.White;

        protected override void OnInitialized()
        {
            MainGraphics!.OriginalSize = new SizeF(300, 300); //decided to use 300 by 300 this time.
            MainGraphics.BorderWidth = 10;
            MainGraphics.HighlightTransparent = true;
            base.OnInitialized();
        }
        private void PopulateBlank(ISvg svg)
        {
            var circle = new Circle();
            circle.CX = "150";
            circle.CY = "150";
            circle.R = "150";
            circle.Fill = BlankColor.ToWebColor();
            svg.Children.Add(circle);
        }
        private void BuildDefs(ISvg svg)
        {
            //do defs first.
            Defs defs = new Defs();
            LinearGradient linear = new LinearGradient();
            linear.ID = "gradient_0";
            linear.GradientUnits = "userSpaceOnUse";
            linear.X1 = "0";
            if (PieceCategory == EnumCheckerPieceCategory.FlatPiece)
            {
                linear.Y1 = "299";
            }
            else
            {
                linear.Y1 = "300";
            }
            linear.X2 = "300";
            linear.Y2 = linear.Y1;

            defs.Children.Add(linear);
            svg.Children.Add(defs); //hopefully this simple.
            Stop stop = new Stop();
            stop.Offset = "0";
            stop.Stop_Color = "rgb(255,255,255)";
            stop.Stop_Opacity = ".39215687";
            linear.Children.Add(stop);
            stop = new Stop();
            stop.Offset = "1";
            stop.Stop_Color = "rgb(0,0,0)";
            stop.Stop_Opacity = ".58823532";
            linear.Children.Add(stop);
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            ISvg svg = MainGraphics!.GetMainSvg(false); //try to set this to false (?)

            SvgRenderClass render = new SvgRenderClass();
            if (HasImage == false)
            {
                PopulateBlank(svg);
                render.RenderSvgTree(svg, 0, builder);
                return;
            }
            BuildDefs(svg); //forgot this too.
            if (PieceCategory == EnumCheckerPieceCategory.FlatPiece)
            {
                PopulateFlatPiece(svg);
                render.RenderSvgTree(svg, 0, builder);
                return;
            }
            PopulateCrownOrRegular(svg);
            render.RenderSvgTree(svg, 0, builder);
            base.BuildRenderTree(builder);
        }
        private void PopulateCrownOrRegular(ISvg svg)
        {
            Ellipse ellipse; //looks like was not necessary to change the numbers this time.  that was good.
            ellipse = new Ellipse();
            ellipse.Fill = MainColor.ToWebColor();
            ellipse.CX = "150";
            ellipse.CY = "178.5";
            ellipse.RX = "150";
            ellipse.RY = "121";
            svg.Children.Add(ellipse);
            if (PieceCategory != EnumCheckerPieceCategory.OnlyPiece)
            {
                ellipse = new Ellipse();
                PopulateEllipseIncludingStroke(ellipse, svg, GetGradientID, 150, 178.5, 150, 121);
                ellipse = new Ellipse();
                PopulateEllipseIncludingStroke(ellipse, svg, MainColor.ToWebColor(), 150, 151.5, 150, 121);
            }
            if (PieceCategory == EnumCheckerPieceCategory.OnlyPiece)
            {
                ellipse = new Ellipse();
                PopulateEllipseIncludingStroke(ellipse, svg, GetGradientID, 150, 150, 150, 150);
                ellipse = new Ellipse();
                PopulateEllipseIncludingStroke(ellipse, svg, MainColor.ToWebColor(), 150, 135, 150, 135);
            }
            if (PieceCategory == EnumCheckerPieceCategory.CrownedPiece)
            {
                ellipse = new Ellipse();
                PopulateEllipseIncludingStroke(ellipse, svg, GetGradientID, 150, 148.5, 150, 121);
                ellipse = new Ellipse();
                PopulateEllipseIncludingStroke(ellipse, svg, MainColor.ToWebColor(), 150, 121.5, 150, 121);
            }
        }
        private static string GetGradientID => "url(#gradient_0)";
        private void PopulateFlatPiece(ISvg svg)
        {
            var ellipse = new Ellipse();
            ellipse.Fill = MainColor.ToWebColor();
            ellipse.CX = "150";
            ellipse.CY = "252.25";
            ellipse.RX = "150";
            ellipse.RY = "46";
            svg.Children.Add(ellipse);
            ellipse = new Ellipse();
            PopulateEllipseIncludingStroke(ellipse, svg, GetGradientID, 150, 252.25, 150, 46);
            Rect rect = new Rect();
            rect.Fill = MainColor.ToWebColor();
            rect.Y = "158.75";
            rect.Width = "300";
            rect.Height = "93.5";
            svg.Children.Add(rect);

            rect = new Rect();
            rect.Fill = GetGradientID;
            rect.Y = "158.75";
            rect.Width = "300";
            rect.Height = "93.5";
            svg.Children.Add(rect);

            Path path = new Path();
            path.Fill = "none";
            path.PopulateStrokesToStyles(strokeWidth: (int)MainGraphics!.BorderWidth);
            path.D = "M0 158.75 L0 252.25";
            svg.Children.Add(path);
            path = new Path();
            path.Fill = "none";
            path.PopulateStrokesToStyles(strokeWidth: (int)MainGraphics!.BorderWidth);
            path.D = "M300 158.75 L300 252.25";
            svg.Children.Add(path);
            ellipse = new Ellipse();
            PopulateEllipseIncludingStroke(ellipse, svg, MainColor.ToWebColor(), 150, 158.75, 150, 46);
        }
        private void PopulateEllipseIncludingStroke(Ellipse ellipse, ISvg svg, string color, double cx, double cy, double rx, double ry)
        {
            ellipse.PopulateStrokesToStyles(strokeWidth: (int)MainGraphics!.BorderWidth);
            ellipse.CX = cx.ToString();
            ellipse.CY = cy.ToString();
            ellipse.Fill = color;
            ellipse.RX = rx.ToString();
            ellipse.RY = ry.ToString();
            svg.Children.Add(ellipse);
        }
    }
}