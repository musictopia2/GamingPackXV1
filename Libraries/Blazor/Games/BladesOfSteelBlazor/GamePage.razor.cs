using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using BladesOfSteelCP.Data;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BladesOfSteelBlazor
{
    public partial class GamePage
    {
        private int TargetHeight => 15;
        [CascadingParameter]
        public TestOptions? TestData { get; set; }

        [CascadingParameter]
        public IGameInfo? GameData { get; set; }

        [CascadingParameter]
        public BasicData? BasicData { get; set; }

        [CascadingParameter]
        public MultiplayerBasicParentShell? Shell { get; set; }
        private BladesOfSteelVMData? GetData => cons!.Resolve<BladesOfSteelVMData>();
    }
}