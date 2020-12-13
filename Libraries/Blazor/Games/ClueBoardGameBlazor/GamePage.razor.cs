using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using ClueBoardGameCP.Data;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
namespace ClueBoardGameBlazor
{
    public partial class GamePage
    {
        public int TargetHeight { get; set; } = 17; //can adjust as needed;
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        [CascadingParameter]
        public IGameInfo? GameData { get; set; }
        [CascadingParameter]
        public BasicData? BasicData { get; set; }
        [CascadingParameter]
        public MultiplayerBasicParentShell? Shell { get; set; }
        private string GetColor(EnumColorChoice color) => color.ToColor();
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }
}