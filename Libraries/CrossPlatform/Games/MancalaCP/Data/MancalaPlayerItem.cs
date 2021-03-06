using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace MancalaCP.Data
{
    public class MancalaPlayerItem : SimplePlayer
    {
        public int HowManyPiecesAtHome { get; set; }
        public CustomBasicList<PlayerPieceData> ObjectList { get; set; } = new CustomBasicList<PlayerPieceData>();
    }
}