using BasicGameFrameworkLibrary.Attributes;
using System.Collections.Generic;
using System.Drawing;
namespace AggravationCP.Graphics
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        public SizeF OriginalSize => new SizeF(600, 600);

        public Dictionary<int, RectangleF> SpaceList = new Dictionary<int, RectangleF>();
        public GameBoardGraphicsCP()
        {
            CreateSpaces();
        }
        private void CreateSpaces()
        {
            SpaceList.Clear();
            var bounds = new RectangleF(0, 0, 600, 600);
            float int_Size = SpaceSize.Width; //for compatibility.
            float Pointx;
            PointF pt_Center = new PointF(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
            AddRectangle(9, pt_Center.X - int_Size * 4, bounds.Top + (bounds.Width / 100));
            AddRectangle(10, pt_Center.X - int_Size * 1.75f, bounds.Top + (bounds.Width / 100));
            AddRectangle(11, pt_Center.X + int_Size * 0.75f, bounds.Top + (bounds.Width / 100));
            AddRectangle(12, pt_Center.X + int_Size * 3, bounds.Top + (bounds.Width / 100));
            float int_CenterOffset;
            int int_Count;
            int_CenterOffset = bounds.Width / 15;
            Pointx = bounds.Width - int_Size - (bounds.Width / 100);
            Pointx += bounds.Left; // i think
            AddRectangle(13, Pointx, (pt_Center.Y) - int_Size * 4);
            AddRectangle(14, Pointx, pt_Center.Y - int_Size * 1.75f);
            AddRectangle(15, Pointx, pt_Center.Y + int_Size * 0.75f);
            AddRectangle(16, Pointx, pt_Center.Y + int_Size * 3);
            AddRectangle(4, pt_Center.X - int_Size * 4, bounds.Height - int_Size - (bounds.Width / 100));
            AddRectangle(3, pt_Center.X - int_Size * 1.75f, bounds.Height - int_Size - (bounds.Width / 100));
            AddRectangle(2, pt_Center.X + int_Size * 0.75f, bounds.Height - int_Size - (bounds.Width / 100));
            AddRectangle(1, pt_Center.X + int_Size * 3, bounds.Height + -int_Size - (bounds.Width / 100));
            AddRectangle(8, bounds.Left + (bounds.Width / 100), pt_Center.Y - int_Size * 4);
            AddRectangle(7, bounds.Left + (bounds.Width / 100), pt_Center.Y - int_Size * 1.75f);
            AddRectangle(6, bounds.Left + (bounds.Width / 100), pt_Center.Y + int_Size * 0.75f);
            AddRectangle(5, bounds.Left + (bounds.Width / 100), pt_Center.Y + int_Size * 3);
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(68 - int_Count, pt_Center.X - (int_Size / 2), pt_Center.Y + (int_CenterOffset + (int_Size)) + (int_Count * (int_Size * 2)));
            }
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(72 - int_Count, pt_Center.X - (int_CenterOffset + (int_Size * 2)) - (int_Count * (int_Size * 2)), pt_Center.Y - (int_Size / 2));
            }
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(76 - int_Count, pt_Center.X - (int_Size / 2), pt_Center.Y - (int_CenterOffset + (int_Size * 2)) - (int_Count * (int_Size * 2)));
            }
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(80 - int_Count, pt_Center.X + (int_CenterOffset + (int_Size)) + (int_Count * (int_Size * 2)), pt_Center.Y - (int_Size / 2));
            }
            // ************************************************************
            // *** Center space
            AddRectangle(81, pt_Center.X - (int_Size / 2), pt_Center.Y - (int_Size / 2));
            // ************************************************************
            // *** Normal spaces
            for (int_Count = 1; int_Count <= 5; int_Count++)
            {
                AddRectangle(58 + int_Count, pt_Center.X + int_CenterOffset, pt_Center.Y + int_CenterOffset + int_Count * (int_Size * 2));
            }
            AddRectangle(64, pt_Center.X - (int_Size / 2), pt_Center.Y + int_CenterOffset + (5 * (int_Size * 2)));
            for (int_Count = 0; int_Count <= 5; int_Count++)
            {
                AddRectangle(22 - int_Count, pt_Center.X - int_CenterOffset - int_Size, pt_Center.Y + int_CenterOffset + int_Count * (int_Size * 2));
            }
            for (int_Count = 1; int_Count <= 5; int_Count++)
            {
                AddRectangle(22 + int_Count, pt_Center.X - int_Size - int_CenterOffset - int_Count * (int_Size * 2), pt_Center.Y + int_CenterOffset);
            }
            AddRectangle(28, pt_Center.X - int_Size - int_CenterOffset - (5 * (int_Size * 2)), pt_Center.Y - (int_Size / 2));
            for (int_Count = 0; int_Count <= 5; int_Count++)
            {
                AddRectangle(34 - int_Count, pt_Center.X - int_Size - int_CenterOffset - int_Count * (int_Size * 2), pt_Center.Y - int_CenterOffset - int_Size);
            }
            for (int_Count = 1; int_Count <= 5; int_Count++)
            {
                AddRectangle(34 + int_Count, pt_Center.X - int_CenterOffset - int_Size, pt_Center.Y - int_Size - int_CenterOffset - int_Count * (int_Size * 2));
            }
            AddRectangle(40, pt_Center.X - (int_Size / 2), pt_Center.Y - int_Size - int_CenterOffset - (5 * (int_Size * 2)));
            for (int_Count = 0; int_Count <= 5; int_Count++)
            {
                AddRectangle(46 - int_Count, pt_Center.X + int_CenterOffset, pt_Center.Y - int_Size - int_CenterOffset - int_Count * (int_Size * 2));
            }
            for (int_Count = 1; int_Count <= 5; int_Count++)
            {
                AddRectangle(46 + int_Count, pt_Center.X + int_CenterOffset + int_Count * (int_Size * 2), pt_Center.Y - int_CenterOffset - int_Size);
            }
            AddRectangle(52, pt_Center.X + int_CenterOffset + (5 * (int_Size * 2)), pt_Center.Y - (int_Size / 2));
            for (int_Count = 0; int_Count <= 5; int_Count++)
            {
                AddRectangle(58 - int_Count, pt_Center.X + int_CenterOffset + int_Count * (int_Size * 2), pt_Center.Y + int_CenterOffset);
            }
        }
        public SizeF SpaceSize => new SizeF(20, 20);
        private void AddRectangle(int spacenumber, float left, float top)
        {
            var output = new RectangleF(left, top, SpaceSize.Width, SpaceSize.Height);
            SpaceList.Add(spacenumber, output);
        }
    }
}