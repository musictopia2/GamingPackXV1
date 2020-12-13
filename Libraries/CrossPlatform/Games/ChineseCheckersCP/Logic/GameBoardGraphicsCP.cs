using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.Exceptions;
using System.Collections.Generic;
using System.Drawing;
namespace ChineseCheckersCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        public Dictionary<int, RectangleF> SpaceList { get; } = new Dictionary<int, RectangleF>();
        public GameBoardGraphicsCP()
        {
            CreateSpaces();
        }
        public SizeF OriginalSize { get; set; } = new SizeF(600, 600); //will use 600 by 600.
        public float PieceHeight { get; private set; }
        public float PieceWidth { get; private set; }
        #region "Positioning Info"
        private int _int_SpaceCount = 0;
        public PointF LocationOfSpace(int index)
        {
            return SpaceList[index].Location;
        }
        private void CreateSpaces()
        {
            SpaceList.Clear();
            var bounds = new RectangleF(0, 0, OriginalSize.Width, OriginalSize.Height);
            _int_SpaceCount = 1; // start with one.  because this is one based
            AddLine(1, 1, true, bounds);
            AddLine(2, 2, false, bounds);
            AddLine(3, 3, true, bounds);
            AddLine(4, 4, false, bounds);
            AddLine(5, 13, true, bounds);
            AddLine(6, 12, false, bounds);
            AddLine(7, 11, true, bounds);
            AddLine(8, 10, false, bounds);
            AddLine(9, 9, true, bounds);
            AddLine(10, 10, false, bounds);
            AddLine(11, 11, true, bounds);
            AddLine(12, 12, false, bounds);
            AddLine(13, 13, true, bounds);
            AddLine(14, 4, false, bounds);
            AddLine(15, 3, true, bounds);
            AddLine(16, 2, false, bounds);
            AddLine(17, 1, true, bounds);
            if (SpaceList.Count == 0)
            {
                throw new BasicBlankException("Failed to create spacelist");
            }
            PieceHeight = (int)SpaceList[1].Height;
            PieceWidth = (int)SpaceList[1].Width;
        }
        private void AddLine(int int_Row, int int_Spaces, bool bln_HasCenter, RectangleF bounds)
        {
            double int_Size;
            double int_OffsetX;
            double int_OffsetY;
            PointF pt_Center = new PointF(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
            int_Size = (int)bounds.Width / 32;
            int_OffsetX = int_Size;
            int_OffsetY = int_Size / 0.577350269; // divide by the tangent of 30 degrees
            var loopTo = int_Spaces;
            int int_Count;
            for (int_Count = 1; int_Count <= loopTo; int_Count++)
            {
                double int_OffsetFactorY = -(int_OffsetY * (int_Row - 9));
                int int_OffsetFactorX;
                RectangleF rect;
                if (bln_HasCenter)
                {
                    int_OffsetFactorX = (int_Count - ((int_Spaces + 1) / 2)) * 2;
                    rect = new RectangleF(pt_Center.X - ((float)int_Size / 2) + ((float)int_Size * int_OffsetFactorX), pt_Center.Y - ((float)int_Size / 2) + (float)int_OffsetFactorY, (float)int_Size, (float)int_Size);
                }
                else
                {
                    int_OffsetFactorX = int_Count - (int_Spaces / 2);
                    rect = new RectangleF(pt_Center.X - ((float)int_Size / 2) - (float)int_OffsetX + ((float)int_OffsetX * int_OffsetFactorX * 2), pt_Center.Y - ((float)int_Size / 2) + (float)int_OffsetFactorY, (float)int_Size, (float)int_Size);
                }
                SpaceList.Add(_int_SpaceCount, rect);
                _int_SpaceCount += 1;
            }
        }
        #endregion
    }
}