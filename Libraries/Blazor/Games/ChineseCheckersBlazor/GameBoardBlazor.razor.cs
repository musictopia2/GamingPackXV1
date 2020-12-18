using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BasicGameFrameworkLibrary.NetworkingClasses.Data;
using ChineseCheckersCP.Data;
using ChineseCheckersCP.Logic;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;

namespace ChineseCheckersBlazor
{
    public record PlayerPieceRecord(ChineseCheckersPlayerItem Player, RectangleF Bounds);
    public partial class GameBoardBlazor
    {
        [Parameter]
        public GameBoardGraphicsCP? Graphics { get; set; }
        [Parameter]
        public ChineseCheckersGameContainer? Container { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";

        private string Color(ChineseCheckersPlayerItem player)
        {
            string output = player.Color.ToColor();
            return output;
        }

        private async Task SpaceClickedAsync(int index)
        {
            if (Container!.MakeMoveAsync == null)
            {
                return;
            }
            await Container.MakeMoveAsync(index);
        }
        protected override bool ShouldRender()
        {
            return Container!.SingleInfo!.Color != EnumColorChoice.None;
        }
    }
}