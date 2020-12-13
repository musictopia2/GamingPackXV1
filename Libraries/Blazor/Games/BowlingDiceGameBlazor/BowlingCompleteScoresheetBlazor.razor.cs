using BowlingDiceGameCP.Logic;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using Microsoft.AspNetCore.Components;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BowlingDiceGameBlazor
{
    public partial class BowlingCompleteScoresheetBlazor
    {
        [Parameter]
        public BowlingDiceGameMainGameClass? MainGame { get; set; }
        private string GetViewBox()
        {
            float height = (MainGame!.PlayerList.Count * 100) + 50;
            float width = (10 * 150) + 300;
            return $"0 0 {width} {height}"; //can experiement a lot here.
        }
        private string WhiteColor => cc.White.ToWebColor();
    }
}