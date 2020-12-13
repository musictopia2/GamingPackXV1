using Microsoft.AspNetCore.Components;
using ThreeLetterFunCP.Logic;
namespace ThreeLetterFunBlazor
{
    public partial class TileHandBlazor
    {
        [Parameter]
        public TileBoardObservable? Board { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";
    }
}