using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using ThreeLetterFunCP.Data;
namespace ThreeLetterFunCP.Logic
{
    internal static class Extensions
    {
        public static void RemoveTiles(this IDeckDict<ThreeLetterFunCardData> thisList)
        {
            foreach (var thisCard in thisList)
                thisCard.ClearTiles();
        }
        public static void RemoveTiles(this CustomBasicList<TileInformation> thisList, ThreeLetterFunVMData model)
        {
            thisList.RemoveRange(0, 2);
            model.TileBoard1!.UpdateBoard(); //i think
        }
        public static void RemoveTiles(this CustomBasicList<TileInformation> thisList)
        {
            thisList.RemoveRange(0, 2);
        }
        public static void TakeTurns(this PlayerCollection<ThreeLetterFunPlayerItem> playerList)
        {
            playerList.ForEach(player =>
            {
                player.ClearTurn();
            });
        }
    }
}