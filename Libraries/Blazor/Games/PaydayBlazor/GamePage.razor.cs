using System;
using System.Linq;
using System.Net.Http;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using PaydayCP.Data;

namespace PaydayBlazor
{
    public partial class GamePage
    {
        private int _targetImageHeight = 13; //can adjust here.
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