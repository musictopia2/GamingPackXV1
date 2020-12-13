using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using LifeBoardGameCP.Data;
using Microsoft.AspNetCore.Components;
namespace LifeBoardGameBlazor
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
        private readonly int _targetImageSize = 17;
        private string GetColor(EnumColorChoice color) => color.ToColor();
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }
}