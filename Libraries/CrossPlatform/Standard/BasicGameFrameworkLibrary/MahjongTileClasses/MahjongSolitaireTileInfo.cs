using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
namespace BasicGameFrameworkLibrary.MahjongTileClasses
{
    public class MahjongSolitaireTileInfo : BasicMahjongTile, IDeckObject, IMahjongTileInfo
    {
        public void Populate(int chosen)
        {

            MahjongBasicTileHelper.PopulateTile(this, chosen);
        }
        public void Reset() { }
    }
}