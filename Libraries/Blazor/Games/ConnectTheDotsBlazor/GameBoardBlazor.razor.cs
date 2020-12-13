using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using ConnectTheDotsCP.Data;
using ConnectTheDotsCP.Graphics;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace ConnectTheDotsBlazor
{
    public partial class GameBoardBlazor
    {
        [Parameter]
        public GameBoardGraphicsCP? Graphics { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public ConnectTheDotsGameContainer? Container { get; set; }
        private string LineColor(LineInfo line)
        {
            if (line.Equals(Container!.PreviousLine))
            {
                return cc.Green.ToWebColor();
            }
            return cc.Brown.ToWebColor();
        }
        private string SquareColor(SquareInfo square)
        {
            if (square.Color == 1)
            {
                return cc.Blue.ToWebColor();
            }
            return cc.Red.ToWebColor();
        }
        private async Task MakeMoveAsync(int index)
        {
            if (Container!.Command.IsExecuting || Container.MakeMoveAsync == null)
            {
                return;
            }
            await Container.MakeMoveAsync(index);
        }
    }
}