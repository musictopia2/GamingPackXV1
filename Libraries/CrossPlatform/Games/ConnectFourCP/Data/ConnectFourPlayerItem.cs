using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using Newtonsoft.Json;
namespace ConnectFourCP.Data
{
    public class ConnectFourPlayerItem : PlayerBoardGame<EnumColorChoice>
    {
        [JsonIgnore]
        public override bool DidChooseColor => Color != EnumColorChoice.None;

        public override void Clear()
        {
            Color = EnumColorChoice.None;
        }
    }
}