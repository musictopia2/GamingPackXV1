using BasicGameFrameworkLibrary.DrawableListsObservable;
using LifeBoardGameCP.Cards;
using Microsoft.AspNetCore.Components;
namespace LifeBoardGameBlazor
{
    public partial class LifePileBlazor
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public SingleObservablePile<LifeBaseCard>? SinglePile { get; set; }
        private string RealHeight => $"{TargetHeight}vh";
    }
}