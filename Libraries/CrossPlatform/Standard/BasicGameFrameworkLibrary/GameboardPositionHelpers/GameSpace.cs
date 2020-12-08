using CommonBasicStandardLibraries.CollectionClasses;
using System.Drawing;
namespace BasicGameFrameworkLibrary.GameboardPositionHelpers
{
    public class GameSpace
    {
        public RectangleF Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
                NewArea = new byte[(int)value.Width + 1, (int)value.Height + 1];
            }
        }
        private RectangleF _area;
        internal byte[,]? NewArea { get; set; }
        public bool Enabled { get; set; } = true; // if false, then the space cannot even be clicked
        public int Index { get; set; }
        public int Row { get; set; }
        public int Column { get; set; } // don't worry about number (some games need that as well some don't)
        public CustomBasicList<RectangleF> ObjectList { get; set; } = new CustomBasicList<RectangleF>();
        public CustomBasicList<RectangleF> PieceList { get; set; } = new CustomBasicList<RectangleF>(); //hopefully that works.
        public CustomBasicList<string> ColorList { get; set; } = new CustomBasicList<string>(); //hopefully this will help

    }
}