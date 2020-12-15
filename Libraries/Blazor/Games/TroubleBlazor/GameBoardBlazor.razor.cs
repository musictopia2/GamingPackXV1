using BasicGameFrameworkLibrary.Dice;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using TroubleCP.Data;
using TroubleCP.Graphics;
namespace TroubleBlazor
{
    public partial class GameBoardBlazor
    {
        [Parameter]
        public TroubleGameContainer? GameContainer { get; set; }
        [Parameter]
        public string DiceHeight { get; set; } = "";
        [Parameter]
        public DiceCup<SimpleDice>? Cup { get; set; }
        [Parameter]
        public GameBoardGraphicsCP? GraphicsData { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";
        private Assembly GetAssembly => Assembly.GetAssembly(GetType())!;
        private string GetColor() => GetColor(GameContainer!.SingleInfo!);
        private string GetColor(TroublePlayerItem player) => player.Color.ToColor();
        protected override bool ShouldRender()
        {
            return GameContainer!.SingleInfo!.Color != EnumColorChoice.None;
        }
    }
}