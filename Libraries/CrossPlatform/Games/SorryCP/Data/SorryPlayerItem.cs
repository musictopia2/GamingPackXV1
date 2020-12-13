using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Newtonsoft.Json;
namespace SorryCP.Data
{
    public class SorryPlayerItem : PlayerBoardGame<EnumColorChoice>
    {
        [JsonIgnore]
        public override bool DidChooseColor => Color != EnumColorChoice.None;
        public override void Clear()
        {
            Color = EnumColorChoice.None;
        }
        public CustomBasicList<int> PieceList { get; set; } = new CustomBasicList<int>();
        public int HowManyHomePieces { get; set; }
    }
}