using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using Newtonsoft.Json;
namespace SequenceDiceCP.Data
{
    public class SequenceDicePlayerItem : PlayerBoardGame<EnumColorChoice>
    {
        [JsonIgnore]
        public override bool DidChooseColor => Color != EnumColorChoice.None;
        public override void Clear()
        {
            Color = EnumColorChoice.None;
        }
    }
}