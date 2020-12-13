namespace AggravationCP.Data
{
    public class SpaceInfo
    {
        public int SpaceNumber { get; set; }
        public EnumColorChoice ColorOwner { get; set; }
        public int Player { get; set; }
        public EnumBoardStatus WhatBoard { get; set; }
        public int Index { get; set; }
    }
}