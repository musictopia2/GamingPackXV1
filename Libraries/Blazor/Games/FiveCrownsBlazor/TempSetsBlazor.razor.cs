using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using FiveCrownsCP.Cards;
using FiveCrownsCP.Data;
using Microsoft.AspNetCore.Components;
using System;
namespace FiveCrownsBlazor
{
    public partial class TempSetsBlazor
    {
        [Parameter]
        public string TargetContainerSize { get; set; } = "";
        [Parameter]
        public TempSetsObservable<EnumSuitList, EnumColorList, FiveCrownsCardInformation>? TempPiles { get; set; }
        private string GetKey => Guid.NewGuid().ToString();
    }
}