using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using Microsoft.AspNetCore.Components;
namespace MilkRunBlazor
{
    public partial class GamePage
    {
        private int TargetHeight => 20;
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