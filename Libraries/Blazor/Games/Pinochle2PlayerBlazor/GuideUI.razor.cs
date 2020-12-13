using Microsoft.AspNetCore.Components;
using Pinochle2PlayerCP.Data;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace Pinochle2PlayerBlazor
{
    public partial class GuideUI
    {
        [Parameter]
        public Pinochle2PlayerVMData? GameData { get; set; }
        private string GetRows => aa.RepeatAuto(20);
    }
}