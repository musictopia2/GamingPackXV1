using CountdownCP.Data;
using CountdownCP.Graphics;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace CountdownBlazor
{
    public partial class PlayerBoardBlazor
    {
        [Parameter]
        public CountdownPlayerItem? Player { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";
        private PlayerBoardCP? BoardProcesses { get; set; }
        protected override void OnParametersSet()
        {
            BoardProcesses!.UpdatePlayer(Player!);
        }
        protected override void OnInitialized()
        {
            BoardProcesses = cons!.Resolve<PlayerBoardCP>();
            base.OnInitialized();
        }
        private bool CanShowClick(SimpleNumber number)
        {
            return CountdownVMData.CanChooseNumber!(number);
        }
        private async Task ProcessClick(SimpleNumber number)
        {
            await BoardProcesses!.GameContainer.ProcessCustomCommandAsync(BoardProcesses.GameContainer.MakeMoveAsync!, number);
        }
    }
}