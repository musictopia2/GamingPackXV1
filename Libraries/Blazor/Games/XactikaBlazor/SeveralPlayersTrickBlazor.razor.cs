using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using Microsoft.AspNetCore.Components;
using XactikaCP.Cards;
using XactikaCP.Data;
namespace XactikaBlazor
{
    public partial class SeveralPlayersTrickBlazor
    {
        [Parameter]
        public BasicTrickAreaObservable<EnumShapes, XactikaCardInformation>? DataContext { get; set; }
        private SeveralPlayersTrickObservable<EnumShapes, XactikaCardInformation, XactikaPlayerItem, XactikaSaveInfo> GetTricks()
        {
            return (SeveralPlayersTrickObservable<EnumShapes, XactikaCardInformation, XactikaPlayerItem, XactikaSaveInfo>)DataContext!;
        }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        [Parameter]
        public bool ExtraLongSecondColumn { get; set; } = false;
        private string RealHeight => $"{TargetHeight}vh";
    }
}