using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using Microsoft.AspNetCore.Components;
namespace DominoBonesMultiplayerGamesBlazor
{
    public partial class GamePage
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }

        [CascadingParameter]
        public IGameInfo? GameData { get; set; }

        [CascadingParameter]
        public BasicData? BasicData { get; set; }

        [CascadingParameter]
        public MultiplayerBasicParentShell? Shell { get; set; }
        private int TargetHeight { get; set; } = 7; //can adjust as needed though.
    }
}