using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using System.Collections.Generic;
namespace BingoCP.Data
{
    [SingletonGame]
    public class BingoSaveInfo : BasicSavedGameClass<BingoPlayerItem>
    {
        public GameBoardCP BingoBoard = new GameBoardCP();
        public Dictionary<int, BingoItem> CallList = new Dictionary<int, BingoItem>(); //host will create this list and send to clients.
    }
}