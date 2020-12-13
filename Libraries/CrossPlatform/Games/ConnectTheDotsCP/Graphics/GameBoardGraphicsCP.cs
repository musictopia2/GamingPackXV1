using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.Exceptions;
using ConnectTheDotsCP.Data;
using System.Collections.Generic;
using System.Drawing;
namespace ConnectTheDotsCP.Graphics
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        private readonly ConnectTheDotsGameContainer _gameContainer;
        public GameBoardGraphicsCP(ConnectTheDotsGameContainer gameContainer)
        {
            _gameContainer = gameContainer;
            if (_gameContainer.DotList == null)
            {
                InitBoard(_gameContainer);
            }
            LoadSpaces();
        }
        public SizeF OriginalSize { get; private set; } = new SizeF(480, 480);
        public float SquareHeight { get; private set; }
        public float SpaceHeight { get; private set; }
        internal static void InitBoard(ConnectTheDotsGameContainer gameContainer)
        {
            InitDots(gameContainer);
            InitSquares(gameContainer);
            InitLines(gameContainer);
        }
        private static void InitDots(ConnectTheDotsGameContainer gameContainer)
        {
            gameContainer.DotList = new Dictionary<int, DotInfo>();
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    DotInfo thisDot = new DotInfo();
                    thisDot.Row = x;
                    thisDot.Column = y;
                    gameContainer.DotList.Add(gameContainer.DotList.Count + 1, thisDot);
                }
            }
        }
        private static void InitSquares(ConnectTheDotsGameContainer gameContainer)
        {
            gameContainer.SquareList = new Dictionary<int, SquareInfo>();
            for (int x = 1; x <= 7; x++)
            {
                for (int y = 1; y <= 7; y++)
                {
                    SquareInfo thisSquare = new SquareInfo();
                    thisSquare.Row = x;
                    thisSquare.Column = y;
                    thisSquare.Color = 0;
                    gameContainer.SquareList.Add(gameContainer.SquareList.Count + 1, thisSquare);
                }
            }
        }
        private static void InitLines(ConnectTheDotsGameContainer gameContainer)
        {
            gameContainer.LineList = new Dictionary<int, LineInfo>();
            LineInfo thisLine;
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 7; y++)
                {
                    thisLine = new LineInfo();
                    thisLine.Horizontal = true;
                    thisLine.Row = x;
                    thisLine.Column = y;
                    thisLine.DotRow1 = x;
                    thisLine.DotColumn1 = y;
                    thisLine.DotRow2 = x;
                    thisLine.DotColumn2 = y + 1;
                    thisLine.Index = gameContainer.LineList.Count + 1;
                    gameContainer.LineList.Add(thisLine.Index, thisLine);
                }
                if (x != 8)
                {
                    for (int y = 1; y <= 8; y++)
                    {
                        thisLine = new LineInfo();
                        thisLine.Horizontal = false;
                        thisLine.Row = x;
                        thisLine.Column = y;
                        thisLine.DotRow1 = x;
                        thisLine.DotColumn1 = y;
                        thisLine.DotRow2 = x + 1;
                        thisLine.DotColumn2 = y;
                        thisLine.Index = gameContainer.LineList.Count + 1;
                        gameContainer.LineList.Add(thisLine.Index, thisLine);
                    }
                }
            }
        }
        public RectangleF WhiteRect { get; private set; }
        private void LoadDots() //this loads in the dot and bounds.
        {
            if (_gameContainer.DotList!.Count == 0)
            {
                throw new BasicBlankException("Should have initialized dots first.  Rethink");
            }
            int z = 0;
            var dotSize = new SizeF(10, 10);
            float currentHiddenLeft;
            float currentHiddenTop;
            float diffs = SpaceHeight - SquareHeight;
            float currentTop;
            float currentLeft;
            var bigHeight = new SizeF(40, 40);
            currentHiddenTop = diffs + WhiteRect.Location.Y - diffs;
            currentTop = diffs + WhiteRect.Location.Y;
            for (int x = 1; x <= 8; x++)
            {
                currentHiddenLeft = WhiteRect.Location.X - diffs;
                currentLeft = diffs + WhiteRect.Location.X;
                for (int y = 1; y <= 8; y++)
                {
                    z++;
                    DotInfo thisDot = _gameContainer.DotList[z];
                    thisDot.Dot = new RectangleF(currentLeft, currentTop, dotSize.Width, dotSize.Height);
                    thisDot.Bounds = new RectangleF(currentLeft - (bigHeight.Width / 3), currentTop - (bigHeight.Height / 3), bigHeight.Width, bigHeight.Height);
                    currentLeft += SpaceHeight;
                    currentHiddenLeft += SpaceHeight;
                }
                currentTop += SpaceHeight;
                currentHiddenTop += SpaceHeight;
            }
        }
        private void LoadSquares()
        {
            if (_gameContainer.SquareList!.Count == 0)
            {
                throw new BasicBlankException("Should have loaded the squares first.  Rethink");
            }
            int z = 0;
            float currentLeft;
            float currentTop;
            var startSize = new SizeF(15, 15);
            currentTop = startSize.Height + WhiteRect.Location.Y;
            for (int x = 1; x <= 7; x++)
            {
                currentLeft = startSize.Width + WhiteRect.Location.X;
                for (int y = 1; y <= 7; y++)
                {
                    z++;
                    SquareInfo thisSquare = _gameContainer.SquareList[z];
                    thisSquare.Rectangle = new RectangleF(currentLeft, currentTop, SquareHeight, SquareHeight);
                    currentLeft += SpaceHeight;
                }
                currentTop += SpaceHeight;
            }
        }
        private void LoadLines()
        {
            if (_gameContainer.LineList!.Count == 0)
            {
                throw new BasicBlankException("Should have loaded the lines.  Rethink");
            }
            int z = 0;
            LineInfo thisLine;
            float oldTop;
            float oldLeft;
            var lineSize = new SizeF(10, 10);
            float newTop;
            oldTop = lineSize.Width + WhiteRect.Location.Y;
            for (int x = 1; x <= 8; x++)
            {
                oldLeft = lineSize.Width + WhiteRect.Location.X;
                for (int y = 1; y <= 7; y++)
                {
                    z++;
                    thisLine = _gameContainer.LineList[z];
                    thisLine.StartingPoint = new PointF(oldLeft, oldTop);
                    oldLeft += SpaceHeight;
                    thisLine.FinishingPoint = new PointF(oldLeft, oldTop);
                }
                oldLeft = lineSize.Width + WhiteRect.Location.X;
                if (x != 8)
                {
                    for (int y = 1; y <= 8; y++)
                    {
                        z++;
                        thisLine = _gameContainer.LineList[z];
                        thisLine.StartingPoint = new PointF(oldLeft, oldTop);
                        newTop = oldTop + SpaceHeight;
                        thisLine.FinishingPoint = new PointF(oldLeft, newTop);
                        oldLeft += SpaceHeight;
                    }
                }
                oldTop += SpaceHeight;
            }
        }
        private void LoadSpaces()
        {
            WhiteRect = new RectangleF(10, 10, 470, 470);
            var thisSize = new SizeF(60, 60);
            SpaceHeight = thisSize.Height;
            thisSize = new SizeF(53, 53);
            SquareHeight = thisSize.Height;
            LoadSquares();
            LoadDots();
            LoadLines();
        }
    }
}