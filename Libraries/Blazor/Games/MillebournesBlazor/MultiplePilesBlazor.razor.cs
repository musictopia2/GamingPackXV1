using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGamingUIBlazorLibrary.Extensions;
using Microsoft.AspNetCore.Components;
using MillebournesCP.Cards;
namespace MillebournesBlazor
{
    public partial class MultiplePilesBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; }
        [Parameter]
        public BasicMultiplePilesCP<MillebournesCardInformation>? Piles { get; set; }
        [Parameter]
        public string AnimationTag { get; set; } = "";
        [Parameter]
        public bool Inline { get; set; } = true;
        private string RealHeight => TargetHeight.HeightString();
    }
}