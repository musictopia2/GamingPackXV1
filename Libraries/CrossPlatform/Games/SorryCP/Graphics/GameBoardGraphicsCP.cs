using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using SorryCP.Data;
using System.Collections.Generic;
using System.Drawing;
namespace SorryCP.Graphics
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        public SizeF OriginalSize { get; private set; } = new SizeF(640, 640);
        public readonly Dictionary<int, RectangleF> SpaceList = new Dictionary<int, RectangleF>(); //needs to be public now so blazor can read it.

        public Dictionary<int, RectangleF> HomeSpaces = new Dictionary<int, RectangleF>();
        //these 2 needs to be public so blazor can use the information.
        public RectangleF DiscardLocation { get; private set; }
        public RectangleF DeckLocation { get; private set; }
        public SorryGameContainer Container { get; set; }
        public float PieceHeight { get; set; }
        public float PieceWidth { get; set; }
        public GameBoardGraphicsCP(SorryGameContainer container)
        {
            Container = container;
            CreateSpaces();
        }
        public PointF LocationOfSpace(int index)
        {
            return SpaceList[index].Location;
        }
        public SizeF CardSize()
        {
            var bounds = GetBounds();
            return new SizeF(50 * (bounds.Size.Width / 390), 105 * (bounds.Size.Height / 390));
        }
        private RectangleF GetBounds() => new RectangleF(0, 0, OriginalSize.Width, OriginalSize.Height);
        private void CreateSpaces()
        {
            var bounds = GetBounds();
            int int_Count;
            RectangleF rect;
            PointF pt_Center = new PointF(bounds.Left + (bounds.Width / 2), bounds.Top + (bounds.Height / 2));
            RectangleF obj_Space;
            HomeSpaces = new Dictionary<int, RectangleF>();
            DeckLocation = new RectangleF(pt_Center.X - CardSize().Width - (CardSize().Width / 10), pt_Center.Y - (CardSize().Height / 2), CardSize().Width, CardSize().Height);
            DiscardLocation = new RectangleF(pt_Center.X + (CardSize().Width / 10), pt_Center.Y - (CardSize().Height / 2), CardSize().Width, CardSize().Height);
            int spaceNumber;
            for (int_Count = 10; int_Count <= 14; int_Count++)
            {
                // *** Blue
                rect = new RectangleF(bounds.Left + ((bounds.Width * 13) / 16), bounds.Top + ((bounds.Height * int_Count) / 16), bounds.Width / 16, ((((bounds.Height * (int_Count + 1)) / 16)) - (((bounds.Height * int_Count) / 16))) + 1);
                obj_Space = rect;
                spaceNumber = (77 + 14) - int_Count;
                SpaceList.Add(spaceNumber, obj_Space);
                // *** yellow
                rect = new RectangleF(bounds.Left + ((bounds.Width * (int_Count - 9)) / 16), bounds.Top + ((bounds.Height * 13) / 16), ((((bounds.Width * (int_Count + 1)) / 16)) - (((bounds.Width * int_Count) / 16))) + 1, bounds.Height / 16);
                obj_Space = rect;
                spaceNumber = 72 + int_Count;
                SpaceList.Add(spaceNumber, obj_Space);
                // *** green
                rect = new RectangleF(bounds.Left + ((bounds.Width * 2) / 16), bounds.Top + ((bounds.Height * (int_Count - 9)) / 16), bounds.Width / 16, ((((bounds.Height * (int_Count + 1)) / 16)) - (((bounds.Height * int_Count) / 16))) + 1);
                obj_Space = rect;
                spaceNumber = 77 + int_Count;
                SpaceList.Add(spaceNumber, obj_Space);
                // *** red
                rect = new RectangleF(bounds.Left + ((bounds.Width * int_Count) / 16), bounds.Top + ((bounds.Height * 2) / 16), ((((bounds.Width * (int_Count + 1)) / 16)) - (((bounds.Width * int_Count) / 16))) + 1, bounds.Height / 16);
                obj_Space = rect;
                spaceNumber = (92 + 14) - int_Count;
                SpaceList.Add(spaceNumber, obj_Space);
            }
            for (int_Count = 0; int_Count <= 15; int_Count++)
            {
                if ((int_Count > 0) & (int_Count < 15))
                {
                    // *** Top 
                    rect = new RectangleF(bounds.Left + ((bounds.Width * int_Count) / 16), bounds.Top, ((((bounds.Width * (int_Count + 1)) / 16)) - (((bounds.Width * int_Count) / 16))) + 1, bounds.Height / 16);
                    obj_Space = rect;
                    spaceNumber = 43 + int_Count;
                    SpaceList.Add(spaceNumber, obj_Space);
                    // *** Bottom
                    rect = new RectangleF(bounds.Left + ((bounds.Width * int_Count) / 16), bounds.Top + ((bounds.Height * 15) / 16), ((((bounds.Width * (int_Count + 1)) / 16)) - (((bounds.Width * int_Count) / 16))) + 1, bounds.Height / 16);
                    obj_Space = rect;
                    if ((28 - int_Count) < 17)
                    {
                        spaceNumber = 88 - int_Count;
                    }
                    else
                    {
                        spaceNumber = 28 - int_Count;
                    }
                    SpaceList.Add(spaceNumber, obj_Space);
                    // *** Left
                    rect = new RectangleF(bounds.Left, bounds.Top + ((bounds.Height * int_Count) / 16), bounds.Width / 16, ((((bounds.Height * (int_Count + 1)) / 16)) - (((bounds.Height * int_Count) / 16))) + 1);
                    obj_Space = rect;
                    spaceNumber = 43 - int_Count;
                    SpaceList.Add(spaceNumber, obj_Space);
                    // *** Right
                    rect = new RectangleF(bounds.Left + ((bounds.Width * 15) / 16), bounds.Top + ((bounds.Height * int_Count) / 16), bounds.Width / 16, ((((bounds.Height * (int_Count + 1)) / 16)) - (((bounds.Height * int_Count) / 16))) + 1);
                    obj_Space = rect;
                    spaceNumber = 58 + int_Count;
                    SpaceList.Add(spaceNumber, obj_Space);
                }
                else
                {
                    // *** Top 
                    rect = new RectangleF(bounds.Left + ((bounds.Width * int_Count) / 16), bounds.Top, (((bounds.Width * (int_Count + 1)) / 16)) - (((bounds.Width * int_Count) / 16)), bounds.Height / 16);
                    obj_Space = rect;
                    spaceNumber = 43 + int_Count;
                    SpaceList.Add(spaceNumber, obj_Space);
                    // *** Bottom
                    rect = new RectangleF(bounds.Left + ((bounds.Width * int_Count) / 16), bounds.Top + ((bounds.Height * 15) / 16), (((bounds.Width * (int_Count + 1)) / 16)) - (((bounds.Width * int_Count) / 16)), bounds.Height / 16);
                    obj_Space = rect;
                    if ((28 - int_Count) < 17)
                    {
                        spaceNumber = 88 - int_Count;
                    }
                    else
                    {
                        spaceNumber = 28 - int_Count;
                    }
                    SpaceList.Add(spaceNumber, obj_Space);
                }
            }
            //blue start.
            obj_Space = new RectangleF(410, 505, 40, 40);
            SpaceList.Add(1, obj_Space);
            obj_Space = new RectangleF(460, 505, 40, 40);
            SpaceList.Add(2, obj_Space);
            obj_Space = new RectangleF(410, 555, 40, 40);
            SpaceList.Add(3, obj_Space);
            obj_Space = new RectangleF(460, 555, 40, 40);
            SpaceList.Add(4, obj_Space);
            //yellow start
            obj_Space = new RectangleF(40, 410, 40, 40);
            SpaceList.Add(5, obj_Space);
            obj_Space = new RectangleF(90, 410, 40, 40);
            SpaceList.Add(6, obj_Space);
            obj_Space = new RectangleF(40, 460, 40, 40);
            SpaceList.Add(7, obj_Space);
            obj_Space = new RectangleF(90, 460, 40, 40);
            SpaceList.Add(8, obj_Space);
            //green start
            obj_Space = new RectangleF(130, 40, 40, 40);
            SpaceList.Add(9, obj_Space);
            obj_Space = new RectangleF(180, 40, 40, 40);
            SpaceList.Add(10, obj_Space);
            obj_Space = new RectangleF(130, 90, 40, 40);
            SpaceList.Add(11, obj_Space);
            obj_Space = new RectangleF(180, 90, 40, 40);
            SpaceList.Add(12, obj_Space);
            //red start
            obj_Space = new RectangleF(505, 130, 40, 40);
            SpaceList.Add(13, obj_Space);
            obj_Space = new RectangleF(555, 130, 40, 40);
            SpaceList.Add(14, obj_Space);
            obj_Space = new RectangleF(505, 180, 40, 40);
            SpaceList.Add(15, obj_Space);
            obj_Space = new RectangleF(555, 180, 40, 40);
            SpaceList.Add(16, obj_Space);
            obj_Space = new RectangleF(490, 300, 100, 100);
            HomeSpaces.Add(1, obj_Space); //blue home.
            obj_Space = new RectangleF(240, 490, 100, 100);
            HomeSpaces.Add(2, obj_Space); //yellow home
            obj_Space = new RectangleF(50, 240, 100, 100);
            HomeSpaces.Add(3, obj_Space); //green home
            obj_Space = new RectangleF(300, 50, 100, 100);
            HomeSpaces.Add(4, obj_Space); //red home.
            if (SpaceList.Count == 0)
            {
                throw new BasicBlankException("No spaces was created");
            }
            PieceHeight = bounds.Height / 17f;
            PieceWidth = bounds.Width / 17f;
        }
        public CustomBasicList<RectangleF> GetFourHomeRectangles(RectangleF home) //needs public so blazor can communicate with it.
        {
            var firstRect = new RectangleF(home.Location.X, home.Location.Y, home.Width / 4, home.Height / 4);
            var secondRect = new RectangleF(home.Location.X + (home.Width / 2), home.Location.Y, home.Width / 4, home.Height / 4);
            var thirdRect = new RectangleF(home.Location.X, home.Location.Y + (home.Height / 2), home.Width / 4, home.Height / 4);
            var fourthRect = new RectangleF(home.Location.X + (home.Width / 2), home.Location.Y + (home.Height / 2), home.Width / 4, home.Height / 4);
            return new CustomBasicList<RectangleF>() { firstRect, secondRect, thirdRect, fourthRect };
        }
    }
}