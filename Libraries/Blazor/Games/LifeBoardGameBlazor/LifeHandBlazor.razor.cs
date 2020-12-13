using BasicGameFrameworkLibrary.DrawableListsObservable;
using LifeBoardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace LifeBoardGameBlazor
{
    public partial class LifeHandBlazor
    {
        [Parameter]
        public HandObservable<LifeBaseCard>? Hand { get; set; }
        /// <summary>
        /// this is where you usually set a percentage which represents how high or wide the container is.
        /// if hand type is horizontal, then its the width
        /// otherwise, its the height
        /// </summary>
        [Parameter]
        public string TargetContainerSize { get; set; } = ""; //if not set, will keep going forever.
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}