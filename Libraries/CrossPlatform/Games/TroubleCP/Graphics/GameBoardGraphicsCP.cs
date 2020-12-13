using BasicGameFrameworkLibrary.Attributes;
using System.Collections.Generic;
using System.Drawing;
namespace TroubleCP.Graphics
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        public SizeF OriginalSize => new SizeF(600, 600);
        public SizeF SpaceSize => new SizeF(30, 30);
        public GameBoardGraphicsCP()
        {
            CreateSpaces();
        }
        public Dictionary<int, RectangleF> SpaceList = new Dictionary<int, RectangleF>();
        public RectangleF DiceRectangle { get; private set; }
        public PointF RecommendedPointForDice
        {
            get
            {
                var pt_Center = new PointF(OriginalSize.Width / 2, OriginalSize.Height / 2);
                return new PointF(pt_Center.X - (OriginalSize.Width / 10), pt_Center.Y - (OriginalSize.Height / 12));
            }
        }
        public float SuggestDiceHeight => DiceRectangle.Height * .75f;
        private void CreateSpaces()
        {
            SpaceList.Clear();
            var bounds = new RectangleF(0, 0, 600, 600);
            PointF pt_Center = new PointF(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
            DiceRectangle = new RectangleF(pt_Center.X - (bounds.Width / 8), pt_Center.Y - (bounds.Height / 8), bounds.Width / 4, bounds.Height / 4);
            float int_Size;
            int int_Count;
            int_Size = SpaceSize.Width;
            float int_Offset;
            float int_OffsetShort;
            // ********************************
            // *** Draw home spaces
            int_Offset = bounds.Width / 6;
            // *** Blue home
            AddRectangle(1, bounds.Width - int_Offset - int_Size * 2, int_Offset - (int_Size * 2));
            AddRectangle(2, bounds.Width - int_Offset - int_Size, int_Offset - (int_Size));
            AddRectangle(3, bounds.Width - int_Offset, int_Offset);
            AddRectangle(4, bounds.Width - int_Offset + int_Size, int_Offset + int_Size);
            // *** green home
            AddRectangle(5, bounds.Width - int_Offset - int_Size * 2, bounds.Height - int_Offset + int_Size);
            AddRectangle(6, bounds.Width - int_Offset - int_Size, bounds.Height - int_Offset);
            AddRectangle(7, bounds.Width - int_Offset, bounds.Height - int_Offset - int_Size);
            AddRectangle(8, bounds.Width - int_Offset + int_Size, bounds.Height - int_Offset - int_Size * 2);
            // *** red home
            AddRectangle(12, int_Offset + int_Size, bounds.Height - int_Offset + int_Size);
            AddRectangle(11, int_Offset, bounds.Height - int_Offset);
            AddRectangle(10, int_Offset - int_Size, bounds.Height - int_Offset - int_Size);
            AddRectangle(9, int_Offset - (int_Size * 2), bounds.Height - int_Offset - int_Size * 2);
            // *** yellow home
            AddRectangle(13, int_Offset - (int_Size * 2), int_Offset + int_Size);
            AddRectangle(14, int_Offset - int_Size, int_Offset);
            AddRectangle(15, int_Offset, int_Offset - int_Size);
            AddRectangle(16, int_Offset + int_Size, int_Offset - (int_Size * 2));
            // ********************************
            // *** Draw edge spaces
            int_Offset = bounds.Width / 7;
            // *** Bottom
            for (int_Count = -2; int_Count <= 2; int_Count++)
            {
                AddRectangle(20 + int_Count, pt_Center.X - (int_Size / 2) - (int_Count * (int_Size * 1.6f)), bounds.Height - int_Offset);
            }
            // *** left
            for (int_Count = -2; int_Count <= 2; int_Count++)
            {
                AddRectangle(27 + int_Count, int_Offset - int_Size, pt_Center.Y - (int_Size / 2) - (int_Count * (int_Size * 1.6f)));
            }
            // *** top
            for (int_Count = -2; int_Count <= 2; int_Count++)
            {
                AddRectangle(34 + int_Count, pt_Center.X - (int_Size / 2) + (int_Count * (int_Size * 1.6f)), int_Offset - int_Size);
            }
            // *** right
            for (int_Count = -2; int_Count <= 2; int_Count++)
            {
                AddRectangle(41 + int_Count, bounds.Width - int_Offset, pt_Center.Y - (int_Size / 2) + (int_Count * (int_Size * 1.6f)));
            }
            // ********************************
            // *** Draw finish spaces
            int_Offset = bounds.Width * 0.22f;
            // *** Green
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(45 + int_Count, bounds.Width - int_Offset - int_Size - (int_Size * int_Count * 0.9f), bounds.Height - int_Offset - int_Size - (int_Size * int_Count * 0.9f));
            }
            // *** red
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(49 + int_Count, int_Offset + (int_Size * int_Count * 0.9f), bounds.Height - int_Offset - int_Size - (int_Size * int_Count * 0.9f));
            }
            // *** yellow
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(53 + int_Count, int_Offset + (int_Size * int_Count * 0.9f), int_Offset + (int_Size * int_Count * 0.9f));
            }
            // *** blue
            for (int_Count = 0; int_Count <= 3; int_Count++)
            {
                AddRectangle(57 + int_Count, bounds.Width - int_Offset - int_Size - (int_Size * int_Count * 0.9f), int_Offset + (int_Size * int_Count * 0.9f));
            }
            // ********************************
            // *** Draw odd spaces
            int_Offset = bounds.Width * 0.24f;
            int_OffsetShort = bounds.Width * 0.14f;
            AddRectangle(17, bounds.Width - int_Offset - int_Size, bounds.Height - int_OffsetShort - int_Size);
            AddRectangle(23, int_Offset, bounds.Height - int_OffsetShort - int_Size);
            AddRectangle(24, int_OffsetShort, bounds.Height - int_Offset - int_Size);
            AddRectangle(30, int_OffsetShort, int_Offset);
            AddRectangle(31, int_Offset, int_OffsetShort);
            AddRectangle(37, bounds.Width - int_Size - int_Offset, int_OffsetShort);
            AddRectangle(38, bounds.Width - int_Size - int_OffsetShort, int_Offset);
            AddRectangle(44, bounds.Width - int_Size - int_OffsetShort, bounds.Height - int_Size - int_Offset);
        }
        private void AddRectangle(int spacenumber, float left, float tops)
        {
            var output = new RectangleF(left, tops, SpaceSize.Width, SpaceSize.Height);
            SpaceList.Add(spacenumber, output);
        }
    }
}