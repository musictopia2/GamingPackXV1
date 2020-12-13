using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommonInterfaces;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Drawing;
namespace TileRummyCP.Data
{
    public class TileInfo : SimpleDeckObject, IDeckObject, IRummmyObject<EnumColorType, EnumColorType>, IComparable<TileInfo>, ILocationDeck
    {
        private PointF _location;
        public PointF Location
        {
            get { return _location; }
            set
            {
                if (SetProperty(ref _location, value))
                {
                    
                }
            }
        }
        public TileInfo()
        {
            DefaultSize = new SizeF(60, 40);
        }
        private bool _isJoker;
        public bool IsJoker
        {
            get { return _isJoker; }
            set
            {
                if (SetProperty(ref _isJoker, value))
                {
                    
                }
            }
        }
        public int Points { get; set; }
        private EnumDrawType _whatDraw = EnumDrawType.IsNone;
        public EnumDrawType WhatDraw
        {
            get { return _whatDraw; }
            set
            {
                if (SetProperty(ref _whatDraw, value))
                {
                    Drew = value != EnumDrawType.IsNone;
                }
            }
        }
        private EnumColorType _color;
        public EnumColorType Color
        {
            get { return _color; }
            set
            {
                if (SetProperty(ref _color, value))
                {
                    
                }

            }
        }
        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                if (SetProperty(ref _number, value))
                {
                    
                }
            }
        }
        int IRummmyObject<EnumColorType, EnumColorType>.GetSecondNumber => Number;
        int ISimpleValueObject<int>.ReadMainValue => Number;
        bool IWildObject.IsObjectWild => IsJoker;
        bool IIgnoreObject.IsObjectIgnored => false;
        EnumColorType ISuitObject<EnumColorType>.GetSuit => Color;
        EnumColorType IColorObject<EnumColorType>.GetColor => Color;
        public void Populate(int chosen)
        {
            int x;
            int y;
            int z;
            int q = 0;
            for (x = 1; x <= 4; x++)
            {
                for (y = 1; y <= 13; y++)
                {
                    for (z = 1; z <= 2; z++)
                    {
                        q += 1;
                        if (q == chosen)
                        {
                            Deck = chosen;
                            if (x == 1)
                            {
                                Color = EnumColorType.Black;
                            }
                            else if (x == 2)
                            {
                                Color = EnumColorType.Blue;
                            }
                            else if (x == 3)
                            {
                                Color = EnumColorType.Orange;
                            }
                            else if (x == 4)
                            {
                                Color = EnumColorType.Red;
                            }
                            Number = y;
                            Points = y;
                            IsJoker = false;
                            return;
                        }
                    }
                }
            }
            for (x = 105; x <= 106; x++)
            {
                if (chosen == x)
                {
                    Deck = chosen;
                    IsJoker = true;
                    Number = 20;
                    Points = 25;
                    if (x == 105)
                    {
                        Color = EnumColorType.Black;
                    }
                    else
                    {
                        Color = EnumColorType.Red;
                    }
                    return;
                }
            }
            throw new BasicBlankException("Cannot find the deck " + Deck);
        }
        public void Reset()
        {
            WhatDraw = EnumDrawType.IsNone;
        }
        int IComparable<TileInfo>.CompareTo(TileInfo other)
        {
            if (Number != other.Number)
            {
                return Number.CompareTo(other.Number);
            }
            return Color.CompareTo(other.Color);
        }
    }
}