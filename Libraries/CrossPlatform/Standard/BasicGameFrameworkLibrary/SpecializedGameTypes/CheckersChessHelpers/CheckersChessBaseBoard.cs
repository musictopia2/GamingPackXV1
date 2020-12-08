using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Drawing;
using System.Linq;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers
{
    public abstract class CheckersChessBaseBoard<E, S>
        where E : struct, Enum
        where S : CheckersChessSpace<E>, new()
    {
        public static SizeF OriginalSize = new SizeF(500, 500);
        public static CustomBasicList<S> PrivateSpaceList = new CustomBasicList<S>(); // looks like blazor needs to reference this as well.
        private static string _firstColor = "";
        private static string _secondColor = "";
        public static bool HasGreen { get; set; }
        public CheckersChessBaseBoard()
        {
            PrivateSpaceList.Clear(); //try this too.
            _thisGame = GetGame();
            SetUpColors();
            CreateSpaces();
        }
        private void SetUpColors()
        {
            if (_thisGame == EnumCheckerChessGame.Checkers)
            {
                _firstColor = cc.White;
                _secondColor = cc.Black;
                HasGreen = false;
            }
            else
            {
                _firstColor = cc.WhiteSmoke;
                _secondColor = cc.Tan;
                HasGreen = true;
            }
        }

        public PointF SuggestedOffLocation(bool isReversed)
        {
            if (isReversed == true)
            {
                return new PointF(0, 0);
            }
            return new PointF(0, OriginalSize.Height);
        }
        public static int GetIndexByPoint(int row, int column)
        {
            int x;
            int y;
            int z = 0;
            for (x = 1; x <= 8; x++)
            {
                for (y = 1; y <= 8; y++)
                {
                    z += 1;
                    if (x == row && y == column)
                    {
                        return z;
                    }
                }
            }
            throw new BasicBlankException("Nothing found");
        }
        public static int GetRealIndex(int originalIndex, bool isReversed)
        {
            if (originalIndex == 0)
            {
                return 0;
            }
            if (isReversed == false)
            {
                return originalIndex;
            }
            var thisSpace = (from Items in PrivateSpaceList
                             where Items.MainIndex == originalIndex
                             select Items).Single();
            return thisSpace.ReversedIndex;
        }
        public static S GetSpace(int originalIndex, bool isReversed)
        {
            if (isReversed == false)
            {
                return (from Items in PrivateSpaceList
                        where Items.MainIndex == originalIndex
                        select Items).Single();
            }
            return (from Items in PrivateSpaceList
                    where Items.ReversedIndex == originalIndex
                    select Items).Single();
        }
        public static S? GetSpace(int row, int column)
        {
            if (row < 1 || row > 8)
            {
                return null;
            }
            if (column < 1 || column > 8)
            {
                return null;
            }
            return (from Items in PrivateSpaceList
                    where Items.Row == row && Items.Column == column
                    select Items).Single();
        }

        protected RectangleF GetBounds = new RectangleF(0, 0, 500, 500);
        public static int LongestSize => 500 / 8;
        private void CreateSpaces()
        {
            int x;
            int y;
            string rowPaint;
            string thisPaint;
            float locX;
            float locY;
            var thisBounds = GetBounds;
            var diffs = thisBounds.Width / 8;
            locY = 0;
            rowPaint = _firstColor!;
            for (x = 1; x <= 8; x++)
            {
                thisPaint = rowPaint!;
                locX = 0;
                for (y = 1; y <= 8; y++)
                {
                    S thisSpace = new S();
                    thisSpace.ThisRect = new RectangleF(locX, locY, diffs, diffs);
                    thisSpace.Column = y;
                    thisSpace.Row = x;
                    thisSpace.MainIndex = GetIndexByPoint(x, y);
                    thisSpace.ReversedIndex = GetIndexByPoint(9 - x, 9 - y);
                    thisSpace.Color = thisPaint;
                    if (thisPaint!.Equals(_firstColor) == true)
                    {
                        thisPaint = _secondColor!;
                    }
                    else
                    {
                        thisPaint = _firstColor!;
                    }
                    PrivateSpaceList.Add(thisSpace);
                    locX += diffs;
                }
                if (rowPaint!.Equals(_firstColor) == true)
                {
                    rowPaint = _secondColor!;
                }
                else
                {
                    rowPaint = _firstColor!;
                }
                locY += diffs;
            }
        }

        public static CustomBasicList<int> GetBlackStartingSpaces()
        {
            CustomBasicList<int> newList = new CustomBasicList<int>();
            for (var x = 6; x <= 8; x++)
            {
                for (var y = 1; y <= 8; y++)
                {
                    var thisSpace = GetSpace(x, y);
                    if (thisSpace!.Color!.Equals(_secondColor) == true)
                    {
                        newList.Add(thisSpace.MainIndex);
                    }
                }
            }
            return newList;
        }
        public abstract bool CanHighlight(); // so if you can't won't even both doing that part.
        private readonly EnumCheckerChessGame _thisGame;
        public abstract EnumCheckerChessGame GetGame();
        public static int SpaceSelected { get; set; } // this is used so it can do something different.
        public void ClearBoard()
        {
            foreach (var thisSpace in PrivateSpaceList)
            {
                thisSpace.ClearSpace();
            }
            AfterClearBoard();
        }
        protected abstract void AfterClearBoard();
    }
}