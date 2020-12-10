using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MahjongTileClasses;

namespace MahJongSolitaireCP.Data
{
    public class BoardInfo
    {
        public enum EnumBoardCategory
        {
            FarLeft = 1,
            Regular = 2,
            FarRight = 3,
            VeryTop = 4
        }
        public int Floor { get; set; }
        public int RowStart { get; set; }
        public int HowManyColumns { get; set; }
        public int FrontTaken { get; set; }
        public int BackTaken { get; set; }
        public bool Enabled { get; set; } = false; // i think this is needed as well

        public DeckRegularDict<MahjongSolitaireTileInfo> TileList { get; set; } = new DeckRegularDict<MahjongSolitaireTileInfo>(); //no need for observable.
        public EnumBoardCategory BoardCategory { get; set; } = EnumBoardCategory.Regular; // most are regular
        public int ColumnStart { get; set; }
    }
}