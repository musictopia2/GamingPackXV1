using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using RollEmCP.Data;
using RollEmCP.Logic;
using System.Threading.Tasks;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace RollEmBlazor
{
    public partial class GameBoardBlazor
    {
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public GameBoardGraphicsCP? BoardData { get; set; }
        private CustomBasicList<NumberInfo> _numbers = new CustomBasicList<NumberInfo>();
        protected override void OnParametersSet()
        {
            _numbers = BoardData!.GetNumberList;
            base.OnParametersSet();
        }
        private async Task SpaceClickedAsync(NumberInfo number)
        {
            if (BoardData!.CanEnableMove == false)
            {
                return; //ignore because can't do.
            }
            await BoardData!.MakeMoveAsync(number);
        }
        private string LineColor(NumberInfo number)
        {
            if (number.IsCrossed)
            {
                return cc.Red.ToWebColor();
            }
            return cc.LimeGreen.ToWebColor();
        }
    }
}