using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using System.Drawing;
namespace ThreeLetterFunCP.Data
{
    public class TileInformation : SimpleDeckObject, IDeckObject
    {
        private char _letter;
        public char Letter
        {
            get
            {
                return _letter;
            }

            set
            {
                if (SetProperty(ref _letter, value) == true)
                {
                }
            }
        }
        private bool _isMoved;
        public bool IsMoved
        {
            get
            {
                return _isMoved;
            }

            set
            {
                if (SetProperty(ref _isMoved, value) == true)
                {
                }
            }
        }
        public int Index { get; set; }
        public TileInformation()
        {
            DefaultSize = new SizeF(19, 30);
        }
        public void Reset() { }

        public void Populate(int chosen)
        {
            //decided to do nothing.
            //only needed to implement it because otherwise, can't use the hand view model for the tiles.
        }
    }
}