using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
namespace BasicGameFrameworkLibrary.MahjongTileClasses
{
    public abstract class BasicMahjongTile : SimpleDeckObject, IMahjongTileInfo
    {
        public enum EnumDirectionType
        {
            IsNorth = 1,
            IsSouth = 2,
            IsWest = 3,
            IsEast = 4,
            IsNoDirection = 0
        }
        public enum EnumColorType
        {
            IsRed = 1,
            IsGreen = 2,
            IsWhite = 3,
            IsNoColor = 4
        }
        public enum EnumBonusType
        {
            IsNoBonus = 0,
            IsSeason = 1,
            IsFlower = 2
        }
        public enum EnumNumberType
        {
            IsCircle = 1,
            IsBamboo = 2,
            IsCharacter = 3,
            IsNoNumber = 0
        }
        private int _index; // this is needed to get the proper image when it comes to drawing.
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                if (SetProperty(ref _index, value) == true) { }
            }
        }
        public int NumberUsed { get; set; }
        public EnumColorType WhatColor { get; set; }
        public EnumBonusType WhatBonus { get; set; }
        public EnumNumberType WhatNumber { get; set; }
        public EnumDirectionType WhatDirection { get; set; }
        private float _left;
        public float Left
        {
            get
            {
                return _left;
            }
            set
            {
                if (SetProperty(ref _left, value) == true) { }
            }
        }
        private float _top;
        public float Top
        {
            get
            {
                return _top;
            }
            set
            {
                if (SetProperty(ref _top, value) == true) { }
            }
        }
        private bool _needsLeft;
        public bool NeedsLeft
        {
            get
            {
                return _needsLeft;
            }
            set
            {
                if (SetProperty(ref _needsLeft, value) == true) { }
            }
        }
        private bool _needsTop;
        public bool NeedsTop
        {
            get
            {
                return _needsTop;
            }
            set
            {
                if (SetProperty(ref _needsTop, value) == true) { }
            }
        }
        private bool _needsRight;
        public bool NeedsRight
        {
            get
            {
                return _needsRight;
            }
            set
            {
                if (SetProperty(ref _needsRight, value) == true) { }
            }
        }
        private bool _needsBottom;
        public bool NeedsBottom
        {
            get
            {
                return _needsBottom;
            }
            set
            {
                if (SetProperty(ref _needsBottom, value) == true) { }
            }
        }
    }
}