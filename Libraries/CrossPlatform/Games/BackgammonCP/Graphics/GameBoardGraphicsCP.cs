using BackgammonCP.Data;
using BasicGameFrameworkLibrary.Attributes;
using System.Collections.Generic;
using System.Drawing;
namespace BackgammonCP.Graphics
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        public readonly BackgammonGameContainer GameContainer;
        public GameBoardGraphicsCP(BackgammonGameContainer gameContainer)
        {
            GameContainer = gameContainer;
            CreateSpaces();
        }
        public SizeF OriginalSize => new SizeF(571, 436);
        public Dictionary<int, RectangleF> SpaceList = new Dictionary<int, RectangleF>(); 
        public SizeF SpaceSize => new SizeF(30, 30);
        private void CreateRectangle(int id, float x, float y, float width, float height)
        {
            SpaceList.Add(id, new RectangleF(x, y, width, height));
        }
        private void CreateSpaces()
        {
            SpaceList.Clear();
            CreateRectangle(12, 12.847499f, 12.847499f, 37.542496f, 164.12199f);
            CreateRectangle(13, 12.847499f, 259.03049f, 37.542496f, 164.12201f);
            CreateRectangle(11, 50.389996f, 12.847499f, 37.542492f, 164.12199f);
            CreateRectangle(14, 50.389996f, 259.03049f, 37.542492f, 164.12201f);
            CreateRectangle(10, 87.932487f, 12.847499f, 37.542496f, 164.12199f);
            CreateRectangle(15, 87.932487f, 259.03049f, 37.542496f, 164.12201f);
            CreateRectangle(9, 125.47498f, 12.847499f, 37.542503f, 164.12199f);
            CreateRectangle(16, 125.47498f, 259.03049f, 37.542503f, 164.12201f);
            CreateRectangle(8, 163.01749f, 12.847499f, 37.542496f, 164.12199f);
            CreateRectangle(17, 163.01749f, 259.03049f, 37.542496f, 164.12201f);
            CreateRectangle(7, 200.55998f, 12.847499f, 37.542496f, 164.12199f);
            CreateRectangle(18, 200.55998f, 259.03049f, 37.542496f, 164.12201f);
            CreateRectangle(6, 269.79749f, 12.847499f, 37.54248f, 164.12199f);
            CreateRectangle(19, 269.79749f, 259.03049f, 37.54248f, 164.12201f);
            CreateRectangle(5, 307.33997f, 12.847499f, 37.542511f, 164.12199f);
            CreateRectangle(20, 307.33997f, 259.03049f, 37.542511f, 164.12201f);
            CreateRectangle(4, 344.88248f, 12.847499f, 37.542511f, 164.12199f);
            CreateRectangle(21, 344.88248f, 259.03049f, 37.542511f, 164.12201f);
            CreateRectangle(3, 382.42499f, 12.847499f, 37.54248f, 164.12199f);
            CreateRectangle(22, 382.42499f, 259.03049f, 37.54248f, 164.12201f);
            CreateRectangle(2, 419.96747f, 12.847499f, 37.54248f, 164.12199f);
            CreateRectangle(23, 419.96747f, 259.03049f, 37.54248f, 164.12201f);
            CreateRectangle(1, 457.50995f, 12.847499f, 37.542511f, 164.12199f);
            CreateRectangle(24, 457.50995f, 259.03049f, 37.542511f, 164.12201f);
            CreateRectangle(26, 526.74744f, 12.847499f, 31.404968f, 192.30501f);
            CreateRectangle(25, 526.74744f, 230.8475f, 31.404968f, 192.30502f);
            CreateRectangle(27, 115.62749f, 203.46666f, 128.47499f, 29.066666f);
            CreateRectangle(0, 269.79749f, 203.46666f, 128.47498f, 29.066666f);
        }
        public RectangleF GetRectangleSpace(int space)
        {
            if (SpaceList.ContainsKey(space))
            {
                return SpaceList[space];
            }
            else
            {
                return default;
            }
        }
    }
}