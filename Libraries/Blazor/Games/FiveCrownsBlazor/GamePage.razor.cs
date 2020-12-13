using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using Microsoft.AspNetCore.Components;
namespace FiveCrownsBlazor
{
    public partial class GamePage
    {
        private int TargetHeight => 9;
        [CascadingParameter]
        public TestOptions? TestData { get; set; }

        [CascadingParameter]
        public IGameInfo? GameData { get; set; }

        [CascadingParameter]
        public BasicData? BasicData { get; set; }

        [CascadingParameter]
        public MultiplayerBasicParentShell? Shell { get; set; }
    }
}