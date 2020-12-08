using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using Microsoft.AspNetCore.Components;
using System;
namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.MainRummySets
{
    public partial class BaseMainRummySetsBlazor<SU, CO, RU, SE, T>
        where SU : Enum
            where CO : Enum
            where RU : IRummmyObject<SU, CO>, IDeckObject, new()
            where SE : SetInfo<SU, CO, RU, T>
    {

        [Parameter]
        public RenderFragment<SE>? ChildContent { get; set; }
        [Parameter]
        public MainSetsObservable<SU, CO, RU, SE, T>? DataContext { get; set; } //hopefully this simple.
        [Parameter]
        public string ContainerWidth { get; set; } = "";
        [Parameter]
        public string ContainerHeight { get; set; } = "";
        [Parameter]
        public double Divider { get; set; } = 1;
        [Parameter]
        public double AdditionalSpacing { get; set; } = -5;
        [CascadingParameter]
        public int TargetImageHeight { get; set; }
        private bool IsDisabled => !DataContext!.IsEnabled;
        private string GetContainerStyle()
        {
            return $"width: {ContainerWidth}; height: {ContainerHeight}; overflow: auto; white-space: nowrap; "; //hopefully this simple.
        }
    }
}