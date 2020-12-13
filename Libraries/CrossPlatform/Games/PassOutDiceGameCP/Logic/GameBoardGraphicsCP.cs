using BasicGameFrameworkLibrary.Attributes;
using PassOutDiceGameCP.Data;
using System.Collections.Generic;
using System.Drawing;
namespace PassOutDiceGameCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        public SizeF OriginalSize => new SizeF(100, 539);
        public RectangleF GetBounds => new RectangleF(new PointF(0, 0), OriginalSize);
        public PassOutDiceGameGameContainer GameContainer;
        public GameBoardGraphicsCP(PassOutDiceGameGameContainer gameContainer)
        {
            GameContainer = gameContainer;
        }
        internal void CreateSpaceList()
        {
            GameContainer.SpaceList = new Dictionary<int, SpaceInfo>();
            int x;
            int y;
            int z = 0;
            for (x = 1; x <= 11; x++)
            {
                for (y = 1; y <= 2; y++)
                {
                    z += 1;
                    var thisSpace = new SpaceInfo();
                    if (z == 1)
                    {
                        thisSpace.IsEnabled = false;
                    }
                    else
                    {
                        thisSpace.IsEnabled = true;
                    }
                    GameContainer.SpaceList.Add(z, thisSpace);
                }
            }
            CreateSpaces();
        }
        public SizeF SpaceSize => new SizeF(45, 45);
        public RectangleF SpaceRectangle(SpaceInfo space)
        {
            return new RectangleF(space.Bounds.Location.X + 3, space.Bounds.Location.Y + 3, space.Bounds.Width - 6, space.Bounds.Height - 6);
        }
        public RectangleF PassRectangle(SpaceInfo space)
        {
            return new RectangleF(space.Bounds.Location.X, space.Bounds.Location.Y, space.Bounds.Width, space.Bounds.Height / 2);
        }
        public RectangleF OutRectangle(SpaceInfo space)
        {
            return new RectangleF(space.Bounds.Location.X, space.Bounds.Location.Y + (space.Bounds.Height / 2), space.Bounds.Width, space.Bounds.Height / 2);
        }
        public (PointF firstDiceLocation, PointF secondDiceLocation) GetDiceLocation(SpaceInfo space)
        {
            PointF firsts;
            PointF seconds;
            firsts = new PointF(space.Bounds.Location.X + 4, space.Bounds.Location.Y + 4);
            int diffs;
            diffs = 22;
            seconds = new PointF(space.Bounds.Location.X + diffs, space.Bounds.Location.Y + diffs);
            return (firsts, seconds);
        }
        private void CreateSpaces()
        {
            int x;
            int y;
            int z = 0;
            int lefts;
            SpaceInfo thisSpace;
            int tops = 0;
            for (x = 1; x <= 11; x++)
            {
                lefts = 8;
                for (y = 1; y <= 2; y++)
                {
                    z += 1;
                    thisSpace = GameContainer.SpaceList![z];
                    thisSpace.Bounds = new RectangleF(lefts, tops, SpaceSize.Width, SpaceSize.Height);
                    if (z == 1)
                    {
                        thisSpace.IsEnabled = false;
                    }
                    else
                    {
                        thisSpace.IsEnabled = true;
                    }
                    lefts += (int)SpaceSize.Width;
                }
                tops += (int)SpaceSize.Height;
            }
            FirstLoad();
        }
        private void FirstLoad()
        {
            SpaceInfo thisSpace;
            int x;
            var loopTo = GameContainer.SpaceList!.Count;
            for (x = 2; x <= loopTo; x++)
            {
                thisSpace = GameContainer.SpaceList[x];
                if (x == 2)
                {
                    thisSpace.FirstValue = 6;
                    thisSpace.SecondValue = 2;
                }
                else if (x == 3)
                {
                    thisSpace.FirstValue = 5;
                    thisSpace.SecondValue = 1;
                }
                else if (x == 4)
                {
                    thisSpace.FirstValue = 6;
                    thisSpace.SecondValue = 4;
                }
                else if (x == 5)
                {
                    thisSpace.FirstValue = 6;
                    thisSpace.SecondValue = 3;
                }
                else if (x == 6)
                {
                    thisSpace.FirstValue = 1;
                    thisSpace.SecondValue = 3;
                }
                else if (x == 7)
                {
                    thisSpace.FirstValue = 3;
                    thisSpace.SecondValue = 3;
                }
                else if (x == 8)
                {
                    thisSpace.FirstValue = 5;
                    thisSpace.SecondValue = 2;
                }
                else if (x == 9)
                {
                    thisSpace.FirstValue = 6;
                    thisSpace.SecondValue = 2;
                }
                else if (x == 10)
                {
                    thisSpace.FirstValue = 2;
                    thisSpace.SecondValue = 2;
                }
                else if (x == 11)
                {
                    thisSpace.FirstValue = 4;
                    thisSpace.SecondValue = 3;
                }
                else if (x == 12)
                {
                    thisSpace.FirstValue = 1;
                    thisSpace.SecondValue = 3;
                }
                else if (x == 13)
                {
                    thisSpace.FirstValue = 5;
                    thisSpace.SecondValue = 6;
                }
                else if (x == 14)
                {
                    thisSpace.FirstValue = 2;
                    thisSpace.SecondValue = 4;
                }
                else if (x == 15)
                {
                    thisSpace.FirstValue = 1;
                    thisSpace.SecondValue = 1;
                }
                else if (x == 16)
                {
                    thisSpace.FirstValue = 5;
                    thisSpace.SecondValue = 4;
                }
                else if (x == 17)
                {
                    thisSpace.FirstValue = 4;
                    thisSpace.SecondValue = 4;
                }
                else if (x == 18)
                {
                    thisSpace.FirstValue = 5;
                    thisSpace.SecondValue = 5;
                }
                else if (x == 19)
                {
                    thisSpace.FirstValue = 6;
                    thisSpace.SecondValue = 1;
                }
                else if (x == 20)
                {
                    thisSpace.FirstValue = 5;
                    thisSpace.SecondValue = 3;
                }
                else if (x == 21)
                {
                    thisSpace.FirstValue = 6;
                    thisSpace.SecondValue = 6;
                }
                else if (x == 22)
                {
                    thisSpace.FirstValue = 1;
                    thisSpace.SecondValue = 4;
                }
            }
        }
    }
}