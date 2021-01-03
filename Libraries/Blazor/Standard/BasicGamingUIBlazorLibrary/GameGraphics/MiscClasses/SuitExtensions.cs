using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using SvgHelper.Blazor.Logic;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using SvgHelper.Blazor.Logic.Classes.SubClasses;
using System.Drawing;
namespace BasicGamingUIBlazorLibrary.GameGraphics.MiscClasses
{
    public static class SuitExtensions
    {
        public static void DrawCardSuit(this IParentGraphic parent, EnumSuitList suit, float x, float y, float width, float height, string color)
        {
            RectangleF rect = new RectangleF(x, y, width, height);
            parent.DrawCardSuit(suit, rect, color);
        }
        public static void DrawRoyalSuits(this IParentGraphic parent,RectangleF rectangle, string fillColor, string borderColor = "", int borderSize = 0)
        {
            ISvg svg = new SVG();
            svg.PopulateSVGStartingPoint(rectangle);
            svg.ViewBox = "0 0 792 792";
            Path path = new Path();
            svg.Children.Add(path);
            path.D = "M727,280.9c-16,0-29,13.5-29,30c0,8.4,3.3,15.9,8.6,21.4c-31.8,32.3-92.1,88.7-116.4,78.1c-21.5-9.4-26.4-52.4-27.1-82.3c0.2,0,0.3,0,0.5,0c16,0,28.9-13.5,28.9-30c0-16.6-13-30-28.9-30c-16,0-29,13.4-29,30c0,13.1,8.1,24.2,19.4,28.3c-12.9,29.8-40.3,69.5-89.8,47.2c-38.4-17.2-55.5-89.8-62.9-141.8c13.4-2.6,23.5-14.8,23.5-29.5c0-16.6-13-30.1-28.9-30.1c-16,0-29,13.4-29,30.1c0,14.7,10.1,26.9,23.5,29.5c-7.4,52.1-24.5,124.6-62.9,141.8c-49.5,22.3-76.9-17.4-89.8-47.2c11.3-4.1,19.4-15.2,19.4-28.3c0-16.6-13-30-29-30c-16,0-29,13.4-29,30c0,16.6,13,30,29,30c0.2,0,0.3,0,0.5,0c-0.7,30-5.6,73-27.1,82.3C177.3,421,117,364.6,85.3,332.3c5.3-5.5,8.6-13,8.6-21.4c0-16.6-13-30-29-30c-16,0-29,13.5-29,30c0,16.6,13,30,29,30c4.5,0,8.7-1.1,12.6-3c26.1,57.6,83.5,191.4,78,245.6c0,0-14.2,16.6,0,27.6c14.2,11,240.5,8.3,240.5,8.3s226.3,2.8,240.5-8.3c14.2-11.1,0-27.6,0-27.6c-5.5-54.2,51.9-188,78-245.6c3.8,1.9,8,3,12.6,3c16,0,29-13.4,29-30C756,294.3,743,280.9,727,280.9z";
            path.Fill = fillColor.ToWebColor();
            if (borderSize > 0)
            {
                string reals = borderColor.ToWebColor();
                path.PopulateStrokesToStyles(reals, borderSize);
            }
            parent.Children.Add(svg);
        }
        public static void DrawCardSuit(this IParentGraphic parent, EnumSuitList suit, RectangleF rectangle, string color)
        {
            if (suit == EnumSuitList.Clubs)
            {
                DrawClubs(parent, rectangle, color);
                return;
            }
            ISvg svg;
            if (suit == EnumSuitList.Spades)
            {

                svg = new SVG();
                svg.PopulateSVGStartingPoint(rectangle);
                svg.ViewBox = "68.547241 122.68109 537.42297 635.16461";
                parent.Children.Add(svg);
                Path path = new Path();
                svg.Children.Add(path);
                path.D = "m213.23 502.9c-195.31 199.54-5.3525 344.87 149.07 249.6.84137 49.146-37.692 95.028-61.394 138.9h166.73c-24.41-42.64-65.17-89.61-66.66-138.9 157.66 90.57 325.33-67.37 150.39-249.6-91.22-100.08-148.24-177.95-169.73-204.42-19.602 25.809-71.82 101.7-168.41 204.42z";
                path.Transform = "translate(-40.697 -154.41)";
                path.Fill = color.ToWebColor();
                return;
            }
            if (suit == EnumSuitList.Hearts)
            {
                svg = new SVG();
                svg.PopulateSVGStartingPoint(rectangle);
                svg.ViewBox = "0 0 40 40";
                parent.Children.Add(svg);
                Path path = new Path();
                svg.Children.Add(path);
                path.D = "m20 10c0.97-5 2.911-10 9.702-10 6.792 0 12.128 5 9.703 15-2.426 10-13.584 15-19.405 25-5.821-10-16.979-15-19.405-25-2.4254-10 2.9109-15 9.703-15 6.791 0 8.732 5 9.702 10z";
                path.Fill = color.ToWebColor();
                return;
            }
            if (suit == EnumSuitList.Diamonds)
            {
                svg = new SVG();
                svg.PopulateSVGStartingPoint(rectangle);
                svg.ViewBox = "0 0 127 175";
                parent.Children.Add(svg);
                G g = new G();
                svg.Children.Add(g);
                g.Transform = "translate(0,-877.36216)";
                Path path = new Path();
                g.Children.Add(path);
                path.D = "M 59.617823,1026.4045 C 54.076551,1017.027 35.802458,991.8393 22.320951,974.99722 15.544428,966.53149 10,959.28947 10,958.90385 c 0,-0.38562 2.498012,-3.68932 5.551138,-7.34155 14.779126,-17.67921 34.688967,-44.7342 42.813135,-58.17773 2.491067,-4.12211 4.836029,-7.13807 5.211026,-6.70213 0.374997,0.43594 3.911379,5.74741 7.858624,11.80326 8.617724,13.22128 27.37269,38.4164 38.049687,51.11535 l 7.73836,9.2038 - 7.73836,9.2038 c - 14.035208,16.69312 - 34.03523,44.26125 - 44.489713,61.32495 l - 1.855601,3.0286 - 3.520473,-5.9577 z";
                path.Fill = color.ToWebColor();
                return;
            }
        }
        private static void DrawClubs(IParentGraphic parent, RectangleF rectangle, string color)
        {
            ISvg svg = new SVG();
            svg.ViewBox = "0 0 400 400";
            svg.PopulateSVGStartingPoint(rectangle);
            parent.Children.Add(svg);
            Circle circle = new Circle();
            svg.Children.Add(circle);
            circle.PopulateCircle(125, 0, 150, color);
            circle = new Circle();
            svg.Children.Add(circle);
            circle.PopulateCircle(0, 150, 150, color);
            circle = new Circle();
            svg.Children.Add(circle);
            circle.PopulateCircle(250, 150, 150, color);
            Path path = new Path();
            path.Fill = color.ToWebColor();
            svg.Children.Add(path);
            path.D = "M 185 150 Q 185 150 150 200 L 150 250 Q 175 270 175 280 L 150 400 L 250 400 Q 225 350 225 280 Q 225 270 250 250 L 250 200 Q 230 180 220 150 C";
        }
    }
}