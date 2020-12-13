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
            if (Container!.CanMove == null || Container.MakeMoveAsync == null)
            {
                return;
            }
            if (Container.CanMove.Invoke() == false)
            {
                return;
            }
            await Container.MakeMoveAsync(index);
        }

        //private string TestColor => EnumColorChoice.Blue.ToColor();

        //private string Color(ChineseCheckersPlayerItem player) => EnumColorChoice.Gray.ToColor();
    }
}