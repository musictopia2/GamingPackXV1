using AggravationCP.Data;
using AggravationCP.Graphics;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using System.Reflection;
namespace AggravationBlazor
{
    public partial class GameBoardBlazor
    {
        [Parameter]
        public GameBoardGraphicsCP? GraphicsData { get; set; }
        [Parameter]
        public AggravationGameContainer? GameContainer { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";
        private Assembly GetAssembly => Assembly.GetAssembly(GetType())!;
        private string GetColor() => GetColor(GameContainer!.SingleInfo!);
        private string GetColor(AggravationPlayerItem player) => player.Color.ToColor();

        protected override bool ShouldRender()
        {
            return GameContainer!.SingleInfo!.Color != EnumColorChoice.None;
        }
    }
}