using Microsoft.AspNetCore.Components;
using XactikaCP.Logic;
namespace XactikaBlazor
{
    public partial class ChooseShapeBlazor
    {
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public ChooseShapeObservable? ShapeData { get; set; }
    }
}