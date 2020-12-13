using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.GameboardPositionHelpers;
using ClueBoardGameCP.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Collections;
using System.Drawing;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace ClueBoardGameCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        public ClueBoardGameGameContainer? GameContainer;
        public GameBoardGraphicsCP(ClueBoardGameGameContainer gameContainer)
        {
            GameContainer = gameContainer;
            InitializeRooms();
            InitializeSquares();
        }
        public struct Room
        {
            public string Name;
            public string FloorColor1;// its okay because no autoresume needed for this.
            public string FloorColor2;
            public CustomBasicList<RectangleF> MiscList; // coordinates.
            public RectangleF PieceArea; // this is the rectangle (coordinates only)
            public CustomBasicList<Door> Doors;
            public PointF TextPoint;
        }
        public struct Door
        {
            public PointF Point;
            public string Direction;
        }

        public CustomBasicList<Room> ArrRooms = new CustomBasicList<Room>();
        public Hashtable? ArrSquares;
        public SizeF OriginalSize => new SizeF(576, 600);
        public float SpaceSize => 24;
        public string RoomName(int room) => ArrRooms[room - 1].Name;
        private void InitializeRooms()
        {
            Room obj_TempRoom;
            Door obj_TempDoor;
            RectangleF tempRect;
            ArrSquares = new Hashtable();
            // *** Create the rooms
            ArrRooms = new CustomBasicList<Room>();
            // ******************************
            // *** Create the kitchen
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Kitchen";
            obj_TempRoom.FloorColor1 = cc.Black;
            obj_TempRoom.FloorColor2 = cc.White;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(19, 19, 6, 7);
            tempRect = new RectangleF(24, 18, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(18, 24, 1, 2);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(16, 25, 2, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.TextPoint = new PointF(20.5f, 22);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Down";
            obj_TempDoor.Point = new PointF(20, 19);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the Ball Room
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Ball Room";
            obj_TempRoom.FloorColor1 = cc.SaddleBrown;
            obj_TempRoom.FloorColor2 = cc.SaddleBrown;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(9, 18, 8, 6);
            tempRect = new RectangleF(10, 24, 5, 2);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Left";
            obj_TempDoor.Point = new PointF(16, 20);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Down";
            obj_TempDoor.Point = new PointF(14, 18);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Down";
            obj_TempDoor.Point = new PointF(10, 18);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Right";
            obj_TempDoor.Point = new PointF(9, 20);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the conservatory
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Conservatory";
            obj_TempRoom.FloorColor1 = cc.Green;
            obj_TempRoom.FloorColor2 = cc.White;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(1, 20, 5, 6);
            tempRect = new RectangleF(6, 21, 1, 5);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(7, 25, 2, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.TextPoint = new PointF(1, 23);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Left";
            obj_TempDoor.Point = new PointF(5, 20);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the billiard room
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Billiard Room";
            obj_TempRoom.FloorColor1 = cc.DarkGreen;
            obj_TempRoom.FloorColor2 = cc.DarkGreen;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(1, 13, 6, 5);
            tempRect = new RectangleF(1, 12, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(1, 18, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Left";
            obj_TempDoor.Point = new PointF(6, 16);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Down";
            obj_TempDoor.Point = new PointF(2, 13);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the library
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Library";
            obj_TempRoom.FloorColor1 = cc.Blue;
            obj_TempRoom.FloorColor2 = cc.Blue;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(1, 7, 6, 5);
            tempRect = new RectangleF(7, 8, 1, 3);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Up";
            obj_TempDoor.Point = new PointF(4, 11);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Left";
            obj_TempDoor.Point = new PointF(7, 9);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the study
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Study";
            obj_TempRoom.FloorColor1 = cc.Green;
            obj_TempRoom.FloorColor2 = cc.Green;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(1, 1, 7, 4);
            tempRect = new RectangleF(8, 1, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(1, 5, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempDoor.Direction = "Up";
            obj_TempDoor.Point = new PointF(7, 4);
            obj_TempRoom.Doors = new CustomBasicList<Door>
            {
                obj_TempDoor
            };
            obj_TempRoom.TextPoint = new PointF(3, 3);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the hall
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Hall";
            obj_TempRoom.FloorColor1 = cc.Maroon;
            obj_TempRoom.FloorColor2 = cc.Maroon;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(10, 1, 6, 7);
            tempRect = new RectangleF(9, 1, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(16, 1, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Right";
            obj_TempDoor.Point = new PointF(10, 5);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Up";
            obj_TempDoor.Point = new PointF(12, 7);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Up";
            obj_TempDoor.Point = new PointF(13, 7);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the lounge
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Lounge";
            obj_TempRoom.FloorColor1 = cc.SeaGreen;
            obj_TempRoom.FloorColor2 = cc.SeaGreen;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(18, 1, 7, 6);
            tempRect = new RectangleF(24, 7, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Up";
            obj_TempDoor.Point = new PointF(18, 6);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
            // ******************************
            // *** Create the dining room
            obj_TempRoom = new Room();
            obj_TempRoom.Name = "Dining Room";
            obj_TempRoom.FloorColor1 = cc.Brown;
            obj_TempRoom.FloorColor2 = cc.Brown;
            obj_TempRoom.MiscList = new CustomBasicList<RectangleF>();
            obj_TempRoom.PieceArea = new RectangleF(17, 10, 8, 6);
            tempRect = new RectangleF(24, 9, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(24, 17, 1, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            tempRect = new RectangleF(20, 16, 5, 1);
            obj_TempRoom.MiscList.Add(tempRect);
            obj_TempRoom.Doors = new CustomBasicList<Door>();
            obj_TempDoor.Direction = "Down";
            obj_TempDoor.Point = new PointF(18, 10);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            obj_TempDoor.Direction = "Right";
            obj_TempDoor.Point = new PointF(17, 13);
            obj_TempRoom.Doors.Add(obj_TempDoor);
            ArrRooms.Add(obj_TempRoom);
        }
        private void InitializeSquares()
        {
            int int_Row;
            int int_Col;
            // ******************************
            // *** Create the spaces
            ArrSquares = new Hashtable();
            ArrSquares.Add(new PointF(8, 2), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(9, 2), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(8, 3), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(9, 3), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(9, 4), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(8, 4), "S" + Convert.ToString(ArrSquares.Count + 1));
            int_Col = 9;
            while (int_Col >= 2)
            {
                ArrSquares.Add(new PointF(int_Col, 5), "S" + Convert.ToString(ArrSquares.Count + 1));
                int_Col -= 1;
            }
            for (int_Col = 2; int_Col <= 9; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 6), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            ArrSquares.Add(new PointF(9, 7), "S" + Convert.ToString((ArrSquares.Count + 1)));
            ArrSquares.Add(new PointF(8, 7), "S" + Convert.ToString((ArrSquares.Count + 1)));
            ArrSquares.Add(new PointF(7, 7), "S" + Convert.ToString((ArrSquares.Count + 1)));
            for (int_Col = 8; int_Col <= 16; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 8), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            int_Row = 7;
            while (int_Row >= 2)
            {
                ArrSquares.Add(new PointF(16, int_Row), "S" + Convert.ToString(ArrSquares.Count + 1));
                int_Row -= 1;
            }
            for (int_Row = 2; int_Row <= 7; int_Row++)
            {
                ArrSquares.Add(new PointF(17, int_Row), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            for (int_Col = 18; int_Col <= 23; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 7), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            int_Col = 23;
            while (int_Col >= 17)
            {
                ArrSquares.Add(new PointF(int_Col, 8), "S" + Convert.ToString(ArrSquares.Count + 1));
                int_Col -= 1;
            }
            int_Col = 23;
            while (int_Col >= 15)
            {
                ArrSquares.Add(new PointF(int_Col, 9), "S" + Convert.ToString(ArrSquares.Count + 1));
                int_Col -= 1;
            }
            // 69-75
            ArrSquares.Add(new PointF(8, 9), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(9, 9), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(9, 10), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(8, 10), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(7, 11), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(8, 11), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(9, 11), "S" + Convert.ToString(ArrSquares.Count + 1));
            // 76-83
            for (int_Col = 2; int_Col <= 9; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 12), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            // 84-89
            for (int_Row = 10; int_Row <= 12; int_Row++)
            {
                for (int_Col = 15; int_Col <= 16; int_Col++)
                {
                    ArrSquares.Add(new PointF(int_Col, int_Row), "S" + Convert.ToString(ArrSquares.Count + 1));
                }
            }
            // 90-98
            for (int_Row = 13; int_Row <= 15; int_Row++)
            {
                for (int_Col = 7; int_Col <= 9; int_Col++)
                {
                    ArrSquares.Add(new PointF(int_Col, int_Row), "S" + Convert.ToString(ArrSquares.Count + 1));
                }
            }
            // 99-104
            for (int_Row = 13; int_Row <= 15; int_Row++)
            {
                for (int_Col = 15; int_Col <= 16; int_Col++)
                    ArrSquares.Add(new PointF(int_Col, int_Row), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            // 105-117
            for (int_Col = 7; int_Col <= 19; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 16), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            // 118-134
            for (int_Col = 7; int_Col <= 23; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 17), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            // 135-141
            for (int_Col = 2; int_Col <= 8; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 18), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            // 142-148
            for (int_Col = 17; int_Col <= 23; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 18), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            // 149-155
            for (int_Col = 2; int_Col <= 8; int_Col++)
            {
                ArrSquares.Add(new PointF(int_Col, 19), "S" + Convert.ToString(ArrSquares.Count + 1));
            }
            // 156-162
            ArrSquares.Add(new PointF(17, 19), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(18, 19), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(6, 20), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(7, 20), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(8, 20), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(17, 20), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(18, 20), "S" + Convert.ToString(ArrSquares.Count + 1));
            // 163-171
            for (int_Row = 21; int_Row <= 24; int_Row++)
            {
                for (int_Col = 7; int_Col <= 8; int_Col++)
                {
                    ArrSquares.Add(new PointF(int_Col, int_Row), "S" + Convert.ToString(ArrSquares.Count + 1));
                }
            }
            ArrSquares.Add(new PointF(9, 24), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(15, 24), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(16, 24), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(17, 24), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(17, 23), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(18, 23), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(18, 22), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(17, 22), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(17, 21), "S" + Convert.ToString(ArrSquares.Count + 1));
            ArrSquares.Add(new PointF(18, 21), "S" + Convert.ToString(ArrSquares.Count + 1));
            // *** Create the entryways
            ArrSquares.Add(new PointF(17, 1), "b1");
            ArrSquares.Add(new PointF(24, 8), "b2");
            ArrSquares.Add(new PointF(15, 25), "b3");
            ArrSquares.Add(new PointF(9, 25), "b4");
            ArrSquares.Add(new PointF(1, 19), "b5");
            ArrSquares.Add(new PointF(1, 6), "b6");
        }
        public RectangleF GetBounds => new RectangleF(new PointF(0, 0), OriginalSize);
        public PointF PositionForStartSpace(int startNumber) // used to be private but has to be public so blazor can use it to position pieces.
        {
            var bounds = GetBounds;
            int int_Row;
            int int_Col;
            PointF pt_Temp;
            string str_Name;
            float dbl_SquareWidth;
            float dbl_SquareHeight;
            dbl_SquareWidth = bounds.Width / 24;
            dbl_SquareHeight = bounds.Height / 25;
            for (int_Row = 1; int_Row <= 25; int_Row++)
            {
                for (int_Col = 1; int_Col <= 24; int_Col++)
                {
                    pt_Temp = new PointF(int_Col, int_Row);
                    if ((ArrSquares!.Contains(pt_Temp)))
                    {
                        str_Name = ArrSquares[pt_Temp].ToString();
                        if ((str_Name ?? "") == (("b" + startNumber) ?? ""))
                        {
                            return new PointF((pt_Temp.X - 1) * (dbl_SquareWidth), ((pt_Temp.Y - 1) * (dbl_SquareHeight)));
                        }
                    }
                }
            }
            return default;
        }
        public PointF PositionForBoardPiece(int space)
        {
            int int_Row;
            int int_Col;
            PointF pt_Temp;
            string str_Name;
            float dbl_SquareWidth;
            float dbl_SquareHeight;
            dbl_SquareHeight = SpaceSize;
            dbl_SquareWidth = SpaceSize;
            for (int_Row = 1; int_Row <= 25; int_Row++)
            {
                for (int_Col = 1; int_Col <= 24; int_Col++)
                {
                    pt_Temp = new PointF(int_Col, int_Row);
                    if ((ArrSquares!.Contains(pt_Temp)))
                    {
                        str_Name = ArrSquares[pt_Temp].ToString();
                        if ((str_Name ?? "") == (("S" + space) ?? ""))
                        {
                            return new PointF((pt_Temp.X - 1) * (dbl_SquareWidth), ((pt_Temp.Y - 1) * (dbl_SquareHeight)));
                        }
                    }
                }
            }
            return default;
        }
        public GameSpace RoomSpaceInfo(int room)
        {
            GameSpace thisRoom = new GameSpace();
            Room obj_Room;
            var bounds = GetBounds;
            float dbl_SquareWidth;
            float dbl_SquareHeight;
            dbl_SquareWidth = bounds.Width / 24;
            dbl_SquareHeight = bounds.Height / 25;
            int y;
            obj_Room = ArrRooms![room - 1]; // because 0 based.
            thisRoom.Area = new RectangleF((obj_Room.PieceArea.Left - 1) * (dbl_SquareWidth), (obj_Room.PieceArea.Top - 1) * (dbl_SquareHeight), dbl_SquareWidth * obj_Room.PieceArea.Width, dbl_SquareHeight * obj_Room.PieceArea.Height);
            var loopTo = obj_Room.Doors.Count - 1;
            for (y = 0; y <= loopTo; y++)
            {
                thisRoom.ObjectList.Add(DoorRectangle(obj_Room.Doors[y], dbl_SquareWidth, dbl_SquareHeight));
            }
            return thisRoom;
        }
        private RectangleF DoorRectangle(Door obj_Door, float dbl_SquareWidth, float dbl_SquareHeight)
        {
            PointF pt_Temp;
            pt_Temp = obj_Door.Point;
            switch (obj_Door.Direction)
            {
                case "Up":
                    {
                        return new RectangleF((pt_Temp.X - 1) * (dbl_SquareWidth), ((pt_Temp.Y - 1) * (dbl_SquareHeight)) + (dbl_SquareHeight * 0.4f), dbl_SquareWidth, dbl_SquareHeight * 0.6f);
                    }

                case "Down":
                    {
                        return new RectangleF((pt_Temp.X - 1) * (dbl_SquareWidth), ((pt_Temp.Y - 1) * (dbl_SquareHeight)), dbl_SquareWidth, dbl_SquareHeight * 0.6f);
                    }

                case "Left":
                    {
                        return new RectangleF((pt_Temp.X - 1) * (dbl_SquareWidth) + (dbl_SquareWidth * 0.4f), ((pt_Temp.Y - 1) * (dbl_SquareHeight)), dbl_SquareWidth * 0.6f, dbl_SquareHeight);
                    }

                case "Right":
                    {
                        return new RectangleF((pt_Temp.X - 1) * (dbl_SquareWidth), ((pt_Temp.Y - 1) * (dbl_SquareHeight)), dbl_SquareWidth * 0.6f, dbl_SquareHeight);
                    }
            }

            return default;
        }

        public CustomBasicList<(RectangleF rect, int id)> ClickRoomList()
        {
            CustomBasicList<(RectangleF, int)> output = new CustomBasicList<(RectangleF, int)>();
            var bounds = new RectangleF(new PointF(0, 0), OriginalSize);
            RectangleF rect_Room;
            float dbl_SquareWidth;
            float dbl_SquareHeight;
            int z = 0;
            dbl_SquareWidth = bounds.Width / 24;
            dbl_SquareHeight = bounds.Height / 25;
            foreach (var obj_Room in ArrRooms!)
            {
                z += 1;
                rect_Room = new RectangleF((obj_Room.PieceArea.Left - 1) * (dbl_SquareWidth), ((obj_Room.PieceArea.Top - 1) * (dbl_SquareHeight)), dbl_SquareWidth * obj_Room.PieceArea.Width, dbl_SquareHeight * obj_Room.PieceArea.Height);
                output.Add((rect_Room, z));
            }
            return output;
        }
        public CustomBasicList<(RectangleF rect, int id)> ClickSquareList()
        {
            var bounds = new RectangleF(new PointF(0, 0), OriginalSize);
            int int_Row;
            int int_Col;
            RectangleF rect_Temp;
            PointF pt_Temp;
            string str_Name;
            float dbl_SquareWidth;
            float dbl_SquareHeight;
            dbl_SquareWidth = bounds.Width / 24f;
            dbl_SquareHeight = bounds.Height / 25f;
            CustomBasicList<(RectangleF, int)> output = new CustomBasicList<(RectangleF, int)>();
            for (int_Row = 1; int_Row <= 25; int_Row++)
            {
                for (int_Col = 1; int_Col <= 24; int_Col++)
                {
                    pt_Temp = new PointF(int_Col, int_Row);
                    if ((ArrSquares!.Contains(pt_Temp)))
                    {
                        str_Name = ArrSquares[pt_Temp].ToString();
                        if (str_Name.Contains("b") == false)
                        {
                            rect_Temp = new RectangleF((pt_Temp.X - 1) * (dbl_SquareWidth), ((pt_Temp.Y - 1) * (dbl_SquareHeight)), dbl_SquareWidth, dbl_SquareHeight);
                            int id = Convert.ToInt32(str_Name.Replace("S", "").Replace("b", ""));
                            output.Add((rect_Temp, id));
                        }
                    }
                }
            }
            return output;
        }
    }
}