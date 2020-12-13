using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor
{
    public partial class GamePage
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        [CascadingParameter]
        public IGameInfo? GameData { get; set; }
        [CascadingParameter]
        public BasicData? BasicData { get; set; }
        private int TargetHeight => 14;
        private CompleteContainerClass GetContainer => new CompleteContainerClass();
    }
}