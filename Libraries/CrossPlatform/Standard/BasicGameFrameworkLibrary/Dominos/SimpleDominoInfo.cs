using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Drawing;
namespace BasicGameFrameworkLibrary.Dominos
{
    public class SimpleDominoInfo : SimpleDeckObject,
        IDominoInfo, IDeckCount, IComparable<SimpleDominoInfo>
    {
        public int FirstNum { get; set; }
        public int SecondNum { get; set; }

        private PointF _location; //this is needed so it can work with scattering pieces.
        public PointF Location
        {
            get { return _location; }
            set
            {
                if (SetProperty(ref _location, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        private int _currentFirst;
        public int CurrentFirst
        {
            get { return _currentFirst; }
            set
            {
                if (SetProperty(ref _currentFirst, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        private int _currentSecond;
        public int CurrentSecond
        {
            get { return _currentSecond; }
            set
            {
                if (SetProperty(ref _currentSecond, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        public SimpleDominoInfo()
        {
            DefaultSize = new SizeF(95, 31); //that is always default size.
            CurrentFirst = -1;
            CurrentSecond = -1;
        }
        public virtual int Points => FirstNum + SecondNum;
        public virtual int HighestDomino => 6;
        public void Reset()
        {
            CurrentFirst = FirstNum;
            CurrentSecond = SecondNum;
            Rotated = false; //i think.
        }
        public void Populate(int chosen)
        {
            int y;
            int z = default;
            var loopTo = HighestDomino;
            int x;
            for (x = 0; x <= loopTo; x++)
            {
                var loopTo1 = HighestDomino;
                for (y = x; y <= loopTo1; y++)
                {
                    z++;
                    if (z == chosen)
                    {
                        FirstNum = x;
                        SecondNum = y;
                        CurrentFirst = x;
                        CurrentSecond = y;
                        Deck = chosen;
                        return;
                    }
                }
            }
            throw new BasicBlankException($"Cannot find the deck of {chosen}");
        }
        public int GetDeckCount()
        {
            int y;
            int z = default;
            var loopTo = HighestDomino;
            int x;
            for (x = 0; x <= loopTo; x++)
            {
                var loopTo1 = HighestDomino;
                for (y = x; y <= loopTo1; y++)
                {
                    z += 1;
                }
            }
            if (z == 0)
            {
                throw new BasicBlankException("The deck count for dominos cannot be 0");
            }
            return z;
        }
        int IComparable<SimpleDominoInfo>.CompareTo(SimpleDominoInfo other)
        {
            if (CurrentFirst.CompareTo(other.CurrentFirst) != 0)
            {
                return CurrentFirst.CompareTo(other.CurrentFirst);
            }
            return CurrentSecond.CompareTo(other.CurrentSecond);
        }
    }
}